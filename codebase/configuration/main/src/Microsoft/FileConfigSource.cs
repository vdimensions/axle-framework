﻿#if NETSTANDARD2_0_OR_NEWER || NET461_OR_NEWER
using Axle.Configuration.Microsoft.Adapters;
using Axle.Verification;

namespace Axle.Configuration.Microsoft
{
    using MSFileConfigurationSource = global::Microsoft.Extensions.Configuration.FileConfigurationSource;

    public sealed class FileConfigSource : IConfigSource
    {
        private readonly MSFileConfigurationSource _source;

        public FileConfigSource(MSFileConfigurationSource source)
        {
            _source = source.VerifyArgument(nameof(source)).IsNotNull();
        }

        public IConfiguration LoadConfiguration()
        {
            return Microsoft2AxleConfigRootAdapter.FromConfigurationSource(_source);
        }
    }
}
#endif