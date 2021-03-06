using System.ComponentModel;
using System.Linq;

using Axle.Reflection;
using Axle.Verification;


namespace Axle.DependencyInjection.Descriptors
{
    public class PropertyDependencyDescriptor : IPropertyDependencyDescriptor
    {
        public PropertyDependencyDescriptor(IProperty property, string dependencyName)
        {
            Member = property.VerifyArgument(nameof(property)).IsNotNull().Value;
            Info = new DependencyInfo(
                property.MemberType,
                dependencyName.VerifyArgument(nameof(dependencyName)).IsNotNull(), 
                property.Name);
            var defaultValueAttribute = Member.GetAttributes<DefaultValueAttribute>().Select(x => x.Attribute).OfType<DefaultValueAttribute>().SingleOrDefault();
            DefaultValue = defaultValueAttribute?.Value;
        }

        public void SetValue(object target, object value)
        {
            ((IWriteableMember) Member).SetAccessor.SetValue(target, value);
        }


        public IProperty Member { get; }
        public DependencyInfo Info { get; }
        public object DefaultValue { get; }
    }
}