/*
 * lib.rs - (C) 2020 by Carsten Igel
 *
 * Published using the MIT License
 */

#![deny(missing_docs)]

//! Crate documentation

extern crate libc;

use libc::{c_char, _SC_LOGIN_NAME_MAX};
use std::path::PathBuf;

mod file_system;

enum FileSystemQueryResult {
    Ok = 0,
    NoResult = 1,
    ParameterIsNull = 17,
    StringConversionError = 18,
}

fn fsqr_as_u16(value: FileSystemQueryResult) -> u16 {
    match value {
        FileSystemQueryResult::Ok => 0u16,
        FileSystemQueryResult::NoResult => 1u16,
        FileSystemQueryResult::ParameterIsNull => 17u16,
        FileSystemQueryResult::StringConversionError => 18u16,
    }
}

#[no_mangle]
/// External
pub extern "C" fn sys_get_maximum_login_name() -> i32 {
    return _SC_LOGIN_NAME_MAX.into();
}

#[no_mangle]
/// External
pub extern "C" fn fs_owning_user_name(
    fs_entry: *const c_char,
    fs_user_name: *mut c_char,
) -> u16 {
    if fs_entry.is_null() {
        return fsqr_as_u16(FileSystemQueryResult::ParameterIsNull);
    }

    if fs_user_name.is_null() {
        return fsqr_as_u16(FileSystemQueryResult::ParameterIsNull);
    }

    let file_path_res = file_system::marshal::c_string_to_str(fs_entry);

    match file_path_res {
        Some(file_path) => {
            let path: PathBuf = PathBuf::from(file_path);
            let user_result: Result<String, file_system::FsError> =
                file_system::owner::user(path);
            match user_result {
                Ok(user) => {
                    if file_system::marshal::str_to_c_string(user, fs_user_name)
                    {
                        return fsqr_as_u16(FileSystemQueryResult::Ok);
                    }

                    return fsqr_as_u16(
                        FileSystemQueryResult::StringConversionError,
                    );
                }
                Err(_) => {
                    return fsqr_as_u16(FileSystemQueryResult::NoResult);
                }
            }
        }
        None => {
            return fsqr_as_u16(FileSystemQueryResult::StringConversionError);
        }
    }
}

#[no_mangle]
/// External
pub extern "C" fn fs_owning_group_name(
    fs_entry: *const c_char,
    fs_group_name: *mut c_char,
) -> u16 {
    if fs_entry.is_null() {
        return fsqr_as_u16(FileSystemQueryResult::ParameterIsNull);
    }

    if fs_group_name.is_null() {
        return fsqr_as_u16(FileSystemQueryResult::ParameterIsNull);
    }

    let file_path_res = file_system::marshal::c_string_to_str(fs_entry);

    match file_path_res {
        Some(file_path) => {
            let path: PathBuf = PathBuf::from(file_path);
            let group_result: Result<String, file_system::FsError> =
                file_system::owner::group(path);
            match group_result {
                Ok(group) => {
                    if file_system::marshal::str_to_c_string(
                        group,
                        fs_group_name,
                    ) {
                        return fsqr_as_u16(FileSystemQueryResult::Ok);
                    }

                    return fsqr_as_u16(
                        FileSystemQueryResult::StringConversionError,
                    );
                }
                Err(_) => {
                    return fsqr_as_u16(FileSystemQueryResult::NoResult);
                }
            }
        }
        None => {
            return fsqr_as_u16(FileSystemQueryResult::StringConversionError);
        }
    }
}

#[no_mangle]
/// External
pub extern "C" fn fs_permissions(
    fs_entry: *const c_char,
    permission: *mut u16,
) -> u16 {
    if fs_entry.is_null() {
        return fsqr_as_u16(FileSystemQueryResult::ParameterIsNull);
    }

    if permission.is_null() {
        return fsqr_as_u16(FileSystemQueryResult::ParameterIsNull);
    }

    let file_path_res = file_system::marshal::c_string_to_str(fs_entry);

    match file_path_res {
        Some(file_path) => {
            let path: PathBuf = PathBuf::from(file_path);
            let permission_result: Result<
                file_system::perms::PermissionSet,
                file_system::FsError,
            > = file_system::perms::get_permissions(path);
            match permission_result {
                Ok(perm) => {
                    unsafe {
                        *permission = perm.encode();
                    }

                    return fsqr_as_u16(FileSystemQueryResult::Ok);
                }
                Err(_err) => {
                    return fsqr_as_u16(FileSystemQueryResult::NoResult);
                }
            }
        }
        None => {
            return fsqr_as_u16(FileSystemQueryResult::StringConversionError);
        }
    }
}

/// Testing module
#[cfg(test)]
mod tests {
    /// Function to test this in general
    #[test]
    fn it_works() {
        assert_eq!(2 + 2, 4);
    }
}
