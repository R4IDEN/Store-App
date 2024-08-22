using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index() 
        {
            var model = Repository.Candidates;

            return View(model);
        }

        public IActionResult Apply()
        {

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Apply([FromForm] Candidate _candidate)
        {
            if(Repository.Candidates.Any(c => c.Email.Equals(_candidate.Email))) 
            {
                ModelState.AddModelError("", "There is already an application for you.");
            }

            if (ModelState.IsValid) 
            {
                Repository.Add(_candidate);
                return View("Feedback", _candidate);
            }
            return View();
        }
    }
}