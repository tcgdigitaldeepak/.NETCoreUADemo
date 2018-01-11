using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Formatting;

namespace Web.WishList
{
    public class CreateModel : PageModel
    {
        static HttpClient client = new HttpClient();

        private readonly Web.Models.Context _context;

        public CreateModel(Web.Models.Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public WishlistItem item { get; set; }

        //[HttpPost]

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {

                string jwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhQGdtYWlsLmNvbSIsImp0aSI6IjM0MWE3NDdlLWYxNjgtNGY3YS1hYzEyLTMzYzNlZDE0MDY1MyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiZWMxODUxODEtOGEyNy00NDUxLThjNjgtZWE4ZjljMzFlYzI1IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6ImFAZ21haWwuY29tIiwiZXhwIjoxNTE4MDgzOTAxLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdCJ9.JUHMRn9gEzBkXqYw0u-ftyRinpstVSvH2o7io9K54OU";
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _context.WishlistItem.Add(item);
                //await _context.SaveChangesAsync();
                 client.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken.Replace('"', ' '));
                HttpResponseMessage response = await client.PostAsJsonAsync("http://localhost:55226/api/TravelAdd", item);
                response.EnsureSuccessStatusCode();

                // return URI of the created resource.
                //return response.Headers.Location;

               
            }
            catch(Exception ex)
            {
                ex.Message.ToString();

            }
            return RedirectToPage("./Index");
        }

        //[HttpPost]
        //public async Task<IActionResult> OnPostAsync(WishlistItem product)
        //{
        //    //HttpResponseMessage response = await client.PostAsJsonAsync(
        //    //    "api/products", product);
        //    //response.EnsureSuccessStatusCode();



        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri("http://localhost:55226/");
        //    client.DefaultRequestHeaders.Accept.Add(
        //       new MediaTypeWithQualityHeaderValue("application/json"));


        //    var response = client.PostAsJsonAsync("api/TravelAdd", product).Result;



        //    //// return URI of the created resource.
        //    //return response.Headers.Location;
        //    return null;
        //}
    }
}