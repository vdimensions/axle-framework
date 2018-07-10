﻿using System;


namespace Axle.Modularity
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ModuleDependencyTerminatedAttribute : ModuleDependencyCallbackAttribute
    {
    }
}