/*
 * mod.rs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

pub mod marshal;
pub mod nibble;
pub mod owner;
pub mod perms;

include!(concat!(env!("OUT_DIR"), "/enum.g.rs"));
