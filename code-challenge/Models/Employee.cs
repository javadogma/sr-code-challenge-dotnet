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

            reportingStructure.Employee = this;
            reportingStructure.NumberOfReports = CountReports();

            return reportingStructure;
        }

        // There is probably another way to do this that leverages Linq, similar to how
        // the employee database is seeded. Until I get to that, recursively walk through
        // the employees.
        protected int CountReports()
        {
            int count = 0;

            foreach (Employee emp in DirectReports)
            {
                if (emp.DirectReports != null)
                {
                    // NDB: This check is redundant. If DirectReports isn't null then the count
                    // should always be > 0. Pain the ass right now becuase of the entity problems.
                    // when that's solved take out the comments.
                    if(emp.DirectReports.Count > 0)
                    {
                        count += emp.DirectReports.Count;
                        count += emp.CountReports();
                    }
                //    else
                //    {
                //        count = 0;
                //    }
                }
                else
                {
                    count = 1;
                }
            }

            return count;
        }
    }
}
