/*
 * fs.rs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

use std::path::PathBuf;

pub enum FsError
{
    UnknownError
}

pub fn str_to_path(path: &str) -> PathBuf
{
    return PathBuf::from(path);
}
