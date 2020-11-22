/*
 * FileSystemPermissions.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    /// <summary>
    /// Represents the permissions of a file system information entry.
    /// </summary>
    public struct FileSystemPermissions
    {
        /// <summary>
        /// The user permission.
        /// </summary>
        private readonly FileSystemPermission? userPermission;

        /// <summary>
        /// The group permission.
        /// </summary>
        private readonly FileSystemPermission? groupPermission;

        /// <summary>
        /// The permission for other users.
        /// </summary>
        private readonly FileSystemPermission? otherPermission;

        public FileSystemPermissions(
            FileSystemPermission userPermission,
            FileSystemPermission groupPermission,
            FileSystemPermission otherPermission)
        {
            this.userPermission = userPermission;
            this.groupPermission = groupPermission;
            this.otherPermission = otherPermission;
        }

        /// <summary>
        /// Gets the permission set for the owning user modelled as <see cref="FileSystemPermission" />.
        /// </summary>
        /// <value>The permission of the owning user.</value>
        public FileSystemPermission UserPermission
        {
            get
            {
                return this.userPermission ?? FileSystemPermission.Unknown;
            }
        }

        /// <summary>
        /// Gets the permission set for the users within the owning group modelled as <see cref="FileSystemPermission" />.
        /// </summary>
        /// <value>The permission of the users within the owning group.</value>
        public FileSystemPermission GroupPermission
        {
            get
            {
                return this.groupPermission ?? FileSystemPermission.Unknown;
            }
        }

        /// <summary>
        /// Gets the permission set for any other user not matching the other criteria modelled as <see cref="FileSystemPermission" />.
        /// </summary>
        /// <value>The permission of any other user not matching the other criteria.</value>
        public FileSystemPermission OtherPermission
        {
            get
            {
                return this.otherPermission ?? FileSystemPermission.Unknown;
            }
        }
    }
}