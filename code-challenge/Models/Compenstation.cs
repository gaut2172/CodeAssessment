using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    /*
     * Class to salary and start date for employees
     */
    public class Compensation
    {
        public string Employee { get; set; }
        public decimal Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
