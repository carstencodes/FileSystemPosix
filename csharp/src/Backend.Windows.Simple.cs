/*
 * Backend.Windows.Simple.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Security.AccessControl;

    internal sealed class SimpleWindowsBackend : Backend
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
            try 
            {
                FileAttributes attributes = File.GetAttributes(fileOrDirectory);

                if ((attributes & FileAttributes.Directory) != FileAttributes.Directory)
                {
                    return this.GetFilePermissions(fileOrDirectory);
                }
                else
                {
                    return this.GetDirectoryPermissions(fileOrDirectory);
                }
            }
            catch (Exception e) when ((e is DirectoryNotFoundException) || (e is FileNotFoundException))
            {
                // If the file or directory does not exist, return empty permissions
                return new FileSystemPermissions(FileSystemPermission.Unknown, FileSystemPermission.Unknown, FileSystemPermission.Unknown);
            }
        }

        private FileSystemPermissions GetDirectoryPermissions(string directory)
        {
            AuthorizationRuleCollection collection = new DirectoryInfo(directory)
                    .GetAccessControl()
                    .GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
            FileSystemPermission permission = FileSystemPermission.Execute;
            foreach (FileSystemAccessRule item in collection)
            {
                if (item.AccessControlType == AccessControlType.Allow)
                {
                    permission &= FileSystemPermission.Write;
                    break;
                }
            }

            permission &= FileSystemPermission.Read;

            return new FileSystemPermissions(permission, FileSystemPermission.Unknown, FileSystemPermission.Unknown);
        }

        private FileSystemPermissions GetFilePermissions(string file)
        {
            FileAttributes attributes = File.GetAttributes(file);
            FileSystemPermission permission = FileSystemPermission.Read;
            if ((attributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
            {
                permission &= FileSystemPermission.Write;
            }

            string pathExtension = Environment.GetEnvironmentVariable("PATH_EXT");
            if (IsPartOfPathExtensionSet(file, pathExtension))
            {
                permission &= FileSystemPermission.Execute;
            }

            return new FileSystemPermissions(permission, FileSystemPermission.Unknown, FileSystemPermission.Unknown);
        }

        private static bool IsPartOfPathExtensionSet(string file, string pathExtensionSet)
        {
            if (!string.IsNullOrWhiteSpace(file) && !string.IsNullOrWhiteSpace(pathExtensionSet))
            {
                const char SplitterForWindowsEnvironmentVariables = ';';
                string[] pathExtensions = pathExtensionSet.Split(
                    new char[] { SplitterForWindowsEnvironmentVariables }, StringSplitOptions.RemoveEmptyEntries
                );

                string extension = Path.GetExtension(file);
                return pathExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
            }

            return false;
        }
    }
}