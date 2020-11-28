/*
 * FileSystemPermissionExtensions.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    /// <summary>
    /// Extensions for the <see cref="FileSystemPermission" /> enumeration.
    /// </summary>
    public static class FileSystemPermissionExtensions
    {
        /// <summary>
        /// Checks if the <paramref name="permission" /> is <see cref="FileSystemPermission.Unknown" />.
        /// </summary>
        /// <param name="permission">The permission to check.</param>
        /// <returns><c>true</c>, if the permission is not known; <c>false</c> otherwise.</returns>
        public static bool IsUnknown(this FileSystemPermission permission)
        {
            return permission.HasFlag(FileSystemPermission.Unknown);
        }

        /// <summary>
        /// Checks, whether the <paramref name="permission" /> allows permission transfer using the special bit.
        /// </summary>
        /// <param name="permission">The permission to check.</param>
        /// <returns><c>true</c>, if the permission sticky bit is set; <c>false</c> otherwise.</returns>
        public static bool IsSpecial(this FileSystemPermission permission)
        {
            return permission.HasFlag(FileSystemPermission.Special);
        }

        /// <summary>
        /// Checks, whether the <paramref name="permission" /> allows read access.
        /// </summary>
        /// <param name="permission">The permission to check.</param>
        /// <returns><c>true</c>, if the permission allows read access; <c>false</c> otherwise.</returns>
        public static bool CanRead(this FileSystemPermission permission)
        {
            return permission.HasFlag(FileSystemPermission.Read);
        }

        /// <summary>
        /// Checks, whether the <paramref name="permission" /> allows write access.
        /// </summary>
        /// <param name="permission">The permission to check.</param>
        /// <returns><c>true</c>, if the permission allows write access; <c>false</c> otherwise.</returns>
        public static bool CanWrite(this FileSystemPermission permission)
        {
            return permission.HasFlag(FileSystemPermission.Write);
        }

        /// <summary>
        /// Checks, whether the <paramref name="permission" /> allows direct execution.
        /// </summary>
        /// <param name="permission">The permission to check.</param>
        /// <returns><c>true</c>, if the permission allows direct execution; <c>false</c> otherwise.</returns>
        public static bool CanExecute(this FileSystemPermission permission)
        {
            return permission.HasFlag(FileSystemPermission.Execute);
        }

        /// <summary>
        /// Checks, whether the <paramref name="permission" /> is correct, i.e. it is either
        /// <see cref="FileSystemPermission.Unknown" /> or a concrete permission.
        /// </summary>
        /// <param name="permission">The permission to check.</param>
        /// <returns><c>true</c>, if the permission is valid; <c>false</c> otherwise.</returns>
        public static bool IsValid(this FileSystemPermission permission)
        {
            return ((permission == FileSystemPermission.None)
                    || (permission.CanRead()
                    && permission.CanWrite()
                    && permission.CanExecute())) ^ permission.IsUnknown();
        }
    }
}