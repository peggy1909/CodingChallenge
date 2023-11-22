using Microsoft.AspNetCore.Mvc;
using WeatherAppNT.AG.Models;
using WeatherAppNT.AG.Services;

namespace WeatherAppNT.AG.Controllers
{
    public class LocationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("geo/reverse/")]
        public ActionResult<List<LocationModel>> GetCity()
        {
            var service = new LocationService();

            HttpContext.Response.Headers.Append("Access-Control-Allow-Origin", "*");

            return service.GetLocation(); //nicht vollständig implementiert
        }
    }
}
