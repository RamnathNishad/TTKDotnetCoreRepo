using System.Data.SqlClient;
using System.Data;

namespace MVCUsingADODotnet.Models
{
    public class AdoDisconnected
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;

        public AdoDisconnected()
        {
            //configure connection
            con = new SqlConnection();
            con.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TTKDB;Integrated Security=True;";
            //configure command
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from employee";
            //configure dataadapter with select command
            da=new SqlDataAdapter(cmd);
            //fill the DataSet using dataadapter
            ds = new DataSet();
            da.Fill(ds,"employee");
            //add the primary key to the table in DataSet
            ds.Tables[0].Constraints.Add("pk1", ds.Tables[0].Columns[0], true);
        }

        public List<Employee> GetEmps()
        {
            var lstEmps=new List<Employee>();
            //traverse the dataset table records and add to the collection
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //get the record values
                var emp = new Employee
                {
                    Ecode =(int)dr[0],
                    Ename = dr[1].ToString(),
                    Salary = (int)dr[2],
                    Deptid = (int)dr[3]
                };
                //add to the collection
                lstEmps.Add(emp);
            }
            return lstEmps;
        }
        public Employee GetEmployee(int id)
        {
            //find the record in dataset table rows
            DataRow row = ds.Tables[0].Rows.Find(id);
            if(row!=null)
            {
                var emp = new Employee
                {
                    Ecode = (int)row[0],
                    Ename = row[1].ToString(),
                    Salary = (int)row[2],
                    Deptid = (int)row[3]
                };
                return emp;
            }
            return null;
        }

        public void AddEmployee(Employee emp)
        {
            //create the new empty row as per DataSet Table structure
            DataRow row = ds.Tables[0].NewRow();
            row[0] = emp.Ecode;
            row[1]= emp.Ename;
            row[2]= emp.Salary;
            row[3] = emp.Deptid;
            //add the row to the DataSet Table Rows
            ds.Tables[0].Rows.Add(row);

            //build the commands for the DataAdapter
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            //update the database using DataAdapter
            da.Update(ds, "employee");
        }
        public void DeleteEmployee(int id)
        {
            try
            {
                //find the row in the dataset table rows for deletion
                DataRow row = ds.Tables[0].Rows.Find(id);
                if (row != null)
                {
                    //delete the row
                    row.Delete();
                    //update database
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    //update the database using DataAdapter
                    da.Update(ds, "employee");
                }
                else
                {
                    throw new Exception("ecode does not exist");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        public void UpdateEmployee(Employee emp)
        {
            try
            {
                //find the row in the dataset table rows for updation
                DataRow row = ds.Tables[0].Rows.Find(emp.Ecode);
                if (row != null)
                {
                    //update the row
                    row[1] = emp.Ename;
                    row[2] = emp.Salary;
                    row[3] = emp.Deptid;

                    //update database
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    //update the database using DataAdapter
                    da.Update(ds, "employee");
                }
                else
                {
                    throw new Exception("ecode does not exist");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
