using System;
using System.Collections.Generic;

namespace challenge.Models
{
    public class Employee
    {
        public String EmployeeId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Position { get; set; }
        public string Department { get; set; }

        // Changed type to string to fix bug. EF Core required custom mapping to process List type, and string was straightforward
        public List<String> DirectReports { get; set; }

        public Employee() { }

        // Constructor used for seed data
        public Employee(String id, String firstName, String lastName, String pos, String dep)
        {
            EmployeeId = id;
            FirstName = firstName;
            LastName = lastName;
            Position = pos;
            Department = dep;
            DirectReports = new List<string>();
        }
    }
}
