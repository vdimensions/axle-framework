﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Axle.Logging.Microsoft
{
    using IMSLogger = global::Microsoft.Extensions.Logging.ILogger;
    using IMSLoggerProvider = global::Microsoft.Extensions.Logging.ILoggerProvider;
    using IMSLoggerFactory = global::Microsoft.Extensions.Logging.ILoggerFactory;

    public sealed class Axle2MicrosoftLoggerFactory : IMSLoggerFactory
    {
        public void AddProvider(IMSLoggerProvider provider)
        {
            throw new NotImplementedException();
        }

        public IMSLogger CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
