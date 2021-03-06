﻿using System.Threading.Tasks;

using Axle.References;


namespace Axle.Resources.Extraction
{
    /// <summary>
    /// An interface representing a resource extractor; that is, an object responsible for locating raw resources before being unmarshaled.
    /// </summary>
    public interface IResourceExtractor
    {
        /// <summary>
        /// Attempts to locate a resource based on the provided parameters.
        /// </summary>
        /// <param name="context">
        /// A <see cref="ResourceContext"/> instance that represents the context
        /// of the current resource extraction.
        /// </param>
        /// <param name="name">
        /// A <see cref="string"/> object used to identify the requested resource.
        /// </param>
        /// <returns>
        /// A <see cref="ResourceInfo"/> instance representing the extracted resource, or <c>null</c> if the resource was not found. 
        /// </returns>
        /// <seealso cref="ExtractAsync"/>
        /// <seealso cref="ResourceContext"/>
        Nullsafe<ResourceInfo> Extract(ResourceContext context, string name);

        /// <summary>
        /// Attempts to asynchronously locate a resource based on the provided parameters.
        /// </summary>
        /// <param name="context">
        /// A <see cref="ResourceContext"/> instance that represents the context
        /// of the current resource extraction.
        /// </param>
        /// <param name="name">
        /// A <see cref="string"/> object used to identify the requested resource.
        /// </param>
        /// <returns>
        /// A <see cref="Task{ResourceInfo}"/> instance representing the asynchronous operation for retrieving the resource. 
        /// </returns>
        /// <seealso cref="Extract"/>
        /// <seealso cref="ResourceContext"/>
        Task<Nullsafe<ResourceInfo>> ExtractAsync(ResourceContext context, string name);
    }
}