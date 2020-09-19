﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Axle.Resources.Properties.Extraction;
using Axle.Resources.Text.Documents;
using Axle.Text;
using Kajabity.Tools.Java;

namespace Axle.Resources.Properties
{
    /// <summary>
    /// A class representing a Java properties file as a resource.
    /// </summary>
    public class PropertiesResourceInfo : TextDocumentResourceInfo
    {
        /// <summary>
        /// Gets the content (MIME) type of a java properties file.
        /// </summary>
        public const string MimeType = "text/x-java-properties";

        /// <summary>
        /// Gets the file extension for a Java properties file.
        /// </summary>
        public const string FileExtension = ".properties";

        internal PropertiesResourceInfo(
                string name, 
                CultureInfo culture, 
                IDictionary<string, CharSequence> data) 
            : base(name, culture, MimeType, data) { }
        internal PropertiesResourceInfo(
                string name, 
                CultureInfo culture, 
                IDictionary<string, CharSequence> data, 
                ResourceInfo originalResource) 
            : base(name, culture, MimeType, data, originalResource) { }

        /// <summary>
        /// Opens a <see cref="Stream">stream</see> object for reading the contents of the properties file.
        /// </summary>
        /// <returns>
        /// A <see cref="Stream">stream</see> object for reading the contents of the properties file.
        /// </returns>
        public override Stream Open()
        {
            // ReSharper disable EmptyGeneralCatchClause
            try
            {
                return base.Open();
            }
            catch { }
            // ReSharper restore EmptyGeneralCatchClause

            var result = new MemoryStream();
            // TODO: add support for char[]
            new JavaPropertyWriter(
                Data.ToDictionary(
                    x => x.Key,
                    x => x.Value.ToString(),
                    PropertiesFileExtractor.DefaultKeyComparer)
                ).Write(result, null);
            result.Seek(0, SeekOrigin.Begin);
            return result;
        }

        /// <inheritdoc />
        public override bool TryResolve(Type type, out object result)
        {
            // TODO: implement java properties (de)serializer and use it here as well.

            return base.TryResolve(type, out result);
        }
    }
}