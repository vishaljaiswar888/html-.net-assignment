using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizWiz3.DataLayer;
using QuizWiz3.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace QuizWiz.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBQuizContext _context;

        public HomeController(DBQuizContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BeginTest(string user_Email)
        {
            // Check if the user email is unique
            var existingUser = _context.Submits.FirstOrDefault(u => u.user_Email == user_Email);
            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "Email already exists. Please enter a unique email.";
                return View("Index");
            }

            // Add the user email to cookie
            Response.Cookies.Append("UserEmail", user_Email);

            // Redirect to the first question
            return RedirectToAction("Quiz", new { questionNumber = 1 });
        }

        public IActionResult Quiz(int questionNumber)
        {
            // Retrieve the quiz question based on the question number
            var quizQuestion = _context.Quizs.FirstOrDefault(q => q.quiz_Id == questionNumber);
            if (quizQuestion != null)
            {
                // Pass the quiz question to the view
                return View(quizQuestion);
            }
            else
            {
                // Handle scenario where quiz question is not found
                ViewBag.ErrorMessage = "Quiz question not found.";
                return View();
            }
        }

        [HttpPost]
        public IActionResult SubmitAnswer(int quiz_Id, string user_Answer)
        {
            // Retrieve the user email from cookie
            var userEmail = Request.Cookies["UserEmail"];

            // Save the user's answer to the database
            var submission = new Submit
            {
                user_Email = userEmail,
                quiz_Id = quiz_Id,
                user_Answer = user_Answer
            };
            _context.Submits.Add(submission);
            _context.SaveChanges();

            // Calculate the next question number
            var nextQuestionNumber = quiz_Id + 1;

            // Retrieve the next quiz question based on the next question number
            var nextQuizQuestion = _context.Quizs.FirstOrDefault(q => q.quiz_Id == nextQuestionNumber);
            if (nextQuizQuestion != null)
            {
                // Redirect to the next question
                return RedirectToAction("Quiz", new { questionNumber = nextQuestionNumber });
            }
            else
            {
                // If there are no more questions, redirect to the quiz results page
                return RedirectToAction("QuizResults");
            }
        }

        public IActionResult QuizResults()
        {
            // Retrieve the user email from cookie
            var userEmail = Request.Cookies["UserEmail"];

            // Retrieve all submissions for the user
            var userSubmissions = _context.Submits
                .Include(s => s.Quiz) // Include Quiz details in the query
                .Where(s => s.user_Email == userEmail)
                .ToList();

            return View(userSubmissions);
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
