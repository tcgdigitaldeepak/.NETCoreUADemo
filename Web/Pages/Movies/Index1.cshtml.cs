using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Web.Pages.Movies
{
    public class Index1Model : PageModel
    {
        static HttpClient client = new HttpClient();
        //public void OnGet()
        //{
        //}
        private readonly Web.Models.Context _context;

        public Index1Model(Web.Models.Context context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; }
        public SelectList Genres;
        public string MovieGenre { get; set; }


        //public async Task OnGetAsync()
        //{
        //    Movie = await _context.Movie.ToListAsync();
        //}

        //public async Task OnGetAsync(string movieGenre, string searchString)
        //{
        //    // Use LINQ to get list of genres.
        //    IQueryable<string> genreQuery = from m in _context.Movie
        //                                    orderby m.Genre
        //                                    select m.Genre;

        //    var movies = from m in _context.Movie
        //                 select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        movies = movies.Where(s => s.Title.Contains(searchString));
        //    }

        //    if (!String.IsNullOrEmpty(movieGenre))
        //    {
        //        movies = movies.Where(x => x.Genre == movieGenre);
        //    }
        //    Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
        //    Movie = await movies.ToListAsync();
        //}
        //public async Task OnGetAsync(string movieGenre, string searchString)
        //{


        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        movies = movies.Where(s => s.Title.Contains(searchString));
        //    }

        //    if (!String.IsNullOrEmpty(movieGenre))
        //    {
        //        movies = movies.Where(x => x.Genre == movieGenre);
        //    }
        //    Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
        //    Movie = await movies.ToListAsync();
        //}
        //[HttpGet]
        //static async Task<Movie> GetProductAsync(string path)
        //{
        //    Movie product = null;
        //    HttpResponseMessage response = await client.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        product = await response.Content.ReadAsAsync<Movie>();
        //    }
        //    return product;
        //}

        [HttpPost]
       // public string Task<Movie> GetAsync(string path)
       // static async Task<Movie> GetProductAsync(WishlistItem model)
        public string Add(WishlistItem model)
        {
            //LoginResponse logResponse = new LoginResponse();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                //client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string stringData = JsonConvert.SerializeObject(model);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = client.PostAsync("http://localhost:55226/api/getall", contentData).Result;

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var loginRes = Res.Content.ReadAsStringAsync().Result;
                    //logResponse.token = loginRes;
                    //Deserializing the response recieved from web api and storing into the Employee list  
                    //logResponse.token =JsonConvert.DeserializeObject<LoginResponse>(loginRes);

                }
                //returning the employee list to view  
                //return View(logResponse);
                return null;
            }
        }

    }
}