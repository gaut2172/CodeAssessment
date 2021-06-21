namespace challenge.Models
{
    /*
     * Class to count number of people that report to an employee. Uses Employee.DirectReports attribute
     */
    public class ReportingStructure
    {
        public Employee Employee { get; set; }
        public int NumberOfReports { get; set; }
    }
}
