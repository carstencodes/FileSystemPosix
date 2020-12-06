/*
 * BackendFactory.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Class for creating the backend matching the current execution system.
    /// </summary>
    internal static class BackendFactory
    {
        /// <summary>
        /// All platforms available including the <c>null</c> value.
        /// </summary>
        private static readonly OSPlatform?[] allPlatformValues;

        /// <summary>
        /// Class initializer called before the first method is called.
        /// </summary>
        static BackendFactory()
        {
            Array allPlatforms = Enum.GetValues(typeof(OSPlatform));
            allPlatformValues = new OSPlatform?[allPlatforms.LongLength + 1];
            allPlatformValues[0] = null;
            Array.Copy(allPlatforms, 0, allPlatformValues, 1, allPlatforms.Length);
        }

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
            return Array.Find(allPlatformValues, IsCurrentRuntimePlatform);
        }

        /// <summary>
        /// Checks whether the specified parameter <paramref name="platformOrNull" /> matches the current os platform.
        /// </summary>
        /// <param name="platformOrNull">The platform or a null value.</param>
        /// <returns><c>true</c>, if the current platform matches the current platform; 
        /// <c>false</c>, if it is <c>null</c> or not the current platform.</returns>
        private static bool IsCurrentRuntimePlatform(OSPlatform? platformOrNull)
        {
            return platformOrNull.HasValue && RuntimeInformation.IsOSPlatform(platformOrNull.Value);
        }
    }
}