using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Net;
using System.Net.Http;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void GetReportingStructure_Returns_Ok()
        {
            // Arrange
            string employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            string expectedReporters = "4";

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reporting/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var jsonAsString = response.Content.ReadAsStringAsync();
            Assert.AreEqual(expectedReporters, jsonAsString.Result);
        }
    }
}
