/*
 * (C) 2020 by Carsten Igel
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
        /// Gets the <see cref="FileSystemEntries" /> for the specified <paramref name="fileOrDirectory"/>.
        /// </summary>
        /// <param name="fileOrDirectory">The full path of the file or directory.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns>The file system entries found.</returns>
        public static FileSystemEntries GetEntries(string fileOrDirectory)
        {
            if (string.IsNullOrWhiteSpace(fileOrDirectory))
            {
                throw new ArgumentNullException(nameof(fileOrDirectory));
            }

            return backend.GetEntries(fileOrDirectory);
        }

        /// <summary>
        /// Gets the <see cref="FileSystemPermissions" /> for the specified <paramref name="fileOrDirectory"/>.
        /// </summary>
        /// <param name="fileOrDirectory">The full path of the file or directory.</param>
        /// <exception cref="ArgumentNullException">If this method is applied to an instance equivalent to <c>null</c>.</exception>
        /// <returns>The file system permissions found.</returns>
        public static FileSystemPermissions GetPermissions(string fileOrDirectory)
        {
            return GetEntries(fileOrDirectory).Permissions;
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