using Microsoft.AspNetCore.Mvc;
using SecurityMVCClient.Models;
using System.Diagnostics;
using System.Text.Json;

namespace SecurityMVCClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult GetData()
        {
            var baseUrl = "http://localhost:5228/api/";
            //get the data from web api
            using (var http = new HttpClient())
            {
                var lstStr=new List<string>();
                //set the api base address for call
                http.BaseAddress = new Uri(baseUrl);
                //send the GET request using bases address and url if any
                var response = http.GetAsync("Sample");
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
                    lstStr = JsonSerializer.Deserialize<List<string>>(finalResult);
                }
                ViewData.Add("lstStr", lstStr);
                return View();
            }

           
        }


        [HttpGet]
        public ActionResult GetDataUsingToken()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetDataUsingToken(Users user) 
        {
            //get the token based on credentials
            var tokenStr = GetToken(user);

            if (tokenStr != null)
            {
                //then call the API method using token

                var baseUrl = "http://localhost:5228/api/";
                //get the data from web api
                using (var http = new HttpClient())
                {
                    var lstStr = new List<string>();
                    //set the api base address for call
                    http.BaseAddress = new Uri(baseUrl);
                    //send the GET request using bases address and url if any

                    //add token in the request header
                    http.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenStr);
                   
                    var response = http.GetAsync("Sample");
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
                        lstStr = JsonSerializer.Deserialize<List<string>>(finalResult);
                    }
                    ViewData.Add("lstStr", lstStr);
                    return View("GetData");
                }
            }
            else
            {
                return View();
            }

        }

        private string GetToken(Users user)
        {
            var baseUrl = "http://localhost:5228/api/";
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(baseUrl);
                var response = http.PostAsJsonAsync("Sample/authenticate", user);
                response.Wait();
                var responseResult = response.Result;
                if(responseResult.IsSuccessStatusCode)
                {
                    var token=responseResult.Content.ReadAsStringAsync();
                    token.Wait();
                    var tokenStr = token.Result;

                    var tokens=JsonSerializer.Deserialize<Tokens>(tokenStr);
                    return tokens.token;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}