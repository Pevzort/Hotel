using Hotel.Models.Data;
using Hotel.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Hotel.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            List<UsersVM> usersList;

            using (Db db = new Db())
            {
                usersList = db.Users
                    .ToArray()
                    .OrderBy(x => x.Id)
                    .Where(x=> x.Id != Convert.ToInt32(Session["UserId"]))
                    .Select(x => new UsersVM(x))
                    .ToList();
            }

            return View(usersList);
        }

        [HttpGet]
        public ActionResult EditUsers(int id)
        {
            UsersVM model;

            using (Db db = new Db())
            {
                UsersDTO dto = db.Users.Find(id);

                model = new UsersVM()
                {
                    Id = dto.Id,
                    Nickname = dto.Nickname,
                    Password = dto.Password,
                    Fname = dto.Fname,
                    Lname = dto.Lname,
                    Birthday = dto.Birthday,
                    Email = dto.Email,
                    Role = dto.Role
                };
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditUsers(UsersVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Not valid!");
                return View(model);
            }

            using (Db db = new Db())
            {
                if (db.Users.Any(x=>x.Role == model.Role))
                {
                    ModelState.AddModelError("", $"{model.Fname} {model.Lname} remained unchanged!");
                    return View(model);
                }
            }

            using (Db db = new Db())
            {
                UsersDTO users = db.Users.Find(model.Id);
                users.Role = model.Role;

                db.SaveChanges();
            }

            TempData["msg"] = "User has been edit!";
            return RedirectToAction("Index");
        }

        public ActionResult DeleteUsers(int id)
        {
            using (Db db = new Db())
            {
                UsersDTO dto = db.Users.Find(id);
                db.Users.Remove(dto);
                db.SaveChanges();
            }
            TempData["msg"] = "You deleted user!";
            return RedirectToAction("Index");
        }
    }
}