using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowClone.DomainModels;
using StackOverflowClone.RepositoryLayer;
using StackOverflowClone.ViewModel;

namespace StackOverflowClone.Controllers
{
    public class HomeController : Controller
    {
        
        QuestionRepo questionRepo;
        CategoryRepo categoryRepo;
        
        public HomeController()
        {
            questionRepo = new QuestionRepo();
            categoryRepo = new CategoryRepo();
        }
        public ActionResult Index()
        {
            List<QuestionViewModel> questionList = new List<QuestionViewModel>();
            questionList = questionRepo.GetQuestion().Take(10).ToList();

            return View(questionList);
        }
        public ActionResult Categories()
        {

            List<CategoryViewModel> categoryList = categoryRepo.GetCategories();
            return View(categoryList);
        }
        public ActionResult Questions()
        {
            List<QuestionViewModel> questionList = questionRepo.GetQuestion().ToList();
            return View(questionList);
        }
        public ActionResult Search(string str)
        {
            List<QuestionViewModel> questions = questionRepo.GetQuestion().Where(temp => temp.QuestionName.ToLower().Contains(str.ToLower()) || temp.Category.CategoryName.ToLower().Contains(str.ToLower())).ToList();
            return View(questions);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}