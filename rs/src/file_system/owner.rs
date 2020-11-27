/*
 * owner.rs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

use users::{get_user_by_uid, get_group_by_gid};
use std::os::unix::fs::MetadataExt;
use std::path::PathBuf;

pub fn user(path: PathBuf) -> Result<String, super::fs::FsError>
{
    let file_metadata = path.symlink_metadata();
    match file_metadata
    {
        Ok(metadata) => {
            let user_id = metadata.uid();

            let user_name = get_user_by_uid(user_id);

            match user_name
            {
                Some(user) => {
                    let name_result = user.name().to_str();
                    match name_result
                    {
                        Some(name) => {
                            let name_ref = std::string::String::from(name);
                            return Ok(name_ref) 
                        },
                        None => { return Err(super::fs::FsError::UnknownError) }
                    }
                },
                None => { return Err(super::fs::FsError::UnknownError) }
            }
        },
        Err(_) => {
            return Err(super::fs::FsError::UnknownError);
        }
    }
}

pub fn group(path: PathBuf) -> Result<String, super::fs::FsError>
{
    let file_metadata = path.symlink_metadata();
    match file_metadata
    {
        Ok(metadata) => {
            let group_id = metadata.gid();

            let group_name = get_group_by_gid(group_id);

            match group_name
            {
                Some(group) => {
                    let name_result = group.name().to_str();
                    match name_result
                    {
                        Some(name) => {
                            let name_ref = std::string::String::from(name);
                            return Ok(name_ref) 
                        },
                        None => { return Err(super::fs::FsError::UnknownError) }
                    }
                },
                None => { return Err(super::fs::FsError::UnknownError) }
            }
        },
        Err(_) => {
            return Err(super::fs::FsError::UnknownError);
        }
    }
}
