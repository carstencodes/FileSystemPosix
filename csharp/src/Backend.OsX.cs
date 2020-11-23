/*
 * Backend.Windows.Simple.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    using OsX;

    /// <summary>
    /// Basic implementation of an OS X specific backend for the file system backend using native interop.
    /// </summary>
    internal sealed class OsXBackend : PosixBackend
    {
        /// <inheritdoc />
        protected sealed override string GetOwningUser(string fileOrDirectory)
        {
            return string.Empty;
        }

        /// <inheritdoc />
        protected sealed override string GetOwningGroup(string fileOrDirectory)
        {
            return string.Empty;
        }

        /// <inheritdoc />
        protected sealed override FileSystemPermissions GetPermissions(string fileOrDirectory)
        {
            return new FileSystemPermissions(FileSystemPermission.None, FileSystemPermission.Unknown, FileSystemPermission.Unknown);
        }
    }
}