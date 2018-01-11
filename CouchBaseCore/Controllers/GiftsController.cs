using Microsoft.AspNetCore.Mvc;
using Couchbase;
using Couchbase.Core;
using CouchBaseCore.Models;
using Couchbase.N1QL;
using Microsoft.AspNetCore.Authorization;

namespace CouchBaseCore.Controllers
{
    //[Produces("application/json")]
    //[Route("api/Gifts")]
    public class GiftsController : Controller
    {

        private IBucket _buckate;
        private IBucket _buckate2;

        public GiftsController()
        {
            _buckate = ClusterHelper.GetBucket("travel-sample");
            _buckate2 = ClusterHelper.GetBucket("test");
        }

        [Authorize]
        [HttpGet]
        [Route("api/getall")]
        public IActionResult GetAll()
        {
            //var n1ql = @" select g.*, META(g).Id
            //          from demo g where g.type='wishlistItem';";
            var n1ql = @" SELECT id,country,name,type FROM `travel-sample`;";

            var query = QueryRequest.Create(n1ql);
            query.ScanConsistency(ScanConsistency.RequestPlus);
            var result = _buckate.Query<WishlistItem>(query);
            return Ok(result.Rows);

        }

        [HttpGet]
        [Route("api/get/{id}")]
        public IActionResult Get(string id)
        {
            var result = _buckate.Get<WishlistItem>(id.ToString());

            return Ok(result.Value);
        }
        [Authorize]
        [HttpDelete]
        [Route("api/delete/{id}")]
        public IActionResult Delete(string id)
        {
            _buckate.Remove(id.ToString());
            return Ok(id);
        }

        
        [Authorize]
        [HttpPost]
        [Route("api/TravelAdd")]
        public IActionResult Add([FromBody]WishlistItem item)
        {
          
            var idd = "";

            var n1ql = @" SELECT count (*) as id FROM   `travel-sample`;";

            var query = QueryRequest.Create(n1ql);
            query.ScanConsistency(ScanConsistency.RequestPlus);
            var res = _buckate.Query<WishlistItem>(query);
            var cnt = res.Rows[0].ToString();
            foreach (var row in res.Rows)
            {
                idd=(row.id);
                
            }

            var document = new Document<dynamic>
            {

            
                Id = idd.ToString(),
                Content = new
                {
                    //name = "Polar Ice",
                    //brewery_id = "Polar"
                    id = item.id,
                    country = item.country,
                    name = item.name,
                    type = item.type
                }
            };
            var result = _buckate.Insert(document);
            if (result.Success)
            {
                return Ok(item);
                // Console.WriteLine("Inserted document '{0}'", document.Id);
            }
            return Ok("Not Success");
        }
        [Authorize]
        [HttpPost]
        [Route("api/AddToken")]
        public IActionResult AddToken([FromBody]Token JwdToken)
        {

            var idd = "";

            var n1ql = @" SELECT count (*) as id FROM   `test`;";

            var query = QueryRequest.Create(n1ql);
            query.ScanConsistency(ScanConsistency.RequestPlus);
            var res = _buckate2.Query<WishlistItem>(query);
            var cnt = res.Rows[0].ToString();
            foreach (var row in res.Rows)
            {
                idd = (row.id);

            }

                var document = new Document<dynamic>
            {
                Id = idd.ToString(),
                Content = new
                {
                    //name = "Polar Ice",
                    //brewery_id = "Polar"
                    Id = idd.ToString(),
                    UserId = JwdToken.UserId,
                    token = JwdToken.token,
                   
                }
            };
            var result = _buckate2.Insert(document);
            if (result.Success)
            {
                return Ok(JwdToken);
                // Console.WriteLine("Inserted document '{0}'", document.Id);
            }
            return Ok("Not Success");
        }


        [Authorize]
        [HttpPost]
        [Route("api/TravelEdit")]
        public IActionResult Edit(WishlistItem item)
        {
            //if (!item.id.GetHashCode)
            //    item.id = Guid.NewGuid();
            _buckate.Upsert(item.id.ToString(), new
            {
                //icao = item.icao,
                //iata = item.iata,
                //callsign = item.callsign,
                id = item.id,
                country = item.country,
                name = item.name,
                type = item.type
            });
            return Ok(item);
        }
    }
}