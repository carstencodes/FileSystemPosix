/*
 * BackendFactory.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    using System.Runtime.InteropServices;
    
    /// <summary>
    /// Class for creating the backend matching the current execution system.
    /// </summary>
    internal static class BackendFactory
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Backend" /> class matching the current system.
        /// </summary>
        /// <returns>The new backend instance created.</returns>
        internal static Backend CreateMatching()
        {
            BackendSettings settings = BackendSettings.Current;

            OSPlatform? currentPlatform = GetCurrentPlatform();

            if (currentPlatform.HasValue && settings.Backends.TryGetValue(currentPlatform.Value, out Backend backend))
            {
                return backend;
            }

            return new BackendDummy();
        }

        /// <summary>
        /// Determines the current platform the system is running on.
        /// </summary>
        /// <returns>The platform enumeration.</returns>
        private static OSPlatform? GetCurrentPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return OSPlatform.Windows;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return OSPlatform.Linux;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return OSPlatform.OSX;
            }
            else
            {
                return null;
            }
        }
    }
}