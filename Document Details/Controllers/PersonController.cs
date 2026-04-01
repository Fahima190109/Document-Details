using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Document_Details.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        [Route("persons/index")]
        public IActionResult Index(string searchBy, string? searchString)
        {
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {nameof(PersonResponse.PersonName), "Person Name"},
                {nameof(PersonResponse.Email), "Person Email" },
                {nameof(PersonResponse.DateOfBirth), "Person DateOfBirth"},
                {nameof(PersonResponse.Gender), "Person Gender" },
                {nameof(PersonResponse.CountryID), "Person CountryID"},
                {nameof(PersonResponse.Address), "Person Address"},
            };
            List<PersonResponse> persons = _personService.GetFilterPersons(searchBy, searchString);  
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentsearchString = searchString;
            return View(persons);
        }
    }
}
