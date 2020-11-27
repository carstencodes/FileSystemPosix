/*
 * nibble.rs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

use std::ops::BitOr;

#[repr(u8)]
pub enum Nibble
{
    B0000 = 0b0000,
    B0001 = 0b0001,
    B0010 = 0b0010,
    B0011 = 0b0011,
    B0100 = 0b0100,
    B0101 = 0b0101,
    B0110 = 0b0110,
    B0111 = 0b0111,
    B1000 = 0b1000,
    B1001 = 0b1001,
    B1010 = 0b1010,
    B1011 = 0b1011,
    B1100 = 0b1100,
    B1101 = 0b1101,
    B1110 = 0b1110,
    B1111 = 0b1111,
}

impl BitOr for Nibble
{
    type Output = Self;

    // rhs is the "right-hand side" of the expression `a | b`
    fn bitor(self, rhs: Self) -> Self::Output {
        let lhs_ref = &self;
        let rhs_ref = &rhs;
        let lhsu8 : u8 = lhs_ref.into();
        let rhsu8 : u8 = rhs_ref.into();
        let mut result : u8 = lhsu8 | rhsu8;
        result = result & 0x0F;
        let element : Nibble = result.into();
        return element;
    }
}

impl std::default::Default for Nibble
{
    fn default() -> Self
    {
        return Nibble::B0000;
    }
}

impl std::convert::From<u8> for Nibble
{
    fn from(value: u8) -> Nibble {
        if value > 0x0Fu8
        {
            return Self::from(value & 0x0Fu8);
        }

        let result = match value
        {
            0b0000 => Some(Nibble::B0000),
            0b0001 => Some(Nibble::B0001),
            0b0010 => Some(Nibble::B0010),
            0b0011 => Some(Nibble::B0011),
            0b0100 => Some(Nibble::B0100),
            0b0101 => Some(Nibble::B0101),
            0b0110 => Some(Nibble::B0110),
            0b0111 => Some(Nibble::B0111),
            0b1000 => Some(Nibble::B1000),
            0b1001 => Some(Nibble::B1001),
            0b1010 => Some(Nibble::B1010),
            0b1011 => Some(Nibble::B1011),
            0b1100 => Some(Nibble::B1100),
            0b1101 => Some(Nibble::B1101),
            0b1110 => Some(Nibble::B1110),
            0b1111 => Some(Nibble::B1111),
            _ => None, // Should not happen
        };

        return result.unwrap_or_default();
    }
}

impl std::convert::From<&Nibble> for u8
{
    fn from(nibble: &Nibble) -> u8 {
        match nibble
        {
            Nibble::B0000 => 0b0000,
            Nibble::B0001 => 0b0001,
            Nibble::B0010 => 0b0010,
            Nibble::B0011 => 0b0011,
            Nibble::B0100 => 0b0100,
            Nibble::B0101 => 0b0101,
            Nibble::B0110 => 0b0110,
            Nibble::B0111 => 0b0111,
            Nibble::B1000 => 0b1000,
            Nibble::B1001 => 0b1001,
            Nibble::B1010 => 0b1010,
            Nibble::B1011 => 0b1011,
            Nibble::B1100 => 0b1100,
            Nibble::B1101 => 0b1101,
            Nibble::B1110 => 0b1110,
            Nibble::B1111 => 0b1111,
        }
    }
}

impl Nibble
{
    pub fn to_u4(self: &Nibble) -> u8
    {
        let value : u8 = self.into();
        return value & 0x0F;
    }
}
