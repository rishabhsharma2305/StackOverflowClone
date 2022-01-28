using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowClone.RepositoryLayer;
using StackOverflowClone.DomainModels;
using StackOverflowClone.ViewModel;

namespace StackOverflowClone.Controllers
{
    public class QuestionController : Controller
    {
        QuestionRepo questionRepo;
        AnswerRepo answerRepo;
        CategoryRepo categoryRepo;
        public QuestionController()
        {
            questionRepo = new QuestionRepo();
            answerRepo = new AnswerRepo();
            categoryRepo = new CategoryRepo();
        }
        public ActionResult View(int id)
        {
            questionRepo.UpdateViewCount(id);
            QuestionViewModel qvm = questionRepo.GetQustionById(id);
            return View(qvm);
        }

        //public ActionResult AddAnswer(int id=1)
        //{
        //    NewAnswerViewModel navm = new NewAnswerViewModel();
        //    navm.QuestionID = id;
        //    return View(navm); 
        //}
        [HttpPost]
        public ActionResult AddAnswer(NewAnswerViewModel navm)
        {
            if (ModelState.IsValid)
            {
                navm.UserID = Convert.ToInt32(Session["CurrentUserId"]);
                navm.VotesCount = 0;
                navm.AnswerDateAndTime = DateTime.Now;
                answerRepo.InsertAnswer(navm);
                int id = navm.QuestionID;
                return RedirectToAction("View", "Question", new { id });
            }
            else
            {
                ModelState.AddModelError("X", "Invalid Entry");
                QuestionViewModel qvm = questionRepo.GetQustionById(navm.QuestionID);
                return View("View",qvm);
            }
            
        }
        [HttpPost]
        public ActionResult Editanswer(EditAnswerViewModel eavm)
        {
            eavm.UserID = Convert.ToInt32(Session["UserId"]);
            answerRepo.UpdateAnswer(eavm);
            int id = eavm.QuestionID;
            return RedirectToAction("View", "Question", new { id });
        }
        
        public ActionResult Create()
        {
            List<CategoryViewModel> categories = categoryRepo.GetCategories();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public ActionResult Create(QuestionViewModel qvm)
        {
            qvm.AnswersCount = 0;
            qvm.VotesCount = 0;
            qvm.ViewsCount = 0;
            qvm.QuestionDateAndTime = DateTime.Now;
            qvm.UserID = Convert.ToInt32(Session["CurrentUserId"]);
            questionRepo.Insert(qvm);
            return RedirectToAction("Questions", "Home");
        }

        // GET: Question
        public ActionResult Index()
        {
            return View();
        }
    }
}