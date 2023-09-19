using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SlackOverload.Areas.Identity.Data;
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

        public IActionResult Index(int page, SortMethodVm vm)
        {
            
            HashSet<Question> allquestions = _context.Question
                           .Include(q => q.Answers)
                           .Include(q => q.QuestionTags)
                           .Include(q => q.ApplicationUser)
                           .Include(q => q.QuestionTags)
                           .ToHashSet();

            if (vm.SortMethod == null)
            {
                vm.SortMethod = SortMethodVm.QuestionSortMethod.Latest;
            }

            ViewBag.Questions = Math.Ceiling(allquestions.Count() / 5.0);

            SortMethodVm newVm = new SortMethodVm(allquestions, vm.SortMethod.Value);
            
            newVm.Questions = allquestions;
            newVm.SortMethod = vm.SortMethod;

            
           
            if (newVm.SortMethod.Equals(SortMethodVm.QuestionSortMethod.Latest))
            {
                newVm.Questions = allquestions
                    .OrderByDescending(q => q.DatePosted)
                    .Skip((page - 1) * 5).Take(5)
                    .ToHashSet();

            }
            else if (newVm.SortMethod.Equals(SortMethodVm.QuestionSortMethod.Earliest))
            {
                newVm.Questions = allquestions
                    .OrderBy(q => q.DatePosted)
                    .Skip((page - 1) * 5).Take(5)
                    .ToHashSet();
            }
            else if (newVm.SortMethod.Equals(SortMethodVm.QuestionSortMethod.MostAnswered))
            {
                newVm.Questions = allquestions
                    .OrderByDescending(q => q.Answers.Count)
                    .Skip((page - 1) * 5).Take(5)
                    .ToHashSet();
            }
            else if (newVm.SortMethod.Equals(SortMethodVm.QuestionSortMethod.LeastAnswered))
            {
                newVm.Questions = allquestions
                    .OrderBy(q => q.Answers.Count)
                    .Skip((page - 1) * 5).Take(5)
                    .ToHashSet();
            }

            
            return View(newVm);
        }

        public IActionResult Details(int id)
        {
            Question? question = _context.Question
                .Include(q => q.Answers)
                .ThenInclude(a => a.AnswerComments)
                .Include(q => q.Answers)
                .ThenInclude(a => a.ApplicationUser)
                .Include(q => q.QuestionTags)
                .Include(q => q.ApplicationUser)
                .Include(q => q.QuestionComments)
                .ThenInclude(qc => qc.ApplicationUser)
                .Include(q => q.QuestionTags)
                .ThenInclude(qt => qt.Tag)
                .FirstOrDefault(q => q.Id == id);

            if (question == null)
            {
                return NotFound();
            }
            else
            {
                if (_context.MarkedAnswer.Any(ma => ma.QuestionId == question.Id))
                {
                    MarkedAnswers GetTheMarkedAnswer = _context.MarkedAnswer
                        .Where(ma => ma.QuestionId == question.Id).First();

                    Answer correctAnswer = _context.Answer.Find(GetTheMarkedAnswer.AnswerId);
                    ViewBag.CorrectAnswer = correctAnswer.Id;
                }
                return View(question);
            }
        }

        public IActionResult TagQuestions(int id)
        {
            Tags? tag = _context.Tags
                .Include(t => t.QuestionTags)
                .ThenInclude(qt => qt.Question)
                .ThenInclude(q => q.ApplicationUser)
                .Where(t => t.Id == id)
                .FirstOrDefault();

            if (tag == null)
            {
                return NotFound();
            }
            else
            {
                return View(tag);
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