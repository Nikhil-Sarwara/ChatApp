using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;

namespace ChatApp.API.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return Ok(new
            {
                message = "Welcome to the ChatApp API",
                status = "success",
                timestamp = DateTime.UtcNow.ToString("o"),
                version = "1.0.0",
                ApiBehaviorOptions = new
                {
                    InvalidModelStateResponseFactory = "Returns a 400 Bad Request response with validation errors",
                    ClientErrorMapping = new
                    {
                        BadRequest = "400",
                        NotFound = "404",
                        InternalServerError = "500"
                    }
                },
                OpenApiSpecVersion = "3.0.1",
            });
        }
    }
}
