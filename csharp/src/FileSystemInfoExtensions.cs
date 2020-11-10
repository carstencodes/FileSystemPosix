/*
 * (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    using System;
    using System.IO;

    /// <summary>
    /// Extensions for the <see cref="FileSystemInfo" /> class and its sub-classes.
    /// </summary>
    public static class FileSystemInfoExtensions
    {
        /// <summary>
        /// Gets the <see cref="FileSystemEntries" /> for this instance.
        /// </summary>
        /// <param name="info">The current instance.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns>The file system entries found.</returns>
        public static FileSystemEntries GetEntries(this FileSystemInfo info)
        {
            if (null == info)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return FileSystem.GetEntries(info.FullName);
        }

        /// <summary>
        /// Gets the <see cref="FileSystemPermissions" /> for this instance.
        /// </summary>
        /// <param name="info">The current instance.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns>The file system permissions.</returns>
        public static FileSystemPermissions GetPermissions(this FileSystemInfo info)
        {
            return info.GetEntries().Permissions;
        }

        /// <summary>
        /// Gets the <see cref="FileSystemPermission" /> regarding the owning user for this instance.
        /// </summary>
        /// <param name="info">The current instance.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns>The user permissions.</returns>
        public static FileSystemPermission GetPermissionsOfOwningUser(this FileSystemInfo info)
        {
            return info.GetPermissions().UserPermission;
        }

        /// <summary>
        /// Gets the <see cref="FileSystemPermission" /> regarding the owning group for this instance.
        /// </summary>
        /// <param name="info">The current instance.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns>The group permissions.</returns>
        public static FileSystemPermission GetPermissionsOfOwningGroup(this FileSystemInfo info)
        {
            return info.GetPermissions().GroupPermission;
        }

        /// <summary>
        /// Gets the <see cref="FileSystemPermission" /> regarding any other user for this instance.
        /// </summary>
        /// <param name="info">The current instance.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns>The permissions for any other user.</returns>
        public static FileSystemPermission GetPermissionsOfOtherUsers(this FileSystemInfo info)
        {
            return info.GetPermissions().OtherPermission;
        }
    }
}