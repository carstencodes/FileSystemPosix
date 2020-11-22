/*
 * lib.rs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

#![deny(missing_docs)]
#![warn(missing_doc_code_examples)]
#![warn(missing_crate_level_docs)]

//! Crate documentation

/// Testing module
#[cfg(test)]
mod tests {
    /// Function to test this in general
    #[test]
    fn it_works() {
        assert_eq!(2 + 2, 4);
    }
}
