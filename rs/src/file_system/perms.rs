/*
 * perms.rs - (C) 2020 by Carsten Igel
 *
 * Published using the MIT License
 */

use libc::mode_t;
use libc::{
    S_IRGRP, S_IROTH, S_IRUSR, S_ISGID, S_ISUID, S_ISVTX, S_IWGRP, S_IWOTH,
    S_IWUSR, S_IXGRP, S_IXOTH, S_IXUSR,
};
use std::convert::TryFrom;
use std::os::unix::fs::PermissionsExt;
use std::path::PathBuf;

pub struct PermissionSet {
    user_permissions: super::nibble::Nibble,
    group_permissions: super::nibble::Nibble,
    other_permissions: super::nibble::Nibble,
}

const PERMISSION_NONE: super::nibble::Nibble = super::nibble::Nibble::B0000;
const PERMISSION_READ: super::nibble::Nibble = super::nibble::Nibble::B0001;
const PERMISSION_WRITE: super::nibble::Nibble = super::nibble::Nibble::B0010;
const PERMISSION_EXECUTE: super::nibble::Nibble = super::nibble::Nibble::B0100;
const PERMISSION_SPECIAL: super::nibble::Nibble = super::nibble::Nibble::B1000;

impl PermissionSet {
    pub fn encode(self: &PermissionSet) -> u16 {
        let mut result = 0u16;

        let user_perms: u16 = self.user_permissions.to_u4().into();
        let group_perms: u16 = self.group_permissions.to_u4().into();
        let other_perms: u16 = self.other_permissions.to_u4().into();

        result = result + user_perms;
        result = result << 4;
        result = result + group_perms;
        result = result << 4;
        result = result + other_perms;
        result = result & 0x0FFFu16;

        return result;
    }
}

fn unwrap_permissions(
    mode: mode_t,
    read_perm: mode_t,
    write_perm: mode_t,
    exec_perm: mode_t,
    special: mode_t,
) -> super::nibble::Nibble {
    let mut permission: super::nibble::Nibble = PERMISSION_NONE;

    if mode & read_perm == read_perm {
        permission = permission | PERMISSION_READ;
    }

    if mode & write_perm == write_perm {
        permission = permission | PERMISSION_WRITE;
    }

    if mode & exec_perm == exec_perm {
        permission = permission | PERMISSION_EXECUTE;
    }

    if mode & special == special {
        permission = permission | PERMISSION_SPECIAL;
    }

    return permission;
}

pub fn get_permissions(path: PathBuf) -> Result<PermissionSet, super::errors::FsError> {
    let file_metadata = path.symlink_metadata();
    match file_metadata {
        Ok(metadata) => {
            let permission_set_mode: u32 = metadata.permissions().mode();

            let permission_set_result = mode_t::try_from(permission_set_mode);

            match permission_set_result {
                Err(_) => {
                    return Err(super::errors::FsError::UnknownError);
                }
                Ok(permission_set) => {
                    let user_permissions = unwrap_permissions(
                        permission_set,
                        S_IRUSR,
                        S_IWUSR,
                        S_IXUSR,
                        S_ISUID,
                    );
                    let group_permissions = unwrap_permissions(
                        permission_set,
                        S_IRGRP,
                        S_IWGRP,
                        S_IXGRP,
                        S_ISGID,
                    );
                    let other_permissions = unwrap_permissions(
                        permission_set,
                        S_IROTH,
                        S_IWOTH,
                        S_IXOTH,
                        S_ISVTX,
                    );

                    return Ok(PermissionSet {
                        user_permissions,
                        group_permissions,
                        other_permissions,
                    });
                }
            }
        }
        Err(_) => {
            return Err(super::errors::FsError::UnknownError);
        }
    }
}
