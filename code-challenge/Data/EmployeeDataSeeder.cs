using challenge.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Data
{
    public class EmployeeDataSeeder
    {
        private EmployeeContext _employeeContext;
        private const String EMPLOYEE_SEED_DATA_FILE = "resources/EmployeeSeedData.json";

        public EmployeeDataSeeder(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public async Task Seed()
        {
            if(!_employeeContext.Employees.Any())
            {
                List<Employee> employees = LoadEmployees();
                _employeeContext.Employees.AddRange(employees);

                await _employeeContext.SaveChangesAsync();
            }
        }

        /*
         * Function to parse the JSON seed data into a list of Employee objects 
         */
        private List<Employee> LoadEmployees()
        {
            {
                // Use Linq method to parse JSON seed data into a JObject
                // I needed to edit the EmployeeSeedData.json file so that it was compatible
                JObject jObject = JObject.Parse(File.ReadAllText(EMPLOYEE_SEED_DATA_FILE));
                
                List<Employee> employees = new List<Employee>();

                // for each employee in the JObject
                foreach (var currEmployee in jObject["employees"])
                {
                    // create new C# Employee instance
                    Employee newEmployee = new Employee(
                        (string)currEmployee["employeeId"],
                        (string)currEmployee["firstName"],
                        (string)currEmployee["lastName"],
                        (string)currEmployee["position"],
                        (string)currEmployee["department"]);

                    // if current employee has data in directReports property...
                    if (currEmployee["directReports"] != null)
                    {
                        List<string> reporters = new List<string>();
                        
                        // add all employeeIds that report to current employee
                        foreach (var currReporter in currEmployee["directReports"])
                        {
                            newEmployee.DirectReports.Add((string)currReporter["employeeId"]);
                        }
                    }

                    employees.Add(newEmployee);
                }

                return employees;
            }
        }
    }
}
