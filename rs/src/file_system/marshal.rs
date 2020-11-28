/*
 * marshal.rs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

use libc::c_char;
use std::ffi::CStr;
use std::str::Utf8Error;
use std::string::String;

/// 
pub fn c_string_to_str(c_string_pointer: * const c_char) -> Option<String> 
{
    if c_string_pointer.is_null()
    {
        return None;
    }

    let value_ref : &CStr = unsafe { CStr::from_ptr(c_string_pointer) };
    let value : Result<&str, Utf8Error> = value_ref.to_str();
    return match value
    {
        Ok(res) => Some( String::from(res) ),
        Err(_) => None,
    };
}

/// 
pub fn str_to_c_string(value: String, c_string_pointer: * mut c_char) -> bool
{
    if c_string_pointer.is_null()
    {
        return false;
    }

    let value_ptr = value.as_bytes();
    unsafe
    {
        let c_string = CStr::from_bytes_with_nul_unchecked(value_ptr);
        std::ptr::copy(c_string.as_ptr(), c_string_pointer, value.len());
    }

    return true;
}
