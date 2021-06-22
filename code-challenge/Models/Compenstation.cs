using System;
using System.ComponentModel.DataAnnotations;

namespace challenge.Models
{
    /*
     * Class to salary and start date for employees
     */
    public class Compensation
    {
        [Key]
        public string CompensationId { get; set; }
        public string Employee { get; set; }
        public decimal Salary { get; set; }
        public DateTime EffectiveDate { get; set; }

        public Compensation() { }

        public Compensation(
            string employeeId,
            decimal salary,
            DateTime effectiveDate)
        {
            Employee = employeeId;
            Salary = salary;
            EffectiveDate = effectiveDate;
        }

    }
}
