using System.Security.Cryptography;
using Axle.Security.Cryptography.Algorithms.Hash.Sdk;

namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class Sha1NativeHashAlgorithm : AbstractHashAlgorithm
    {
        public Sha1NativeHashAlgorithm() : base(new SHA1CryptoServiceProvider()) { }
    }
}