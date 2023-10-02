namespace MyFirstMVCoreApp.Models
{
    public interface IEmpDataAccess
    {
        List<Employee> GetAllEmps();
        Employee GetEmpById(int id);
        void AddEmployee(Employee emp);
        void DeleteEmployee(int ecode);
        void UpdateEmployee(Employee emp);
    }

    public class EmpDataAccess : IEmpDataAccess
    {

        EmpDbContext dbCtx;
        public EmpDataAccess(EmpDbContext dbCtx)
        {
            this.dbCtx = dbCtx;
        }
        public void AddEmployee(Employee emp)
        {
            dbCtx.Employee.Add(emp);
            dbCtx.SaveChanges();
        }

        public void DeleteEmployee(int ecode)
        {
            var record = dbCtx.Employee.Find(ecode);            
            dbCtx.Employee.Remove(record);
            dbCtx.SaveChanges();
        }

        public List<Employee> GetAllEmps()
        {
            var lstEmps = dbCtx.Employee.ToList();
            return lstEmps;
        }

        public Employee GetEmpById(int id)
        {
            var record=dbCtx.Employee.Find(id);            
            //OR
            //var record = dbCtx.Employee.Where(o => o.Ecode == id)
            //                           .SingleOrDefault();
            //OR
            //record = (from e in dbCtx.Employee
            //         where e.Ecode==id
            //         select e).SingleOrDefault();
            return record;
        }

        public void UpdateEmployee(Employee emp)
        {
            //find the record
            var record = dbCtx.Employee.Find(emp.Ecode);
            //update the values
            record.Ename = emp.Ename;
            record.Salary = emp.Salary;
            record.Deptid = emp.Deptid;
            //save to DB
            dbCtx.SaveChanges();
        }
    }
}
