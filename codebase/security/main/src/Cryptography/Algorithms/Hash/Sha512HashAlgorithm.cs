using System.Security.Cryptography;


namespace Axle.Security.Cryptography.Algorithms.Hash
{
    #if NETSTANDARD2_0_OR_NEWER || NETFRAMEWORK
    [System.Serializable]
    #endif
    public sealed class Sha512HashAlgorithm : AbstractHashAlgorithm
    {
        public Sha512HashAlgorithm() : base(SHA512.Create()) { }
    }
}