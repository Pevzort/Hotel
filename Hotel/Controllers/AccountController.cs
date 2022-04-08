using Hotel.Models.Data;
using Hotel.Models.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace Hotel.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Register()
        {
            UsersVM model = new UsersVM();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(UsersVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "No valid");
                return View(model);
            }

            using (Db db = new Db())
            {
                if (db.Users.Any(x=>x.Nickname == model.Nickname))
                {
                    ModelState.AddModelError("", $"{model.Nickname} is exist!");
                    return View(model);
                }

                if (db.Users.Any(x=>x.Email == model.Email))
                {
                    ModelState.AddModelError("", $"{model.Email} is exist!");
                    return View(model);
                }
            }

            if (!model.Password.Equals(model.ConfirmPassword))
            {
                ModelState.AddModelError("", "Password do not match!");
                return View(model);
            }

            using (Db db = new Db())
            {
                UsersDTO users = new UsersDTO();
                users.Nickname = model.Nickname;
                users.Password = model.Password;
                users.Fname = model.Fname;
                users.Lname = model.Lname;
                users.Birthday = model.Birthday;
                users.Email = model.Email;
                users.Role = "User";

                db.Users.Add(users);
                db.SaveChanges();
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            LoginVM model = new LoginVM();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "No valid");
                return View(model);
            }

            using (Db db = new Db())
            {
                if (db.Users.Any(x=>x.Nickname == x.Nickname && model.Password == model.Password))
                {
                    var users = db.Users.FirstOrDefault(x => x.Nickname == model.Nickname && x.Password == model.Password);
                    if (users != null)
                    {
                        Session["UserId"] = users.Id.ToString();
                        Session["Nickname"] = users.Nickname.ToString();
                        Session["Role"] = users.Role.ToString();

                        return RedirectToAction("LoggedIn");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Имя пользователя или пароль неверны!");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Имя пользователя или пароль неверны!");
                    return View(model);
                }
            }
        }

        public ActionResult LoggedIn()
        {
            if (Session["UserId"] != null)
            {
                return Redirect("/Home/Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}