using System.Data.SqlClient;
using System.Data;


namespace MVCUsingADODotnet.Models
{
    public class AdoConnected
    {
        SqlConnection con;
        SqlCommand cmd;

        public AdoConnected()
        {
            con = new SqlConnection();
            con.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TTKDB;Integrated Security=True;";
        }
        public List<Employee> GetEmps()
        {
            //configure sql command for SELECT ALL
            cmd = new SqlCommand();
            cmd.CommandText = "select * from employee";

            //attach connection and command
            cmd.Connection= con;

            //open the connection
            con.Open();

            //execute the command
            SqlDataReader sdr = cmd.ExecuteReader();

            //Traverse the result
            var lstEmps=new List<Employee>();
            while(sdr.Read())
            {
                //get the record column values inti object
                var emp = new Employee
                {
                    Ecode = (int)sdr[0],
                    Ename = sdr[1].ToString(),
                    Salary = (int)sdr[2],
                    Deptid = (int)sdr[3]
                };
                //add the object to the result collectiom
                lstEmps.Add(emp);
            };

            //close the connection;
            con.Close();

            //return the result
            return lstEmps;
        }
        public Employee GetEmployee(int id)
        {
            cmd = new SqlCommand();
            cmd.CommandText = $"select ecode,ename,salary,deptid from employee where ecode=@ec";
            cmd.Connection= con;

            //specify the parameter values in the command text
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ec", id);

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            Employee emp = new Employee();
            if(sdr.Read())
            {
                emp.Ecode = (int)sdr[0];
                emp.Ename = sdr[1].ToString();
                emp.Salary = (int)sdr[2];
                emp.Deptid = (int)sdr[3];
            }
            //close the connection
            con.Close();

            return emp;
        }

        public void AddEmployee(Employee emp)
        {
            //configure the command for INSER
            cmd = new SqlCommand();
            cmd.CommandText = "insert into employee(ecode,ename,salary,deptid) values(@ec,@en,@sal,@did)";
            //attach connection
            cmd.Connection= con;
            cmd.CommandType= CommandType.Text;
            
            //specify the parameters values 
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ec", emp.Ecode);
            cmd.Parameters.AddWithValue("@en", emp.Ename);
            cmd.Parameters.AddWithValue("@sal", emp.Salary);
            cmd.Parameters.AddWithValue("@did", emp.Deptid);

            //open connection
            con.Open();
            cmd.ExecuteNonQuery();

            //close the connection
            con.Close();
        }
        public void DeleteEmployee(int id)
        {
            try
            {
                cmd = new SqlCommand();
                cmd.CommandText = "delete from employee where ecode=@ec";
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ec", id);
                con.Open();
                int recordsAffected = cmd.ExecuteNonQuery();
                if (recordsAffected == 0)
                {
                    throw new Exception("ecode does not exist, could not delete the record");
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public void UpdateEmployee(Employee emp)
        {
            cmd = new SqlCommand("update employee set ename=@en,salary=@sal,deptid=@did where ecode=@ec");
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
           
            //specify the parameters values 
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ec", emp.Ecode);
            cmd.Parameters.AddWithValue("@en", emp.Ename);
            cmd.Parameters.AddWithValue("@sal", emp.Salary);
            cmd.Parameters.AddWithValue("@did", emp.Deptid);

            //open connection
            con.Open();
            cmd.ExecuteNonQuery();

            //close the connection
            con.Close();
        }
        public void UpdateSalary(int id,int salary)
        {
            //configure the command for calling Stored procedure
            cmd = new SqlCommand();
            cmd.CommandText = "sp_UpdateSalary";
            cmd.Connection = con;
            //specify the command type - mandatory
            cmd.CommandType = CommandType.StoredProcedure;

            double bonus = 0;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ec", id);
            cmd.Parameters.AddWithValue("@sal", salary);
            cmd.Parameters.AddWithValue("@bonus", bonus);

            //specify the directions of parameters if it is there
            cmd.Parameters[2].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();

            //get the bonus parameter updated value
            bonus = (double)cmd.Parameters[2].Value;

            con.Close();
        }
    }
}
