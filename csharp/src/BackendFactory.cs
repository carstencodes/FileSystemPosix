/*
 * BackendFactory.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission
{
    using System;
    
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
            return new BackendDummy(); // TODO
        }
    }
}