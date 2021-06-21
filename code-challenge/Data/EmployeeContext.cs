using challenge.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Data
{
    public class EmployeeContext : DbContext
    {
        // Overriding method that deals with the Employee.DirectReports attribute serialization. 
        // Allows List<string> attribute in Employee class when using EF Core database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // the HasConversion() method required a later version of EF Core
            modelBuilder.Entity<Employee>().Property(p => p.DirectReports)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v));
        }

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
