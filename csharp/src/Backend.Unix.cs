/*
 * Backend.Windows.Simple.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    using System;
    using System.Text;

    using Posix;

    /// <summary>
    /// Basic implementation of a linux specific backend for the file system backend using native interop.
    /// </summary>
    internal sealed class UnixBackend : Backend
    {
        private int maximumBufferSize = -1;

        /// <inheritdoc />
        protected sealed override string GetOwningUser(string fileOrDirectory)
        {
            int bufferSize = this.GetMaximumBufferSize();
            StringBuilder builder = new StringBuilder(bufferSize);
            ushort result = NativeMethods.fs_owning_user_name(fileOrDirectory, builder);
            // TODO check result
            return builder.ToString();
        }

        /// <inheritdoc />
        protected sealed override string GetOwningGroup(string fileOrDirectory)
        {
            int bufferSize = this.GetMaximumBufferSize();
            StringBuilder builder = new StringBuilder(bufferSize);
            ushort result = NativeMethods.fs_owning_group_name(fileOrDirectory, builder);
            // TODO check result
            return builder.ToString();
        }

        /// <inheritdoc />
        protected sealed override FileSystemPermissions GetPermissions(string fileOrDirectory)
        {
            ushort permissions = 0;
            ushort result = NativeMethods.fs_permissions(fileOrDirectory, out permissions);
            // TODO check result
            // TODO convert permissions
            return new FileSystemPermissions(FileSystemPermission.None, FileSystemPermission.Unknown, FileSystemPermission.Unknown);
        }

        private int GetMaximumBufferSize()
        {
            if (this.maximumBufferSize < 0)
            {
                int bufferSize = NativeMethods.sys_get_maximum_login_name();
                this.maximumBufferSize = bufferSize;
            }

            return this.maximumBufferSize + 1;
        }
    }
}