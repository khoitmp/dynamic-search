using DynamicSearch.Lib.Extension;
using DynamicSearch.Lib.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicSearch.Test
{
    [TestClass]
    public class CompilerTest
    {
        private static IQueryCompiler _compiler;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            var services = new ServiceCollection();
            services.AddDynamicSearch();
            _compiler = services.BuildServiceProvider().GetService<IQueryCompiler>();
        }

        [TestMethod]
        public void Equals_Test()
        {
        }

        [TestMethod]
        public void NotEquals_Test()
        {
        }

        [TestMethod]
        public void LessThan_Test()
        {
        }

        [TestMethod]
        public void LessThanOrEquals_Test()
        {
        }

        [TestMethod]
        public void GreaterThan_Test()
        {
        }

        [TestMethod]
        public void GreaterThanOrEquals_Test()
        {
        }

        [TestMethod]
        public void Contains_Test()
        {
        }

        [TestMethod]
        public void NotContains_Test()
        {
        }

        [TestMethod]
        public void StartsWith_Test()
        {
        }

        [TestMethod]
        public void NotStartsWith_Test()
        {
        }

        [TestMethod]
        public void EndWiths_Test()
        {
        }

        [TestMethod]
        public void NotEndsWith_Test()
        {
        }

        [TestMethod]
        public void In_Test()
        {
        }

        [TestMethod]
        public void NotIn_Test()
        {
        }
    }
}