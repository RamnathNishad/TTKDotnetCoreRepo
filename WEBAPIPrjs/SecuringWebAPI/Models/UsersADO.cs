
using System.Data.SqlClient;
using System.Data;

namespace SecuringWebAPI.Models
{
    public class UsersADO : IUsersADO
    {
        SqlConnection con;
        SqlCommand cmd;

        private readonly IConfiguration _configuration;
        public UsersADO(IConfiguration configuration)
        {
            _configuration = configuration;
            con = new SqlConnection();
            con.ConnectionString = _configuration.GetConnectionString("sqlcon");
        }
        public bool IsValidUser(Users user)
        {
            cmd = new SqlCommand();
            cmd.CommandText = "select username,password from tbl_users where username=@uname and password=@pwd";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@uname", user.Name);
            cmd.Parameters.AddWithValue("@pwd", user.Password);

            cmd.Connection = con;
            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            if(sdr.Read())
            {
                con.Close();
                return true;
            }

            con.Close();
            return false;
        }
    }
}
