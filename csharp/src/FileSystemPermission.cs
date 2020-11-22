/*
 * FileSystemPermission.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    using System;

    /// <summary>
    /// Represents a permission for a file system entry of a single permission set (user, group, others).
    /// </summary>
    [Flags]
    public enum FileSystemPermission
    {
        /// <summary>
        /// Represents a value that is not associated with any permission.
        /// </summary>
        None = 0,

        /// <summary>
        /// Represents a value that is associated only with the execute permission (x).
        /// </summary>
        Execute = 1,

        /// <summary>
        /// Represents a value that is associated only with the write permission (w).
        /// </summary>
        Write = 2,

        /// <summary>
        /// Represents a value that is associated only with the read permission (r).
        /// </summary>
        Read = 4,

        /// <summary>
        /// Represents a value that is associated only with the sticky bit (s).
        /// </summary>
        Sticky = 8,

        /// <summary>
        /// Represents a value that implies that there is no information available.
        /// </summary>
        Unknown = 16,
    }
}