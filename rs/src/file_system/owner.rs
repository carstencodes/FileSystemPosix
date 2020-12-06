/*
 * owner.rs - (C) 2020 by Carsten Igel
 *
 * Published using the MIT License
 */

use std::os::unix::fs::MetadataExt;
use std::path::PathBuf;
use users::{get_group_by_gid, get_user_by_uid};

pub fn user(path: PathBuf) -> Result<String, super::errors::PosixFsError> {
    let file_metadata = path.symlink_metadata();
    match file_metadata {
        Ok(metadata) => {
            let user_id = metadata.uid();

            let user_name = get_user_by_uid(user_id);

            match user_name {
                Some(user) => {
                    let name_result = user.name().to_str();
                    match name_result {
                        Some(name) => {
                            let name_ref = std::string::String::from(name);
                            return Ok(name_ref);
                        }
                        None => {
                            return Err(super::errors::PosixFsError::FsErr(
                                super::errors::FsError::GroupNotFound,
                            ))
                        }
                    }
                }
                None => {
                    return Err(super::errors::PosixFsError::FsErr(
                        super::errors::FsError::UserNotFound,
                    ))
                }
            }
        }
        Err(io_error) => {
            let error = io_error.kind();
            let resulting_error = super::errors::PosixFsError::IoErr(error);
            return Err(resulting_error);
        }
    }
}

pub fn group(path: PathBuf) -> Result<String, super::errors::PosixFsError> {
    let file_metadata = path.symlink_metadata();
    match file_metadata {
        Ok(metadata) => {
            let group_id = metadata.gid();

            let group_name = get_group_by_gid(group_id);

            match group_name {
                Some(group) => {
                    let name_result = group.name().to_str();
                    match name_result {
                        Some(name) => {
                            let name_ref = std::string::String::from(name);
                            return Ok(name_ref);
                        }
                        None => {
                            return Err(super::errors::PosixFsError::FsErr(
                                super::errors::FsError::OsStringConversion,
                            ))
                        }
                    }
                }
                None => {
                    return Err(super::errors::PosixFsError::FsErr(
                        super::errors::FsError::GroupNotFound,
                    ))
                }
            }
        }
        Err(io_error) => {
            let error = io_error.kind();
            let resulting_error = super::errors::PosixFsError::IoErr(error);
            return Err(resulting_error);
        }
    }
}
