using System;
using System.Linq;
using challenge.Data;

namespace challenge.Models
{
    public class Compensation
    {
        private readonly Employee _employee;
        private decimal _salary;
        private DateTime _effectiveDate;

        public Compensation(Employee employee)
        {
            _employee = employee;
            _salary = employee.Salary;
            _effectiveDate = employee.EffectiveDate;
        }
    }
}
