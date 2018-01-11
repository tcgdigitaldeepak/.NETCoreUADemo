using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Formatting;

namespace Web.Pages.WishList
{
    public class IndexModel : PageModel
    {
        private readonly Web.Models.Context _context;

        public IndexModel(Web.Models.Context context)
        {
            _context = context;
        }
        static HttpClient client = new HttpClient();
        public IList<WishlistItem> Movie { get;set; }
        public SelectList Genres;
        public string MovieGenre { get; set; }


        //public async Task OnGetAsync()
        //{
        //    Movie = await _context.Movie.ToListAsync();
        //}
        //static async Task<WishlistItem> GetProductAsync(string path)
        //{
        //    WishlistItem product = null;
        //    HttpResponseMessage response = await client.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        product = await response.Content.ReadAsAsync<WishlistItem>();
        //    }
        //    return product;
        //}
        public async Task OnGetAsync(string jwtToken)
        {
        //public async Task OnGetAsync(string movieGenre, string searchString, string jwtToken)
        //{
                try
            {
                // Use LINQ to get list of genres.
                //IQueryable<string> genreQuery = from m in _context.WishlistItem
                //                                orderby m.id
                //                                select m.name;

                WishlistItem product = null;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken.Replace('"', ' '));
                HttpResponseMessage response = await client.GetAsync("http://localhost:55226/api/getall");
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //string unauth=  "Unauthorized" ;
                    //return unauth;
                    Console.Write("Unauthorized");

                }
                if (response.IsSuccessStatusCode)
                {
                    //var product1 = JsonConvert.DeserializeObject<WishlistItem>();
                    // product = await response.Content.ReadAsAsync <WishlistItem>();
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var _Data = JsonConvert.DeserializeObject<List<WishlistItem>>(jsonString);
                    Movie = _Data.ToList();
                    //return _Data;
                }
                //return product;

                //var movies = from m in _context.WishlistItem
                //             select m;

                //if (!String.IsNullOrEmpty(searchString))
                //{
                //    movies = movies.Where(s => s.name.Contains(searchString));
                //}

                //if (!String.IsNullOrEmpty(movieGenre))
                //{
                //    movies = movies.Where(x => x.name == movieGenre);
                //}
                //Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
                // Movie = await _Data.ToListAsync();
            }
            catch (Exception ex)
            {

                ex.Message.ToString();

            }

        }
    }
}
