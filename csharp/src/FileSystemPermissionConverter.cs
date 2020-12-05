/*
 * Posix.FileSystem.Permission.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    /// <summary>
    /// Converter for the conversion of <see cref="System.Byte" /> values
    /// to instances of <see cref="FileSystemPermission" /> values.
    /// </summary>
    internal static class FileSystemPermissionConverter
    {
        internal static FileSystemPermission Parse(byte value)
        {
            const byte UnknownFileSystemPermission = (byte)FileSystemPermission.Unknown;
            const byte ReadPermissions = (byte)FileSystemPermission.Read;
            const byte WritePermissions = (byte)FileSystemPermission.Write;
            const byte ExecutePermissions = (byte)FileSystemPermission.Execute;
            const byte SpecialPermissions = (byte)FileSystemPermission.Special;

            if (value > UnknownFileSystemPermission)
            {
                return FileSystemPermission.Unknown;
            }

            FileSystemPermission permission = FileSystemPermission.None;

            UpdateValueConditional(((value & ReadPermissions) == ReadPermissions), ref permission, FileSystemPermission.Read);
            UpdateValueConditional(((value & WritePermissions) == WritePermissions), ref permission, FileSystemPermission.Write);
            UpdateValueConditional(((value & ExecutePermissions) == ExecutePermissions), ref permission, FileSystemPermission.Execute);
            UpdateValueConditional(((value & SpecialPermissions) == SpecialPermissions), ref permission, FileSystemPermission.Special);

            return permission;
        }

        private static void UpdateValueConditional(bool condition, ref FileSystemPermission target, FileSystemPermission permission)
        {
            if (condition)
            {
                target = target | permission;
            }
        }
    }
}