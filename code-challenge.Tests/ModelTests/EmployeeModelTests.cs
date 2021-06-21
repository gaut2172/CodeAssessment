using challenge.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace code_challenge.Tests.Integration.ModelTests
{
    [TestClass]
    class EmployeeModelTests
    { 
        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
        }

        [TestMethod]
        // Tests if custom constructor works as intended
        public void CreateEmployee_Returns_Created()
        {
            Employee emp1 = new Employee("123", "Josh", "Gauthier", "Software Engineer", "IT");
            emp1.DirectReports.Add("54321");

            // Assert
            Assert.AreEqual(emp1.EmployeeId, "123");
            Assert.AreEqual(emp1.FirstName, "Josh");
            Assert.AreEqual(emp1.LastName, "Gauthier");
            Assert.AreEqual(emp1.Department, "Software Engineer");
            Assert.AreEqual(emp1.Position, "IT");
            Assert.IsTrue(emp1.DirectReports.Count == 1);
            Assert.AreEqual(emp1.DirectReports[0], "54321");
        }
    }
}
