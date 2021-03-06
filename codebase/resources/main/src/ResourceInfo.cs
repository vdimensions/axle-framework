﻿using System;
using System.Globalization;
using System.IO;

using Axle.IO.Serialization;

//using System.Net.Mime;

using Axle.Verification;


namespace Axle.Resources
{
    /// <summary>
    /// An abstract class representing a resource object. 
    /// </summary>
    public abstract class ResourceInfo
    {
        /// <summary>
        /// Creates a new instance of the current <see cref="ResourceInfo"/> implementation.
        /// </summary>
        /// <param name="name">
        /// The unique name of the resource within the current resource bundle. 
        /// </param>
        /// <param name="culture">
        /// The <see cref="CultureInfo"/> for which the resource was requested. 
        /// </param>
        /// <param name="contentType">
        /// A content type header describing the resource's MIME type.
        /// </param>
        //protected ResourceInfo(Uri location, CultureInfo culture, ContentType contentType)
        protected ResourceInfo(string name, CultureInfo culture, string contentType)
        {
            Name = name.VerifyArgument(nameof(name)).IsNotNullOrEmpty();
            Culture = culture.VerifyArgument(nameof(culture)).IsNotNull();
            ContentType = contentType.VerifyArgument(nameof(contentType)).IsNotNull();
        }
        //protected ResourceInfo(Uri location, CultureInfo culture, string contentTypeName)
        //    : this(location, culture, new ContentType(contentTypeName.VerifyArgument(nameof(contentTypeName)).IsNotNullOrEmpty())) { }

        /// <summary>
        /// Opens a new <see cref="Stream"/> to read the current <see cref="ResourceInfo"/> implementation data.
        /// </summary>
        /// <returns>
        /// A new <see cref="Stream"/> instance to read the resource's data.
        /// </returns>
        /// <exception cref="ResourceNotFoundException">
        /// Thrown if the represented resource can no longer be located. 
        /// For example, if the current <see cref="ResourceInfo"/> instance 
        /// represents a file, which is deleted at some point, then this 
        /// exception will be thrown if the resource is requested after the deletion.
        /// </exception>
        /// <exception cref="ResourceLoadException">
        /// Thrown if an error occurs while loading the resource stream.
        /// </exception>
        public abstract Stream Open();

        /// <summary>
        /// Returns an object instance that is represented by the current <see cref="ResourceInfo"/> implementation.
        /// </summary>
        /// <returns>
        /// An object instance that is represented by the current <see cref="ResourceInfo"/> implementation.
        /// </returns>
        public object Resolve(Type targetType)
        {
            if (targetType == null)
            {
                throw new ArgumentNullException(nameof(targetType));
            }

            if (TryResolve(targetType, out var instance))
            {
                return instance;
            }

            throw new NotSupportedException($"Cannot convert the current resource representation to an instance of {targetType?.AssemblyQualifiedName}");
        }

        public T Resolve<T>() => (T) Resolve(typeof(T));

        public virtual bool TryResolve(Type targetType, out object result)
        {
            if (targetType == typeof(Stream))
            {
                result = Open();
                return true;
            }

            if (targetType == typeof(BinaryReader))
            {
                result = new BinaryReader(Open());
                return true;
            }

            if (targetType == typeof(TextReader) || targetType == typeof(StreamReader))
            {
                result = new StreamReader(Open());
                return true;
            }

            #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
            var serializer = new BinarySerializer();
            try
            {
                using (var stream = Open())
                {
                    result = serializer.Deserialize(stream, targetType);
                }
                return true;
            }
            catch { }
            #endif

            result = null;
            return false;
        }

        public bool TryResolve<T>(out T result)
        {
            if (TryResolve(typeof(T), out var r))
            {
                result = (T) r;
                return true;
            }

            result = default(T);
            return false;
        }

        /// <summary>
        /// Gets a <see cref="string"/> value that indicates the resource bundle containing the current resource object.
        /// </summary>
        public string Bundle { get; internal set; }

        /// <summary>
        /// Gets a <see cref="string"/> value that uniquely identifies the resource within its bundle.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A <see cref="CultureInfo"/> object representing the culture this <see cref="ResourceInfo"/> instance is defined for.
        /// <remarks>
        /// This property returns the actual <see cref="CultureInfo"/> of the resource, regardless if it has been requested with a more-specific culture.
        /// </remarks>
        /// </summary>
        public CultureInfo Culture { get; }

        ///// <summary>
        ///// Gets the <see cref="System.Net.Mime.ContentType"/> header describing the represented resource.
        ///// </summary>
        ///// <seealso cref="System.Net.Mime.ContentType"/>
        //public ContentType ContentType { get; }
        /// <summary>
        /// Gets the content type header describing the represented resource.
        /// </summary>
        public string ContentType { get; }
    }
}