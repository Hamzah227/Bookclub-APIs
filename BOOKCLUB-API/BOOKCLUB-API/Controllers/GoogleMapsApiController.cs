using BOOKCLUB_API.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BOOKCLUB_API.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace BOOKCLUB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleMapsApiController : ControllerBase
    {
        //private BidderLocations destination;
        //private readonly IGoogleMapsApi googleApis;

        //public GoogleMapsApiController(IGoogleMapsApi googleApi)
        //{
        //    googleApis = googleApi;
        //}

        //[HttpPost("GetDistance")]
        //public APIModel DistanceRequest([FromBody] JObject json)
        //{
        //    destination = new BidderLocations();
        //    destination = JsonConvert.DeserializeObject<BidderLocations>(json.ToString());
        //    APIModel result = googleApis.GetDistance(destination);
        //    return result;
        //}

        //[HttpPost("GetDirection")]
        //public APIModel GetDirections([FromBody] JObject json)
        //{
        //    destination = new BidderLocations();
        //    destination = JsonConvert.DeserializeObject<BidderLocations>(json.ToString());
        //    APIModel result = googleApis.GetDirections(destination);
        //    return result;
        //}
    }
}
