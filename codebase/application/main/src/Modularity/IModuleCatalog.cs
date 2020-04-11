﻿using System;
using System.Reflection;


namespace Axle.Modularity
{
    public interface IModuleCatalog
    {
        Type[] DiscoverModuleTypes(Assembly assembly);

        Type GetRequiredApplicationHostType(Type moduleType);
        Type[] GetRequiredModules(Type moduleType);
        ModuleMethod GetInitMethod(Type moduleType);
        ModuleEntryMethod GetEntryPointMethod(Type moduleType);
        ModuleMethod GetTerminateMethod(Type moduleType);
        ModuleCallback[] GetDependencyInitializedMethods(Type moduleType);
        ModuleCallback[] GetDependencyTerminatedMethods(Type moduleType);
        UtilizesAttribute[] GetUtilizedModules(Type moduleType);
        ReportsToAttribute[] GetReportsToModules(Type moduleType);
        ModuleCommandLineTriggerAttribute GetCommandLineTrigger(Type moduleType);
        ModuleConfigSectionAttribute GetConfigurationInfo(Type moduleType);
    }
}