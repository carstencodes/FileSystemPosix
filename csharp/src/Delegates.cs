/*
 * Delegates.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    /// <summary>
    /// Specifies a function called to get the name of an owning user or group for the specified <paramref name="fileOrDirectory" />.
    /// This should only be used for extension purposes.
    /// </summary>
    /// <param name="fileOrDirectory">The file or directory whose owning user or group name should be determined.</param>
    /// <returns>The name of the user or group.</returns>
    public delegate string UserOrGroupNameFunction(string fileOrDirectory);

    /// <summary>
    /// Specifies a function called to get the file system permissions for the specified <paramref name="fileOrDirectory" />.
    /// This should only be used for extension purposes.
    /// </summary>
    /// <param name="fileOrDirectory">The file or directory whose permissions should be determined.</param>
    /// <returns>The file system permissions.</returns>
    public delegate FileSystemPermissions FileSystemEntryPermissionsFunction(string fileOrDirectory);
}