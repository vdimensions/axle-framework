﻿using System;
using System.Runtime.Serialization;


namespace Axle.Resources.Marshalling
{
    [Serializable]
    public partial class ResourceMarshallingException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceMarshallingException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown. 
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains contextual information about the source or destination. 
        /// </param>
        protected ResourceMarshallingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}