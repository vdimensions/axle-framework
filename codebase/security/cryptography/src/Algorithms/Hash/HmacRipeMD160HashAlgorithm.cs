#if NETFRAMEWORK
using System.Security.Cryptography;

using Axle.Security.Cryptography.Algorithms.Hash.Sdk;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    /// <summary>
    /// Computes a Hash-based Message Authentication Code (HMAC) using the RIPEMD160 hash function.
    /// </summary>
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class HmacRipeMD160HashAlgorithm : AbstractHashAlgorithm
    {
        public HmacRipeMD160HashAlgorithm() : base(new HMACRIPEMD160()) { }
    }
}
#endif