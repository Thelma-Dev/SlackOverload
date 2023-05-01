using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SlackOverload.Areas.Identity.Data;
using SlackOverload.Data;
using SlackOverload.Models;
using System.Data;

namespace SlackOverload.Controllers
{
    [Authorize(Roles = "User")]
    public class AnswerController : Controller
    {
        private readonly SlackContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AnswerController(SlackContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Upvote(int id)
        {
            ApplicationUser user = _context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            Answer selectedAnswer = _context.Answer
                    .Include(a => a.ApplicationUser)
                    .First(a => a.Id == id);


            if (selectedAnswer == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.AnswerId = selectedAnswer.Id;

                if (selectedAnswer.ApplicationUserId == user.Id)
                {
                    ViewBag.Message = "You cannot vote on your answers";
                    return View();
                }
                else
                {

                    return View(selectedAnswer);
                }

            }
        }

        [HttpPost]

        public IActionResult Upvote(Answer answer)
        {
            try
            {
                ApplicationUser user = _context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

                Answer selectedAnswer = _context.Answer
                    .Include(a => a.ApplicationUser)
                    .First(a => a.Id == answer.Id);

                AnswerVote? VotedAnswerToSwitch = _context.AnswerVote.Where(av => av.ApplicationUserId == user.Id && av.AnswerId == selectedAnswer.Id && av.UpVote == false).FirstOrDefault();

                if (VotedAnswerToSwitch == null)
                {
                    AnswerVote newAnswerVote = new AnswerVote();

                    newAnswerVote.Answer = selectedAnswer;
                    newAnswerVote.AnswerId = selectedAnswer.Id;
                    newAnswerVote.ApplicationUser = user;
                    newAnswerVote.ApplicationUserId = user.Id;
                    newAnswerVote.UpVote = true;

                    if (_context.AnswerVote.Any(av => av.ApplicationUserId == user.Id && av.AnswerId == selectedAnswer.Id && av.UpVote == newAnswerVote.UpVote))
                    {
                        ViewBag.Message = "You already upvoted this answer";
                        return View(selectedAnswer);
                    }
                    else
                    {
                        selectedAnswer.ApplicationUser.Reputation += 5;
                        _context.AnswerVote.Add(newAnswerVote);
                        selectedAnswer.AnswerVotes.Add(newAnswerVote);
                        _context.SaveChanges();

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    selectedAnswer.ApplicationUser.Reputation += 5;
                    VotedAnswerToSwitch.UpVote = true;
                    _context.AnswerVote.Update(VotedAnswerToSwitch);

                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }



            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult DownVote(int id)
        {
            ApplicationUser user = _context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            Answer selectedAnswer = _context.Answer
                     .Include(a => a.ApplicationUser)
                     .First(a => a.Id == id);


            if (selectedAnswer == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.AnswerId = selectedAnswer.Id;

                if (selectedAnswer.ApplicationUserId == user.Id)
                {
                    ViewBag.Message = "You cannot downvote your own answers";
                    return View();
                }
                else
                {

                    return View(selectedAnswer);
                }

            }
        }

        [HttpPost]

        public IActionResult DownVote(Answer answer)
        {
            try
            {
                ApplicationUser user = _context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

                Answer selectedAnswer = _context.Answer
                    .Include(a => a.ApplicationUser)
                    .First(a => a.Id == answer.Id);

                AnswerVote? VotedAnswerToSwitch = _context.AnswerVote.Where(av => av.ApplicationUserId == user.Id && av.AnswerId == selectedAnswer.Id && av.UpVote == true).FirstOrDefault();

                if (VotedAnswerToSwitch == null)
                {
                    AnswerVote newAnswerVote = new AnswerVote();

                    newAnswerVote.Answer = selectedAnswer;
                    newAnswerVote.AnswerId = selectedAnswer.Id;
                    newAnswerVote.ApplicationUser = user;
                    newAnswerVote.ApplicationUserId = user.Id;
                    newAnswerVote.UpVote = false;

                    if (_context.AnswerVote.Any(av => av.ApplicationUserId == user.Id && av.AnswerId == selectedAnswer.Id && av.UpVote == newAnswerVote.UpVote))
                    {
                        ViewBag.Message = "You already downvoted this answer";
                        return View(selectedAnswer);
                    }
                    else
                    {
                        selectedAnswer.ApplicationUser.Reputation -= 5;
                        _context.AnswerVote.Add(newAnswerVote);
                        selectedAnswer.AnswerVotes.Add(newAnswerVote);
                        _context.SaveChanges();

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    selectedAnswer.ApplicationUser.Reputation -= 5;
                    VotedAnswerToSwitch.UpVote = false;
                    _context.AnswerVote.Update(VotedAnswerToSwitch);

                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }



            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
