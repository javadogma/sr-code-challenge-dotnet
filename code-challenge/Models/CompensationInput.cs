using System;

namespace challenge.Models
{
    /// <summary>
    /// This is a little cheesy. There's some duplication of effort here with
    /// the Compensation model. 
    /// </summary>
    public class CompensationInput
    {
        private string id;
        private decimal _salary;
        private DateTime _effectiveDate;

        public CompensationInput()
        {
        }

        public string Id { get { return id; } set { id = value; } }
        public decimal Salary { get { return _salary; } set { _salary = value; } }
        public DateTime EffectiveDate { get { return _effectiveDate; } set { _effectiveDate = value; } }
    }
}
