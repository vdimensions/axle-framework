using System.Collections.Generic;
using System.Linq;
using Axle.DependencyInjection;
using NUnit.Framework;

namespace Axle.Data.MySql.Tests
{
    public class Tests
    {
        [Test]
        public void TestMySqlProviderIsRegistered()
        {
            IContainer container = null;
            using (Application.Build().ConfigureDependencies(c => container = c).LoadMySqlModule().Run())
            {
                var providers = container.Resolve<IEnumerable<IDbServiceProvider>>().ToArray();
                Assert.IsNotEmpty(providers, "No database service providers have been registered");
                Assert.True(providers.Length == 1, "Only one database service provider is expected.");
                Assert.AreEqual(providers[0].ProviderName, MySqlServiceProvider.Name);
            }
        }
    }
}