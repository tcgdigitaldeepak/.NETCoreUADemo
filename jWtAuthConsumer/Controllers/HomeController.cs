using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jWtAuthConsumer.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace jWtAuthConsumer.Controllers
{
    public class HomeController : Controller
    {
        string Baseurl = "http://localhost:53533/";


        public IActionResult Index()
        {
            return View("Index1");
        }

        [HttpPost]
        public IActionResult Login(LoginDto model)
        {
            LoginResponse logResponse = new LoginResponse();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string stringData = JsonConvert.SerializeObject(model);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = client.PostAsync("api/Account/Login", contentData).Result;

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var loginRes = Res.Content.ReadAsStringAsync().Result;
                    logResponse.token = loginRes;
                    //Deserializing the response recieved from web api and storing into the Employee list  
                    //logResponse.token =JsonConvert.DeserializeObject<LoginResponse>(loginRes);
                    //TempData["token"] = logResponse.token;
                    return RedirectToAction("About", "Home", new { jwtToken = logResponse.token });
                }
                //returning the employee list to view  
                //return View(logResponse);
                //return logResponse.token;
                return View("Index1");
            }
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult GetValue(string jwtToken)
        {

            using (var _client = new HttpClient())
            {
                var rsUrl = "http://localhost:53533/api/Values/Get/1";
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken.Replace('"', ' '));
                }

                HttpResponseMessage rsMsg = _client.GetAsync(rsUrl).Result;

                if (rsMsg.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return Json("Unauthorized");
                }
                //if (rsMsg.IsSuccessStatusCode)
                //{
                //Storing the response details recieved from web api   
                var authRes = rsMsg.Content.ReadAsStringAsync().Result;

                return Json(authRes);
                //}
            }
        }
    }
}
