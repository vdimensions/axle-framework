﻿using System;
using System.Security.Cryptography;
using System.Text;


namespace Axle.Security.Cryptography.Algorithms.Sdk
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [Serializable]
    #endif
    public abstract class AbstractEncryptionAlgorithm : IEncryptionAlgorithm, IDisposable
    {
        #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
        [NonSerialized]
        #endif
        private static readonly HexConverter _hex = new HexConverter();

        protected AbstractEncryptionAlgorithm() { }

        public abstract ICryptoTransform CreateEncryptor();
        public abstract ICryptoTransform CreateDecryptor();

        void IDisposable.Dispose() => Dispose(true);
        public void Dispose() => Dispose(true);
        protected virtual void Dispose(bool disposing) { }

        public virtual string Encrypt(string value, Encoding encoding)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }
            return value.Length == 0 ? string.Empty : ToString(Encrypt(encoding.GetBytes(value)));
        }
        public abstract byte[] Encrypt(byte[] value);

        public virtual string ToString(byte[] bytes)
        {
            var cb = Encrypt(bytes);
            return _hex.Convert(cb);
        }
    }
}