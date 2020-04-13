using System.Collections.Generic;
using System.Linq;
using Axle.DependencyInjection;
using NUnit.Framework;

namespace Axle.Data.SqlClient.Tests
{
    public class ModularityTests
    {
        [Test]
        public void TestSqlClientProviderIsRegistered()
        {
            IDependencyContainer dependencyContainer = null;
            using (Application.Build().ConfigureDependencies(c => dependencyContainer = c).UseSqlClient().Run())
            {
                var providers = dependencyContainer.Resolve<IEnumerable<IDbServiceProvider>>().ToArray();
                Assert.IsNotEmpty(providers, "No database service providers have been registered");
                Assert.True(providers.Length == 1, "Only one database service provider is expected.");
                Assert.AreEqual(providers[0].ProviderName, SqlServiceProvider.Name);
            }
        }
    }
}