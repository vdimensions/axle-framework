﻿#if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
using System;

namespace Axle.DependencyInjection.Sdk
{
    partial class AbstractDependencyContainer : IDependencyRegistry
    {
        IDependencyRegistry IDependencyRegistry.RegisterType(Type type, string name, params string[] aliases)
        {
            return RegisterType(type, name, aliases);
        }
    }
}
#endif