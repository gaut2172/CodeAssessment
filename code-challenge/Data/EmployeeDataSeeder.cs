using challenge.Models;
using Newtonsoft.Json;
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

        private List<Employee> LoadEmployees()
        {
            using (FileStream fs = new FileStream(EMPLOYEE_SEED_DATA_FILE, FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            using (JsonReader jr = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();

                List<Employee> employees = serializer.Deserialize<List<Employee>>(jr);
                FixUpReferences(employees);

                return employees;
            }
        }

        // Fixes Employee.DirectReports attribute for the given list of Employee objects
        // param - employees - the list of Employees that was just deserialized from seed data
        private void FixUpReferences(List<Employee> employees)
        {
            var employeeIdRefMap = from employee in employees
                                select new { Id = employee.EmployeeId, EmployeeRef = employee };

            
            // for each employee in the seed data
            employees.ForEach(employee =>
            {
                if (employee.DirectReports != null)
                {
                    Console.WriteLine("Heloooooo");

                    // create list to hold current employee's reporters
                    var referencedEmployees = new List<Employee>(employee.DirectReports.Count);

                    // for each person that reports to current employee
                    employee.DirectReports.ForEach(currReporter =>
                    {
                        
                        var referencedEmployee = employeeIdRefMap.First(e => e.Id == currReporter.EmployeeId).EmployeeRef;
                        if (referencedEmployee != null)
                        {
                            referencedEmployees.Add(referencedEmployee);
                        }
                    });
                    employee.DirectReports = referencedEmployees;
                    for (int i = 0; i < employee.DirectReports.Count; i++)
                    {
                        Console.WriteLine(employee.FirstName);
                        Console.WriteLine(employee.DirectReports[i].EmployeeId);
                    }
                }
            });
        }
    }
}
