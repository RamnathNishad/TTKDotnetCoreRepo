using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemos
{
    internal class Employee
    {
        public int Ecode { get; set; }
        public string Ename { get; set; }
        public int Salary { get; set; }
        public int Deptid { get; set; }

        public static List<Employee> GetEmps()
        {
            return new List<Employee>
            {
                new Employee {Ecode=101,Ename="Ravi",Salary=1111,Deptid=201},
                new Employee {Ecode=102,Ename="David",Salary=2222,Deptid=202},
                new Employee {Ecode=103,Ename="John",Salary=3333,Deptid=202},
                new Employee {Ecode=104,Ename="Tom",Salary=4444,Deptid=203},
                new Employee {Ecode=105,Ename="Harry",Salary=5555,Deptid=201}
            };
        }
    }
}
