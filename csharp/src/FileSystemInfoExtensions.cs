/*
 * FileSystemInfoExtensions.cs - (C) 2020 by Carsten Igel
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
        /// Checks whether the file system entry represented by this instance is owned by the current user.
        /// </summary>
        /// <param name="info">The current instance.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns><c>true</c>, if the current file is owned by the current user; <c>false</c> otherwise.</returns>
        public static bool IsOwned(this FileSystemInfo info)
        {
            return StringComparer.Ordinal.Equals(info.GetOwningUserName(), Environment.UserName);
        }

        /// <summary>
        /// Gets the name of the user owning the file system entry represented by this instance. 
        /// </summary>
        /// <param name="info">The current instance.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns>The name of the owning user.</returns>
        public static string GetOwningUserName(this FileSystemInfo info)
        {
            return info.GetAttributes().OwningUser;
        }

        /// <summary>
        /// Gets the name of the group owning the file system entry represented by this instance.
        /// </summary>
        /// <param name="info">The current instance.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns>The name of the owning group.</returns>
        public static string GetOwningGroupName(this FileSystemInfo info)
        {
            return info.GetAttributes().OwningGroup;
        }

        /// <summary>
        /// Gets the <see cref="FileSystemAttributes" /> for this instance.
        /// </summary>
        /// <param name="info">The current instance.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns>The file system attributes found.</returns>
        public static FileSystemAttributes GetAttributes(this FileSystemInfo info)
        {
            if (null == info)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return FileSystem.GetAttributes(info.FullName);
        }

        /// <summary>
        /// Gets the <see cref="FileSystemPermissions" /> for this instance.
        /// </summary>
        /// <param name="info">The current instance.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns>The file system permissions.</returns>
        public static FileSystemPermissions GetPermissions(this FileSystemInfo info)
        {
            return info.GetAttributes().Permissions;
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