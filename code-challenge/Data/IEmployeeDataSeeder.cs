using System.Collections.Generic;
using System.Threading.Tasks;
using challenge.Models;

namespace challenge.Data
{
    public interface IEmployeeDataSeeder
    {
        int Seed() ;
        List<Employee> LoadEmployees();
        void FixUpReferences(List<Employee> employees);
    }
}
