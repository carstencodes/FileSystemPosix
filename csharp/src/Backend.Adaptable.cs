/*
 * Backend.Adaptable.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    using System;

    /// <summary>
    /// Represents an adjustable implementation of a backend.
    /// </summary>
    internal sealed class AdaptableBackend : Backend
    {
        /// <summary>
        /// Function to call for the owning user.
        /// </summary>
        private readonly UserOrGroupNameFunction getOwningUser;

        /// <summary>
        /// Function to call for the owning group.
        /// </summary>
        private readonly UserOrGroupNameFunction getOwningGroup;

        /// <summary>
        /// Function to call for the permissions.
        /// </summary>
        private readonly FileSystemEntryPermissionsFunction getPermissions;

        /// <summary>
        /// Creates a new instance of the <see cref="AdaptableBackend" /> class using the specified parameters.
        /// </summary>
        /// <param name="getOwningUser">Function to get the name of the owning user.</param>
        /// <param name="getOwningGroup">Function to get the name of the owning group.</param>
        /// <param name="getPermissions">Function to get the file system permissions.</param>
        internal AdaptableBackend(UserOrGroupNameFunction getOwningUser, UserOrGroupNameFunction getOwningGroup, FileSystemEntryPermissionsFunction getPermissions)
        {
            this.getOwningUser = getOwningUser ?? throw new ArgumentNullException(nameof(getOwningUser));
            this.getOwningGroup = getOwningGroup ?? throw new ArgumentNullException(nameof(getOwningGroup));
            this.getPermissions = getPermissions ?? throw new ArgumentNullException(nameof(getPermissions));
        }

        /// <inheritdoc />
        protected sealed override string GetOwningUser(string fileOrDirectory)
        {
            return this.getOwningUser(fileOrDirectory) ?? string.Empty;
        }

        /// <inheritdoc />
        protected sealed override string GetOwningGroup(string fileOrDirectory)
        {
            return this.getOwningGroup(fileOrDirectory) ?? string.Empty;
        }

        /// <inheritdoc />
        protected sealed override FileSystemPermissions GetPermissions(string fileOrDirectory)
        {
            return this.getPermissions(fileOrDirectory);
        }
    }
}