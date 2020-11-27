/*
 * perms.rs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

use libc::{S_IRUSR, S_IWUSR, S_IXUSR, S_ISUID, S_IRGRP, S_IWGRP, S_IXGRP, S_ISGID, S_IROTH, S_IWOTH, S_IXOTH, S_ISVTX};
use std::path::PathBuf;
use std::os::unix::fs::PermissionsExt;
use libc::{mode_t};

pub struct PermissionSet
{
    user_permissions: super::nibble::Nibble,
    group_permissions: super::nibble::Nibble,
    other_permissions: super::nibble::Nibble
}

const PERMISSION_NONE    : super::nibble::Nibble = super::nibble::Nibble::B0000;
const PERMISSION_READ    : super::nibble::Nibble = super::nibble::Nibble::B0001;
const PERMISSION_WRITE   : super::nibble::Nibble = super::nibble::Nibble::B0010;
const PERMISSION_EXECUTE : super::nibble::Nibble = super::nibble::Nibble::B0100;
const PERMISSION_SPECIAL : super::nibble::Nibble = super::nibble::Nibble::B1000;

impl PermissionSet
{
    pub fn encode(self: &PermissionSet) -> u16
    {
        let mut result = 0u16;

        let user_perms : u16 = self.user_permissions.to_u4().into();
        let group_perms : u16 = self.group_permissions.to_u4().into();
        let other_perms : u16 = self.other_permissions.to_u4().into();

        result = result + user_perms;
        result = result << 4;
        result = result + group_perms;
        result = result << 4;
        result = result + other_perms;
        result = result & 0x0FFFu16;
        
        return result;
    }
}

fn unwrap_permissions(mode: mode_t, read_perm: mode_t, write_perm: mode_t, exec_perm: mode_t, special: mode_t) -> super::nibble::Nibble
{
    let mut permission : super::nibble::Nibble = PERMISSION_NONE;

    if mode & read_perm == read_perm
    {
        permission = permission | PERMISSION_READ;
    }

    if mode & write_perm == write_perm
    {
        permission = permission | PERMISSION_WRITE;
    }

    if mode & exec_perm == exec_perm
    {
        permission = permission | PERMISSION_EXECUTE;
    }

    if mode & special == special
    {
        permission = permission | PERMISSION_SPECIAL;
    }

    return permission;
}

pub fn get_permissions(path: PathBuf) -> Result<PermissionSet, super::fs::FsError>
{
    let file_metadata = path.symlink_metadata();
    match file_metadata
    {
        Ok(metadata) => {
            let permission_set : mode_t = metadata.permissions().mode();
            
            let user_permissions = unwrap_permissions(permission_set, S_IRUSR, S_IWUSR, S_IXUSR, S_ISUID);
            let group_permissions = unwrap_permissions(permission_set, S_IRGRP, S_IWGRP, S_IXGRP, S_ISGID);
            let other_permissions = unwrap_permissions(permission_set, S_IROTH, S_IWOTH, S_IXOTH, S_ISVTX);

            return Ok(PermissionSet {
                user_permissions,
                group_permissions,
                other_permissions
            })
        },
        Err(_) => {
            return Err(super::fs::FsError::UnknownError);
        }
    }
}
