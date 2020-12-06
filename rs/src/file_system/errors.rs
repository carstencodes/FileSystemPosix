/*
 * errors.rs - (C) 2020 by Carsten Igel
 *
 * Published using the MIT License
 */

use std::io::ErrorKind;

const FS_ERROR_OFFSET: u16 = 0x1000u16;
const IO_ERROR_OFFSET: u16 = 0x2000u16;
const NUMERIC_CONVERSION_OFFSET: u16 = 04000u16;

include!(concat!(env!("OUT_DIR"), "/enum.g.rs"));

pub enum PosixFsError {
    FsErr(FsError),
    IoErr(ErrorKind),
    NumericConversion(u32),
}

impl std::convert::From<PosixFsError> for u16 {
    fn from(posix_fs_error: PosixFsError) -> u16 {
        return match posix_fs_error {
            PosixFsError::FsErr(err) => {
                let fs_error_code: u16 = err.into();
                return fs_error_code + FS_ERROR_OFFSET;
            }
            PosixFsError::IoErr(err) => {
                let io_err_code: u16 = io_error_to_u16(err);
                return io_err_code + IO_ERROR_OFFSET;
            }
            PosixFsError::NumericConversion(_) => NUMERIC_CONVERSION_OFFSET,
        };
    }
}

fn io_error_to_u16(error: ErrorKind) -> u16 {
    return match error {
        ErrorKind::NotFound => 0u16,
        ErrorKind::PermissionDenied => 1u16,
        ErrorKind::BrokenPipe => 2u16,
        ErrorKind::InvalidInput => 3u16,
        ErrorKind::InvalidData => 4u16,
        ErrorKind::Interrupted => 5u16,
        ErrorKind::UnexpectedEof => 6u16,
        ErrorKind::TimedOut => 7u16,
        _ => 64u16,
    };
}
