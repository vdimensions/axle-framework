using System;

using Axle.Verification;


namespace Axle.DependencyInjection
{
    public static class ContainerMixin
    {
        public static IContainer RegisterInstance(this IContainer container, object instance)
        {
            return container.VerifyArgument(nameof(container)).IsNotNull().Value.RegisterInstance(instance, string.Empty);
        }
        public static IContainer RegisterInstance<T>(this IContainer container, T instance, string name, params string[] aliases)
        {
            return container.VerifyArgument(nameof(container)).IsNotNull().Value.RegisterInstance(instance, name, aliases);
        }
        public static IContainer RegisterInstance<T>(this IContainer container, T instance)
        {
            return container.VerifyArgument(nameof(container)).IsNotNull().Value.RegisterInstance(instance, string.Empty);
        }

        public static IContainer RegisterType(this IContainer container, Type type)
        {
            return container.VerifyArgument(nameof(container)).IsNotNull().Value.RegisterType(type, string.Empty);
        }
        public static IContainer RegisterType<T>(this IContainer container, string name, params string[] aliases)
        {
            return container.VerifyArgument(nameof(container)).IsNotNull().Value.RegisterType(typeof(T), name, aliases);
        }
        public static IContainer RegisterType<T>(this IContainer container)
        {
            return container.VerifyArgument(nameof(container)).IsNotNull().Value.RegisterType(typeof(T), string.Empty);
        }

        public static object Resolve(this IContainer container, Type type)
        {
            return container.VerifyArgument(nameof(container)).IsNotNull().Value.Resolve(type, string.Empty);
        }
        public static T Resolve<T>(this IContainer container, string name)
        {
            return (T) container.VerifyArgument(nameof(container)).IsNotNull().Value.Resolve(typeof(T), name.VerifyArgument(nameof(name)).IsNotNull().Value);
        }
        public static T Resolve<T>(this IContainer container)
        {
            return Resolve<T>(container, string.Empty);
        }

        public static bool TryResolve(this IContainer container, Type type, out object value, string name)
        {
            try
            {
                value = container.VerifyArgument(nameof(container)).IsNotNull().Value.Resolve(type, name);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }
        public static bool TryResolve(this IContainer container, Type type, out object value)
        {
            try
            {
                value = container.VerifyArgument(nameof(container)).IsNotNull().Value.Resolve(type);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }
        public static bool TryResolve<T>(this IContainer container, out T value, string name)
        {
            try
            {
                value = container.VerifyArgument(nameof(container)).IsNotNull().Value.Resolve<T>(name);
                return true;
            }
            catch
            {
                value = default(T);
                return false;
            }
        }
        public static bool TryResolve<T>(this IContainer container, out T value)
        {
            try
            {
                value = container.VerifyArgument(nameof(container)).IsNotNull().Value.Resolve<T>();
                return true;
            }
            catch
            {
                value = default(T);
                return false;
            }
        }
    }
}
