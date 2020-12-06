/*
 * NativeMethods.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission.Posix
{
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// Native methods for the interaction with a linux specific backend.
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("posix_permissions", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int sys_get_maximum_login_name();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSystemEntry"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [DllImport("posix_permissions", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort fs_owning_user_name([MarshalAs(UnmanagedType.LPWStr)] string fileSystemEntry, StringBuilder userName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSystemEntry"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        [DllImport("posix_permissions", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort fs_owning_group_name([MarshalAs(UnmanagedType.LPWStr)] string fileSystemEntry, StringBuilder groupName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSystemEntry"></param>
        /// <param name="permissionSet"></param>
        /// <returns></returns>
        [DllImport("posix_permissions", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort fs_permissions([MarshalAs(UnmanagedType.LPWStr)] string fileSystemEntry, out ushort permissionSet);
    }
}