/*
 * BackendSettings.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Settings for the native backend. Allows to override the settings to improve the interop.
    /// </summary>
    public sealed class BackendSettings
    {
        /// <summary>
        /// The new settings for the backend.
        /// </summary>
        private static readonly Lazy<BackendSettings> settings = new Lazy<BackendSettings>(() => new BackendSettings());

        /// <summary>
        /// The mapping of platform specific backends.
        /// </summary>
        private readonly Dictionary<OSPlatform, Backend> backends;

        /// <summary>
        /// Prevents the public creation of new instances of the <see cref="BackendSettings" /> class.
        /// </summary>
        private BackendSettings()
        {
            this.backends = new Dictionary<OSPlatform, Backend>()
            {
                { OSPlatform.Windows, new SimpleWindowsBackend() },
                { OSPlatform.Linux, new LinuxBackend() },
                { OSPlatform.OSX, new OsXBackend() },
            };
        }

        /// <summary>
        /// The current settings for the library.
        /// </summary>
        internal static BackendSettings Current => settings.Value;

        /// <summary>
        /// Gets the backends currently registered.
        /// </summary>
        internal IReadOnlyDictionary<OSPlatform, Backend> Backends => backends;

        /// <summary>
        /// Replaces the backend functions for a specific <paramref name="platform" />.
        /// </summary>
        /// <param name="platform">The platform to replace the backend for.</param>
        /// <param name="getOwningUser">The function to determine the owning user.</param>
        /// <param name="getOwningGroup">The function to determine the owning group.</param>
        /// <param name="getPermissions">The function to get the permissions of a file system entry.</param>
        /// <returns><c>true</c>, if the backend could be set; <c>false</c> otherwise.</returns>
        public static bool SetupBackend(OSPlatform platform, UserOrGroupNameFunction getOwningUser, UserOrGroupNameFunction getOwningGroup, FileSystemEntryPermissionsFunction getPermissions)
        {
            try
            {
                AdaptableBackend newBackend = new AdaptableBackend(getOwningUser, getOwningGroup, getPermissions);
                Current.backends[platform] = newBackend;
                return true;            
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}