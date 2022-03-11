using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class Employee
    {
        public String EmployeeId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Position { get; set; }
        public String Department { get; set; }
        public List<Employee> DirectReports { get; set; }

        public ReportingStructure GetReportingStructure() 
        {
            ReportingStructure reportingStructure = new ReportingStructure();

            // TODO: calculate this employee's direct reports
            reportingStructure.Employee = this;
            reportingStructure.NumberOfReports = 277;

            return reportingStructure;
        }
    }
}
