using System.Text.Json;

namespace MVCCoreAPIClient.Models
{
    public class ApiConsumer
    {
        static string baseUrl = "http://localhost:5018/api/Employees/";
        public static List<Employee> GetEmps()
        {
            using (var http = new HttpClient())
            {
                var lstEmps = new List<Employee>();
                //set the api base address for call
                http.BaseAddress = new Uri(baseUrl);
                //send the GET request using bases address and url if any
                var response = http.GetAsync("");
                response.Wait();

                //get the response out of the call
                var responseResult = response.Result;

                //if response status is success
                if (responseResult.IsSuccessStatusCode)
                {
                    //read the content of the response
                    var data = responseResult.Content.ReadAsStringAsync();
                    data.Wait();
                    //get the string result of the response
                    var finalResult = data.Result;
                    //convert the json string into object 
                    lstEmps = JsonSerializer.Deserialize<List<Employee>>(finalResult);
                }      
                return lstEmps;
            }
        }
        public static Employee GetEmpById(int id)
        {
            using (var http = new HttpClient())
            {
                var emp = new Employee();
                //set the api base address for call
                http.BaseAddress = new Uri(baseUrl);
                //send the GET request using bases address and url if any
                var response = http.GetAsync(id.ToString());
                response.Wait();

                //get the response out of the call
                var responseResult = response.Result;

                //if response status is success
                if (responseResult.IsSuccessStatusCode)
                {
                    //read the content of the response
                    var data = responseResult.Content.ReadAsStringAsync();
                    data.Wait();
                    //get the string result of the response
                    var finalResult = data.Result;
                    //convert the json string into object 
                    emp = JsonSerializer.Deserialize<Employee>(finalResult);
                }
                return emp;
            }
        }
        public static bool AddEmployee(Employee emp)
        {
            using(var http = new HttpClient())
            {
                http.BaseAddress = new Uri(baseUrl);
                var response = http.PostAsJsonAsync("", emp);
                response.Wait();
                var responseResult = response.Result;
                return responseResult.IsSuccessStatusCode;
            }
        }
        public static bool DeleteEmployee(int id)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(baseUrl);
                var response =http.DeleteAsync(id.ToString());
                response.Wait();
                var responseResult = response.Result;
                return responseResult.IsSuccessStatusCode;
            }
        }
        public static bool UpdateEmployee(Employee emp)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(baseUrl);
                var response = http.PutAsJsonAsync(emp.ecode.ToString(), emp);
                response.Wait();
                var responseResult = response.Result;
                return responseResult.IsSuccessStatusCode;
            }
        }
    }
}
