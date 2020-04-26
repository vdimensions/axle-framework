﻿using System;
using Axle.Configuration;
using Axle.DependencyInjection;
using Axle.Logging;
using Axle.References;

namespace Axle.Web.AspNetCore
{
    /// <summary>
    /// The <see cref="IApplicationHost"/> implementation designed for use with aspnetcore.
    /// </summary>
    public sealed class AspNetCoreApplicationHost : IApplicationHost
    {
        public static AspNetCoreApplicationHost Instance => Singleton<AspNetCoreApplicationHost>.Instance.Value;
        
        private readonly string _environmentName;
        private readonly ILoggingService _loggingService;
        private readonly IDependencyContainerFactory _dependencyContainerFactory;
        private readonly IConfiguration _configuration;
        private readonly string[] _logo;
        
        private AspNetCoreApplicationHost()
        {
            var defaultAppHost = DefaultApplicationHost.Instance;
            
            // TODO: use delegating factory that will also duplicate dependencies in ASPNET DI containers
            _dependencyContainerFactory = defaultAppHost.DependencyContainerFactory;
            
            _loggingService = defaultAppHost.LoggingService;
            
            _environmentName = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.Process);
            if (string.IsNullOrEmpty(_environmentName))
            {
                _environmentName = defaultAppHost.EnvironmentName; 
            }

            _configuration = defaultAppHost.Configuration;
            _logo = defaultAppHost.Logo;
        }

        IDependencyContainerFactory IApplicationHost.DependencyContainerFactory => _dependencyContainerFactory;
        ILoggingService IApplicationHost.LoggingService => _loggingService;
        string IApplicationHost.EnvironmentName => _environmentName;
        IConfiguration IApplicationHost.Configuration => _configuration;
        string[] IApplicationHost.Logo => _logo;
    }
}