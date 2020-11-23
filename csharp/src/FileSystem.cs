/*
 * FileSystem.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    using System;

    /// <summary>
    /// Represents an implementation of the file system for a specific file.
    /// </summary>
    public static class FileSystem
    {
        /// <summary>
        /// The system specific backend.
        /// </summary>
        private static readonly Backend backend;

        /// <summary>
        /// Initializes the static members before the class is initialized.
        /// </summary>
        static FileSystem()
        {
            backend = BackendFactory.CreateMatching();
        }

        /// <summary>
        /// Checks whether the specified <paramref name="fileOrDirectory" /> is owned by the current user.
        /// </summary>
        /// <param name="fileOrDirectory">The full path of the file or directory.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns><c>true</c>, if the current file is owned by the current user; <c>false</c> otherwise.</returns>
        public static bool IsOwned(string fileOrDirectory)
        {
            return StringComparer.Ordinal.Equals(GetOwningUserName(fileOrDirectory), Environment.UserName);
        }

        /// <summary>
        /// Gets the name of the group user the specified <paramref name="fileOrDirectory" />.
        /// </summary>
        /// <param name="fileOrDirectory">The full path of the file or directory.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns>The name of the owning user.</returns>
        public static string GetOwningUserName(string fileOrDirectory)
        {
            return GetAttributes(fileOrDirectory).OwningUser;
        }

        /// <summary>
        /// Gets the name of the group owning the specified <paramref name="fileOrDirectory" />.
        /// </summary>
        /// <param name="fileOrDirectory">The full path of the file or directory.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns>The name of the owning group.</returns>
        public static string GetOwningGroupName(string fileOrDirectory)
        {
            return GetAttributes(fileOrDirectory).OwningGroup;
        }

        /// <summary>
        /// Gets the <see cref="FileSystemAttributes" /> for the specified <paramref name="fileOrDirectory"/>.
        /// </summary>
        /// <param name="fileOrDirectory">The full path of the file or directory.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns>The file system attributes found.</returns>
        public static FileSystemAttributes GetAttributes(string fileOrDirectory)
        {
            if (string.IsNullOrWhiteSpace(fileOrDirectory))
            {
                throw new ArgumentNullException(nameof(fileOrDirectory));
            }

            return backend.GetAttributes(fileOrDirectory);
        }

        /// <summary>
        /// Gets the <see cref="FileSystemPermissions" /> for the specified <paramref name="fileOrDirectory"/>.
        /// </summary>
        /// <param name="fileOrDirectory">The full path of the file or directory.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns>The file system permissions found.</returns>
        public static FileSystemPermissions GetPermissions(string fileOrDirectory)
        {
            return GetAttributes(fileOrDirectory).Permissions;
        }

        /// <summary>
        /// Gets the <see cref="FileSystemPermission" /> for the specified <paramref name="fileOrDirectory"/> regarding the owning user.
        /// </summary>
        /// <param name="fileOrDirectory">The full path of the file or directory.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns>The file system permission found.</returns>
        public static FileSystemPermission GetPermissionsOfOwningUser(string fileOrDirectory)
        {
            return GetPermissions(fileOrDirectory).UserPermission;
        }

        /// <summary>
        /// Gets the <see cref="FileSystemPermission" /> for the specified <paramref name="fileOrDirectory"/> regarding the owning group.
        /// </summary>
        /// <param name="fileOrDirectory">The full path of the file or directory.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns>The file system permission found.</returns>
        public static FileSystemPermission GetPermissionsOfOwningGroup(string fileOrDirectory)
        {
            return GetPermissions(fileOrDirectory).GroupPermission;
        }

        /// <summary>
        /// Gets the <see cref="FileSystemPermission" /> for the specified <paramref name="fileOrDirectory"/> regarding all other users.
        /// </summary>
        /// <param name="fileOrDirectory">The full path of the file or directory.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns>The file system permission found.</returns>
        public static FileSystemPermission GetPermissionsOfOtherUsers(string fileOrDirectory)
        {
            return GetPermissions(fileOrDirectory).OtherPermission;
        }
    }
}