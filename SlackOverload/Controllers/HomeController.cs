using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SlackOverload.Data;
using SlackOverload.Models;
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

        public IActionResult Index()
        {
            HashSet<Question> allquestions = _context.Question
                           .Include(q => q.Answers)
                           .Include(q => q.QuestionTags)
                           .Include(q => q.ApplicationUser)
                           .Include(q => q.QuestionTags)
                           .ToHashSet();

            return View(allquestions);
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