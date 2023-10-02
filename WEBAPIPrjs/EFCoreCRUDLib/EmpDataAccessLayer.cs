using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreCRUDLib
{
    public class EmpDataAccessLayer : IEmpDataAccess
    {
        private readonly EmpDbContext _dbCtx;
        public EmpDataAccessLayer(EmpDbContext dbCtx)
        {
            this._dbCtx = dbCtx;
        }
        public void AddEmployee(Employee emp)
        {
            _dbCtx.Employee.Add(emp);
            _dbCtx.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            var record=_dbCtx.Employee.Find(id);
            if (record != null)
            {
                _dbCtx.Employee.Remove(record);
                _dbCtx.SaveChanges();
            }
        }

        public Employee GetEmpById(int empId)
        {
            var record = _dbCtx.Employee.Find(empId);
            if (record != null)
            {
                return record;
            }
            else
            {
                return null;
            }
        }

        public List<Employee> GetEmps()
        {
            var lstEmps = _dbCtx.Employee.ToList();
            return lstEmps;
        }

        public void UpdateEmployee(Employee emp)
        {
            var record = _dbCtx.Employee.Find(emp.Ecode);
            if (record != null)
            {
                record.Ename=emp.Ename; ;
                record.Salary = emp.Salary;
                record.Deptid=emp.Deptid;
                _dbCtx.SaveChanges();
            }
        }
    }
}
