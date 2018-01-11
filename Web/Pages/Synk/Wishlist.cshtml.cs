using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Web.Pages.Synk
{
    public class WishlistModel : PageModel
    {
        private readonly Web.Models.Context _context;

        public WishlistModel(Web.Models.Context context)
        {
            _context = context;
        }

        [BindProperty]
        public WishlistItem item { get; set; }

        static HttpClient client = new HttpClient();
        public IList<WishlistItem> Movie { get; set; }
       // public SelectList Genres;
       // public string MovieGenre { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

          //public async Task OnGetAsync()
          //{
           public async Task<IActionResult> OnPostAsync()
           {
            try
            {

                //
                WishlistItem product = null;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("token"));
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

                    _context.WishlistItem.Add(item);
                    await _context.SaveChangesAsync();

                }
                return null;
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                return null;
            }

        }

    }
}