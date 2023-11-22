using Microsoft.AspNetCore.Mvc;
using WeatherApp.Services;
using WeatherAppNT.AG.Models;

namespace WeatherAppNT.AG.Controllers
{
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("data/weather/")]
        public ActionResult<WeatherModel> GetWeather([FromQuery] string q)
        {
            var service = new WeatherService();

            HttpContext.Response.Headers.Append("Access-Control-Allow-Origin", "*");

            if (!string.IsNullOrEmpty(q))
            {
                try
                {
                    return service.GetWeather(q);
                }
                catch (Exception ex)
                {
                    return StatusCode(404, new { error = "City not found" });
                }
                
            }

            return BadRequest();
        }
    }
}
