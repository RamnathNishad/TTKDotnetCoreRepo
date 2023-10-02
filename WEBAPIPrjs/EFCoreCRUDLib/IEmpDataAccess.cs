using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreCRUDLib
{
    public  interface IEmpDataAccess
    {
        List<Employee> GetEmps();
        Employee GetEmpById(int empId);

        void AddEmployee(Employee emp);
        void DeleteEmployee(int id);
        void UpdateEmployee(Employee emp);
    }
}
