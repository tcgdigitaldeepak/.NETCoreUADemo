using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Web.Models;

namespace Web.Pages.Login
{
    public class LoginModel : PageModel
    {
        string Baseurl = "http://localhost:53533/";

        static HttpClient client = new HttpClient();

        private readonly Web.Models.Context _context;

        public LoginModel(Web.Models.Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        //public void OnGet()
        //{
        //}
       

        [BindProperty]
        public LoginDto item { get; set; }
        [BindProperty]
        public Token Tokenn { get; set; }

        [HttpPost]
        //public IActionResult Login(LoginDto model)
        //{
            public async Task<IActionResult> OnPostAsync()
            {
                LoginResponse logResponse = new LoginResponse();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string stringData = JsonConvert.SerializeObject(item);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = client.PostAsync("api/Account/Login", contentData).Result;

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var loginRes = Res.Content.ReadAsStringAsync().Result;
                    logResponse.token = loginRes.ToString().Replace('"',' ');

                    Tokenn.UserId = item.Email;
                    
                    Tokenn.token = logResponse.token.ToString();
                    HttpContext.Session.Clear();
                    HttpContext.Session.SetString("token",Tokenn.token);
                    //save sql
                    _context.Token.Add(Tokenn);
                    await _context.SaveChangesAsync();

                    //save couchbase
                    _context.Token.Add(Tokenn);
                    //await _context.SaveChangesAsync();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + logResponse.token);
                    HttpResponseMessage response = await client.PostAsJsonAsync("http://localhost:55226/api/AddToken", Tokenn);
                    response.EnsureSuccessStatusCode();


                    //Deserializing the response recieved from web api and storing into the Employee list  
                    //logResponse.token =JsonConvert.DeserializeObject<LoginResponse>(loginRes);
                    //TempData["token"] = logResponse.token;
                    return RedirectToPage("./Index");
                }
                //returning the employee list to view  
                //return View(logResponse);
                //return logResponse.token;
                return RedirectToPage("./Login"); 
            }
        }
    }
}