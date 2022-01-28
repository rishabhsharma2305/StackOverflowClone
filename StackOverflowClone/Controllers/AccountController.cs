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
    public class AccountController : Controller
    {
        UserRepo userRepo;
        public AccountController()
        {
             userRepo = new UserRepo();
        }
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserViewModel uvm)
        {
            int uid = userRepo.Insert(uvm);
            Session["CurrentUserID"] = uid;
            Session["CurrentUserName"] = uvm.UserName;
            Session["CurrentUserEmail"] = uvm.Email;
            Session["CurrentUserPassword"] = uvm.Password;
            Session["CurrentUserIsAdmin"] = false;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                User user = userRepo.GetUserByEmailPass(lvm.Email, lvm.Password);
                if (user != null)
                {
                    Session["CurrentUserID"] = user.UserID;
                    Session["CurrentUserName"] = user.UserName;
                    Session["CurrentUserEmail"] = user.Email;
                    Session["CurrentUserPassword"] = user.Password;
                    Session["CurrentUserIsAdmin"] = user.IsAdmin;

                    if (user.IsAdmin)
                    {
                        return RedirectToRoute(new { controller = "Home", action = "Index" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return View(lvm);
                }
            }
            else
            {
                ModelState.AddModelError("X", "Invalid details");
                return RedirectToAction("Login", "Account");
            }
            
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }

        public ActionResult EditUserDetails()
        {
            int uid = Convert.ToInt32(Session["CurrentUserId"]);
            User user = userRepo.GetUserById(uid);
            EditUserDetailViewModel eudvm = new EditUserDetailViewModel() 
            { name=user.UserName, email=user.Email,mobile=user.Mobile,id=user.UserID};
            return View(eudvm);
        }
        [HttpPost]
        public ActionResult EditUserDetails(EditUserDetailViewModel eudvm)
        {
            if (ModelState.IsValid)
            {
                eudvm.id = Convert.ToInt32(Session["CurrentUserId"]);
                userRepo.UpdateDetails(eudvm);
                Session["CurrentUserName"] = eudvm.name;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("X", "Invalid details");
                return View(eudvm);
            }
            
        }
        public ActionResult ChangePassword()
        {
            int uid = Convert.ToInt32(Session["CurrentUserId"]);
            User user = userRepo.GetUserById(uid);
            EditPasswordViewModel epvm = new EditPasswordViewModel()
            { UserID=user.UserID, Password="", ConfirmPassword="", Email=user.Email};
            return View(epvm);
        }
        [HttpPost]
        public ActionResult ChangePassword(EditPasswordViewModel epvm)
        {
            if (ModelState.IsValid)
            {
                epvm.UserID = Convert.ToInt32(Session["CurrentUserId"]);
                userRepo.UpdatePassword(epvm);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("X", "Invalid Password");
                return View(epvm);
            }
        }

    }
}