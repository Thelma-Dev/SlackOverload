using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SlackOverload.Areas.Identity.Data;
using SlackOverload.Data;
using SlackOverload.Models;
using SlackOverload.Models.ViewModel;

namespace SlackOverload.Controllers
{
    [Authorize(Roles ="User")]
    public class QuestionController : Controller
    {
        private readonly SlackContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public QuestionController(SlackContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {

            ApplicationUser user = _context.Users
                .Where(u => u.UserName == User.Identity.Name)
                .FirstOrDefault();

            HashSet<Question> allquestions = _context.Question
               .Include(q => q.Answers)
               .Include(q => q.QuestionTags)
               .Where(q => q.ApplicationUserId == user.Id)
               .ToHashSet();

            return View(allquestions);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            HashSet<Tags> allTags = _context.Tags.ToHashSet();

            CreateQuestionVm vm = new CreateQuestionVm(allTags);

            return View(vm);
        }


        [HttpPost]
        public IActionResult Create([Bind("Title","Description","TagId")]CreateQuestionVm vm)
        {
            try
            {
                ApplicationUser user = _context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

                HashSet<Tags> AllTags = _context.Tags.ToHashSet();

                List<string> QuestionTagsId = Request.Form["TagId"].ToList();

                List<Tags> QuestionTags = new List<Tags>();

                foreach(string t in QuestionTagsId)
                {
                    
                    Tags tag = _context.Tags.Find(Int32.Parse(t));

                    if (tag != null)
                    {
                        QuestionTags.Add(tag);
                    } 
                }

                Question newQuestion = new Question();
                newQuestion.ApplicationUser = user;
                newQuestion.ApplicationUserId = user.Id;
                newQuestion.Title = vm.Title; 
                newQuestion.Description = vm.Description;
                newQuestion.DatePosted = DateTime.Now;
                

                if(ModelState.IsValid)
                {
                    _context.Question.Add(newQuestion);
                    _context.SaveChanges();

                    foreach(Tags t in QuestionTags)
                    {
                        QuestionTag newQuestionTag = new QuestionTag();

                        newQuestionTag.Question = newQuestion;
                        newQuestionTag.QuestionId = newQuestion.Id;
                        newQuestionTag.TagId = t.Id;
                        newQuestionTag.Tag = t;

                        t.QuestionTags.Add(newQuestionTag);
                        newQuestion.QuestionTags.Add(newQuestionTag);
                    }
                    _context.SaveChanges();
                } 
                else
                {
                    CreateQuestionVm newVm = new CreateQuestionVm(AllTags);
                    return View(newVm);

                }

                return RedirectToAction("Index");
            } 
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult PostAnswer(int id)
        {
            Question selectedQuestion = _context.Question.Find(id);

            PostAnswerVm vm = new PostAnswerVm();

            ViewBag.QuestionId = selectedQuestion.Id;
            vm.QuestionId = selectedQuestion.Id;

            return View(vm);
        }

        [HttpPost]
        public IActionResult PostAnswer([Bind("QuestionId","QuestionAnswer")] PostAnswerVm vm)
        {
            try
            {
                ApplicationUser user = _context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

                Question selectedQuestion = _context.Question.Find(vm.QuestionId);

                Answer newAnswer = new Answer();

                newAnswer.Question = selectedQuestion;
                newAnswer.QuestionId = selectedQuestion.Id;
                newAnswer.ApplicationUser = user;
                newAnswer.ApplicationUserId = user.Id;
                newAnswer.DatePosted = DateTime.Now;
                newAnswer.QuestionAnswer = vm.QuestionAnswer;


                if (ModelState.IsValid)
                {
                    _context.Answer.Add(newAnswer);
                    selectedQuestion.Answers.Add(newAnswer);
                    _context.SaveChanges();

                    return RedirectToAction("Details", "Home", new { id = selectedQuestion.Id });
                }
                else
                {
                    return View(newAnswer);
                }

                
            } 
            catch(Exception exe)
            {
                return BadRequest();
            }
        }

        public IActionResult ViewAnswer(int id)
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
    }
}
