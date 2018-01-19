﻿using System;
using System.Collections.Generic;
using System.Reflection;


namespace Axle.Environment
{
	/// <summary>
	/// An interface serving as an abstraction to a .NET runtime.
	/// </summary>
    public partial interface IRuntime
    {
        /// <summary>
        /// Converts the provided by the <paramref name="resourceName"/> parameter string to
        /// a valid path for an embedded resource.
        /// </summary>
        /// <param name="resourceName">
        /// The resource name to be converted to an embedment path.
        /// </param>
        /// <returns>
        /// A valid embedded resource path.
        /// </returns>
        string GetEmbeddedResourcePath(string resourceName);

        IEnumerable<Assembly> GetAssemblies();

        /// <summary>
        /// Instructs the current runtime to load the assembly specified by the 
        /// <paramref name="assemblyName"/> parameter. 
        /// </summary>
        /// <param name="assemblyName">
        /// The name of the assebly to be loaded. 
        /// </param>
        /// <returns>
        /// An <see cref="Assembly"/> object corresponding to the given <paramref name="assemblyName"/> parameter. 
        /// </returns>
        Assembly LoadAssembly(string assemblyName);

        /// <summary>
        /// Gets a <see cref="System.Version"/> object that describes the major, minor, build, and revision numbers of 
        /// the current CLR implementation.
        /// </summary>
        Version Version { get; }

        /// <summary>
        /// Gets a <see cref="System.Version"/> object that describes the major, minor, build, and revision numbers of 
        /// the common language runtime that is supported by the current CLR implementation.
        /// </summary>
        Version FrameworkVersion { get; }

        /// <summary>
        /// Gets a <see cref="RuntimeImplementation"/> value that describes the type of the .NET runtime that executes the current code.
        /// </summary>
        RuntimeImplementation Implementation { get; }
    }
}