/*
 * (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    using System;

    /// <summary>
    /// Gets the entry information for a specific file system information.
    /// </summary>
    public struct FileSystemEntries
    {
        /// <summary>
        /// Creates a new instance of the <see cref="FileSystemEntries" /> struct.
        /// </summary>
        /// <param name="user">The name of the owning user.</param>
        /// <param name="group">The name of the owning group.</param>
        /// <param name="permissions">The permissions.</param>
        public FileSystemEntries(
            string user, 
            string group, 
            FileSystemPermissions permissions) 
        {
            this.Permissions = permissions;
            this.OwningUser = user ?? throw new ArgumentNullException(nameof(user));
            this.OwningGroup = group ?? throw new ArgumentNullException(nameof(group));
        }

        /// <summary>
        /// Gets the file system permissions.
        /// </summary>
        /// <value>The permissions.</value>
        public FileSystemPermissions Permissions { get; }

        /// <summary>
        /// Gets the name of the owning user.
        /// </summary>
        /// <value>The user.</value>
        public string OwningUser { get; }

        /// <summary>
        /// Gets the name of the owning group.
        /// </summary>
        /// <value>The group.</value>
        public string OwningGroup { get; }
    }
}