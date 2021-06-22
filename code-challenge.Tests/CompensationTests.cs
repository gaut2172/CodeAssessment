using challenge.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using code_challenge.Tests.Integration.Extensions;

using System;
using System.Net;
using System.Net.Http;
using code_challenge.Tests.Integration.Helpers;
using System.Text;
using System.Globalization;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class CompensationTests
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
        public void CreateCompensation_Returns_Created()
        {
            // Set up
            string expectedEmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            string expectedSalary = "85000.00";
            string expectedEffectiveDate = "2009-06-15T13:45:30";
            decimal salary = Convert.ToDecimal(expectedSalary);
            DateTime effectiveDate = Convert.ToDateTime(expectedEffectiveDate);

            // Arrange
            var compensation = new Compensation()
            {
                Employee = expectedEmployeeId,
                Salary = salary,
                EffectiveDate = effectiveDate
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(expectedEmployeeId, newCompensation.Employee);
            Assert.AreEqual(salary, newCompensation.Salary);
            Assert.AreEqual(effectiveDate, newCompensation.EffectiveDate);
        }

        [TestMethod]
        public void GetCompensationById_Returns_Ok()
        {
            // Arrange
            string employeeId = "b7839309-3348-463b-a7e3-5de1c168beb3";
            string salary = "80000.00";
            string effectiveDateString = "2015-09-25T11:54:49.0000000";
            DateTime effectiveDate = Convert.ToDateTime(effectiveDateString);

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Compensation compensation = response.DeserializeContent<Compensation>();
            string salaryResult = compensation.Salary.ToString("0.##");
            Assert.AreEqual(salary, compensation.Salary.ToString("F"));
            string dateResult = compensation.EffectiveDate.ToString("O", DateTimeFormatInfo.InvariantInfo);
            Assert.AreEqual(effectiveDateString, dateResult);
        }
    }
}