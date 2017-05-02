﻿using System.Diagnostics;

using Axle.References;


namespace Axle.Environment
{
    public static class Platform
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly IEnvironment _env = Singleton<EnvironmentInfo>.Instance.Value;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly IRuntime _runtime = Singleton<RuntimeInfo>.Instance.Value;

        static Platform() { }

        public static IEnvironment Environment { get { return _env; } }
        public static IRuntime Runtime { get { return _runtime; } }
    }
}
