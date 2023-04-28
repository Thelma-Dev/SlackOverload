using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SlackOverload.Data;
using SlackOverload.Models;
using SlackOverload.Models.ViewModel;
using System.Diagnostics;

namespace SlackOverload.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SlackContext _context;

        public HomeController(ILogger<HomeController> logger, SlackContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int page, SortMethodVm.QuestionSortMethod? questionSortMethod = null)
        {

            HashSet<Question> allquestions = _context.Question
                           .Include(q => q.Answers)
                           .Include(q => q.QuestionTags)
                           .Include(q => q.ApplicationUser)
                           .Include(q => q.QuestionTags)
                           .ToHashSet();

            if (questionSortMethod == null)
            {
                questionSortMethod = SortMethodVm.QuestionSortMethod.Latest;
            }

            ViewBag.Questions = Math.Ceiling(allquestions.Count() / 5.0);

            SortMethodVm newVm = new SortMethodVm(allquestions, questionSortMethod.Value);
            
            newVm.Questions = allquestions;
            newVm.SortMethod = questionSortMethod;

            
           
            if (newVm.SortMethod.Equals(SortMethodVm.QuestionSortMethod.Latest))
            {
                newVm.Questions = allquestions
                    .Skip((page - 1) * 5).Take(5)
                    .OrderByDescending(q => q.DatePosted).ToHashSet();

            }
            else if (newVm.SortMethod.Equals(SortMethodVm.QuestionSortMethod.Earliest))
            {
                newVm.Questions = allquestions
                    .Skip((page - 1) * 5).Take(5)
                    .OrderBy(q => q.DatePosted).ToHashSet();
            }
            else if (newVm.SortMethod.Equals(SortMethodVm.QuestionSortMethod.MostAnswered))
            {
                newVm.Questions = allquestions
                    .Skip((page - 1) * 5).Take(5)
                    .OrderByDescending(q => q.Answers.Count).ToHashSet();
            }
            else if (newVm.SortMethod.Equals(SortMethodVm.QuestionSortMethod.LeastAnswered))
            {
                newVm.Questions = allquestions
                    .Skip((page - 1) * 5).Take(5)
                    .OrderBy(q => q.Answers.Count).ToHashSet();
            }
            
            
            return View(newVm);
        }

        public IActionResult Details(int id)
        {
            Question? question = _context.Question
                .Include(q => q.Answers)
                .ThenInclude(a => a.ApplicationUser)
                .Include(q => q.QuestionTags)
                .Include(q => q.ApplicationUser)
                .Include(q => q.QuestionTags)
                .ThenInclude(qt => qt.Tag)
                .FirstOrDefault(q => q.Id == id);

            if (question == null)
            {
                return NotFound();
            }
            else
            {
                return View(question);
            }
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}