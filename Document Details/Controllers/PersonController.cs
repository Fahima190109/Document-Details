using Microsoft.AspNetCore.Mvc;

namespace Document_Details.Controllers
{
    public class PersonController : Controller
    {
        [Route("persons/index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
