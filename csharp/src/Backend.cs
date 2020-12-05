/*
 * Backend.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    using System.IO;

    /// <summary>
    /// Basic implementation of a system specific backend for the file system backend.
    /// </summary>
    internal abstract class Backend
    {
        /// <summary>
        /// Gets the <see cref="FileSystemAttributes" /> that belong to the <paramref name="fileOrDirectory" />.
        /// </summary>
        /// <param name="fileOrDirectory">The path to the file or directory.</param>
        /// <returns>The file system attributes.</returns>
        internal FileSystemAttributes GetAttributes(string fileOrDirectory)
        {
            if (!File.Exists(fileOrDirectory) || !Directory.Exists(fileOrDirectory))
            {
                return new FileSystemAttributes();
            }

            string user = this.GetOwningUser(fileOrDirectory);
            string group = this.GetOwningGroup(fileOrDirectory);
            FileSystemPermissions permissions = this.GetPermissions(fileOrDirectory);

            return new FileSystemAttributes(
                user, group, permissions);
        }

        /// <summary>
        /// Gets the user that the specified <paramref name="fileOrDirectory" /> entry belongs to.
        /// </summary>
        /// <param name="fileOrDirectory">The file system entry.</param>
        /// <returns>The user name of the owning user.</returns>
        protected abstract string GetOwningUser(string fileOrDirectory);

        /// <summary>
        /// Gets the group that the specified <paramref name="fileOrDirectory" /> entry belongs to.
        /// </summary>
        /// <param name="fileOrDirectory">The file system entry.</param>
        /// <returns>The group name of the owning group.</returns>
        protected abstract string GetOwningGroup(string fileOrDirectory);

        /// <summary>
        /// Gets the permissions of the specified <paramref name="fileOrDirectory" />.
        /// </summary>
        /// <param name="fileOrDirectory">The file system entry.</param>
        /// <returns>The file system permissions.</returns>
        protected abstract FileSystemPermissions GetPermissions(string fileOrDirectory);
    }
}