namespace LinqDemos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //get the records 
            var lstEmps=Employee.GetEmps();
            var lstDepts=Department.GetDepts();

            //Q. Find the employees whose salary > 2000
            //SQL: select * from employee where salary>2000
            //LINQ:
            var res1 = from e in lstEmps
                       where e.Salary>2000
                       select e;
            //OR using extension method
            res1 = lstEmps.Where(e => e.Salary > 2000);

            //display the result
            foreach (var item in res1)
            {
                //Console.WriteLine($"{item.Ecode}\t{item.Ename}\t{item.Salary}\t{item.Deptid}");
            }

            //Q.Get the employees order by salary in descending order
            //SQL: select * from employee order by salary desc
            var res2 = from e in lstEmps
                       orderby e.Salary ascending
                       select e;

            //OR using extenstion method
            res2=lstEmps.OrderBy(e => e.Salary);


            //display the result
            foreach (var item in res2)
            {
                //Console.WriteLine($"{item.Ecode}\t{item.Ename}\t{item.Salary}\t{item.Deptid}");
            }

            //retrieve only few column like ecode and ename only
            //SQL: select ecode,ename from employee
            //Linq:
            var res3 = from e in lstEmps
                       select new 
                       { 
                           e.Ecode, 
                           e.Salary,
                           Bonus=0.1*e.Salary
                       };
            //using extension method
            res3 = lstEmps.Select(e => new
            {
                e.Ecode,
                e.Salary,
                Bonus = 0.1 * e.Salary
            });

            //display the result
            foreach (var item in res3)
            {
                //Console.WriteLine($"{item.Ecode}\t{item.Salary}\t{item.Bonus}");
            }

            //Group result
            //SQL: select sum(salary), avg(salary), max(salary),min(salary),count(salary) from employee
            //LINQ:
            var res4 = new
            {
                TotalSalary=lstEmps.Sum(e=>e.Salary),
                MaxSalary=lstEmps.Max(e => e.Salary),
                MinSalary=lstEmps.Min(e => e.Salary),
                AvgSalary=lstEmps.Average(e => e.Salary),
                NoOfEmps=lstEmps.Count()
            };
            Console.WriteLine("Total Salary:" + res4.TotalSalary);
            Console.WriteLine("Max Salary:" + res4.MaxSalary);
            Console.WriteLine("Min Salary:" + res4.MinSalary);
            Console.WriteLine("Avg Salary:" + res4.AvgSalary);
            Console.WriteLine("No. of Salary:" + res4.NoOfEmps);

            //Group by query
            //SQL:
            //select deptid,sum(salary),avg(salary),max(salary),min(salary),count(salary)
            //from employee 
            //group by deptid

            //LINQ:
            var res5 = from e in lstEmps
                       group e by e.Deptid into g
                       select new
                       {
                           Deptid=g.Key,
                           TotalSalary = g.Sum(e => e.Salary),
                           MaxSalary = g.Max(e => e.Salary),
                           MinSalary = g.Min(e => e.Salary),
                           AvgSalary = g.Average(e => e.Salary),
                           NoOfEmps = g.Count()
                       };

            //using extension method and lambda
            res5 = lstEmps.GroupBy(e => e.Deptid).Select(g => new
            {
                Deptid = g.Key,
                TotalSalary = g.Sum(e => e.Salary),
                MaxSalary = g.Max(e => e.Salary),
                MinSalary = g.Min(e => e.Salary),
                AvgSalary = g.Average(e => e.Salary),
                NoOfEmps = g.Count()
            });//.Where(o=>o.TotalSalary>5000);

            //display the result
            foreach (var r in res5)
            {
                //Console.WriteLine($"{r.Deptid}\t{r.TotalSalary}\t{r.MaxSalary}\t{r.MinSalary}\t{r.AvgSalary}\t{r.NoOfEmps}");
            }

            //Find the employees whose name starts with "R"
            //SQL: select * from employee where ename like '%R%'
            //LINQ:
            var res6 = from e in lstEmps
                       where e.Ename.ToUpper().Contains("R")
                       select e;

            //using extension
            res6 = lstEmps.Where(e => e.Ename.ToUpper().Contains("R"));

            foreach (var item in res6)
            {
                //Console.WriteLine($"{item.Ecode}\t{item.Ename}\t{item.Salary}\t{item.Deptid}");
            }

            //get the first 2 employee records sorted by salary ascending
            var res7 = (from e in lstEmps
                        orderby e.Salary descending
                        select e).Take(2);

            foreach (var item in res7)
            {
                //Console.WriteLine($"{item.Ecode}\t{item.Ename}\t{item.Salary}\t{item.Deptid}");
            }

            //Join query:
            //SQL:
            //select e.ecode,e.ename,e.salary,d.deptid,d.dname,d.dhead
            //from employee e,department d 
            //where d.deptid=e.deptid

            //LINQ:
            var res8 = from e in lstEmps
                       join d in lstDepts on e.Deptid equals d.Deptid
                       select new
                       {
                           e.Ecode,
                           e.Ename,
                           e.Salary,
                           d.Deptid,
                           d.Dname,
                           d.Dhead
                       };

            //using extension method
            res8 = lstEmps.Join(lstDepts,
                                e => e.Deptid,
                                d => d.Deptid,
                                (e, d) => new
                                {
                                    e.Ecode,
                                    e.Ename,
                                    e.Salary,
                                    d.Deptid,
                                    d.Dname,
                                    d.Dhead
                                });

            //display the result
            foreach (var item in res8)
            {
                //Console.WriteLine($"{item.Ecode}\t{item.Ename}\t{item.Salary}\t{item.Deptid}\t{item.Dname}\t{item.Dhead}");
            }

            //Nested query:
            //Q. find the employees working in the deptid of employee whose ecode is 101
            //SQL:
            //select * 
            //from employee
            //where deptid=(select deptid from employee where ecode=101)

            //LINQ:
            var res9 = from e in lstEmps
                       where e.Deptid==(from p in lstEmps 
                                       where p.Ecode==101 
                                       select p.Deptid).SingleOrDefault()
                       select e;

            //using extension method
            res9 = lstEmps.Where(e => e.Deptid == (lstEmps.Where(p => p.Ecode == 101)
                                                          .Select(o => o.Deptid)
                                                          .SingleOrDefault()));


            //display the result
            foreach (var item in res9)
            {
                Console.WriteLine($"{item.Ecode}\t{item.Ename}\t{item.Salary}\t{item.Deptid}");
            }
        }
        static void LinqDemo1()
        {
            List<int> numbers = new List<int> { 2, 6, 1, 7, 9, 20, 21, 8, 15 };

            //Find the even numbers greater than 5 in an descending order
            //var result=new List<int>();
            //foreach (var n in numbers)
            //{
            //    if(n%2==0 && n>5)
            //    {
            //        result.Add(n);
            //    }
            //}

            //using linq with operators
            //var result = from n in numbers
            //             where n % 2 == 0 && n > 5
            //             orderby n ascending
            //             select n;
            //using line with extension method and lambda operator
            var result = numbers.Where(n => n % 2 == 0 && n > 5)
                                .OrderByDescending(n => n);

            //display the result
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }
    }
}