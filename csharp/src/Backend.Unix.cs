/*
 * Backend.Windows.Simple.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    using Posix;
    using System;
    using System.Text;

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
            StringBuilder builder = new StringBuilder(bufferSize + 2);
            ushort result = NativeMethods.fs_owning_user_name(fileOrDirectory, builder);
            CheckResult(result);
            return builder.ToString();
        }

        /// <inheritdoc />
        protected sealed override string GetOwningGroup(string fileOrDirectory)
        {
            int bufferSize = this.GetMaximumBufferSize();
            StringBuilder builder = new StringBuilder(bufferSize + 2);
            ushort result = NativeMethods.fs_owning_group_name(fileOrDirectory, builder);
            CheckResult(result);
            return builder.ToString();
        }

        /// <inheritdoc />
        protected sealed override FileSystemPermissions GetPermissions(string fileOrDirectory)
        {
            ushort permissions = 0;
            ushort result = NativeMethods.fs_permissions(fileOrDirectory, out permissions);
            CheckResult(result);
            if (TryConvert(permissions,
                out FileSystemPermission userPermissions,
                out FileSystemPermission groupPermissions,
                out FileSystemPermission otherPermissions))
            {
                return new FileSystemPermissions(userPermissions, groupPermissions, otherPermissions);
            }
            return new FileSystemPermissions(FileSystemPermission.None, FileSystemPermission.Unknown, FileSystemPermission.Unknown);
        }

        private static void CheckResult(ushort nativeMethodResult)
        {
            if (0 != nativeMethodResult)
            {
                throw new NotImplementedException(); // TODO implement
            }
        }

        private static bool TryConvert(ushort permissions, out FileSystemPermission userPermission, out FileSystemPermission groupPermission, out FileSystemPermission otherPermission)
        {
            byte nibble1 = Convert.ToByte((permissions & 0xF000) >> 12);
            byte nibble2 = Convert.ToByte((permissions & 0x0F00) >> 8);
            byte nibble3 = Convert.ToByte((permissions & 0x00F0) >> 4);
            byte nibble4 = Convert.ToByte((permissions & 0x000F) >> 0);

            if (0 != nibble1)
            {
                userPermission = FileSystemPermission.Unknown;
                groupPermission = FileSystemPermission.Unknown;
                otherPermission = FileSystemPermission.Unknown;

                return false; // TODO error encoding
            }

            userPermission = FileSystemPermissionConverter.Parse(nibble2);
            groupPermission = FileSystemPermissionConverter.Parse(nibble3);
            otherPermission = FileSystemPermissionConverter.Parse(nibble4);

            return true;
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