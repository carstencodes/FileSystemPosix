/*
 * Backend.Dummy.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    using System.IO;

    /// <summary>
    /// A dead simple implementation of the basic <see cref="Backend" /> class.
    /// </summary>
    internal sealed class BackendDummy : Backend
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
            return new FileSystemPermissions();
        }
    }
}