using Hotel.Models.Data;
using Hotel.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Hotel.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Services
        public ActionResult Index()
        {
            List<ServicesVM> servicesList;

            using (Db db = new Db())
            {
                servicesList = db.Services
                    .ToArray()
                    .OrderBy(x => x.Id)
                    .Select(x => new ServicesVM(x))
                    .ToList();
            }

            return View(servicesList);
        }

        [HttpGet]
        public ActionResult CreateServices()
        {
            ServicesVM model = new ServicesVM();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateServices(ServicesVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Not valid!");
                return View(model);
            }

            using (Db db = new Db())
            {
                if (db.Services.Any(x => x.Name == model.Name))
                {
                    ModelState.AddModelError("", $"{model.Name} already exist!");
                    return View(model);
                }
            }

            using (Db db = new Db())
            {
                ServicesDTO services = new ServicesDTO();
                services.Name = model.Name;
                services.Price = model.Price;

                db.Services.Add(services);
                db.SaveChanges();
            }

            TempData["msg"] = "Service has been created!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditServices(int id)
        {
            ServicesVM model;

            using (Db db = new Db())
            {
                ServicesDTO dto = db.Services.Find(id);

                model = new ServicesVM()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Price = dto.Price
                };
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditServices(ServicesVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Not valid!");
                return View(model);
            }

            using (Db db = new Db())
            {
                if (db.Services.Any(x => x.Name == model.Name && x.Price == model.Price))
                {
                    ModelState.AddModelError("", $"{model.Name} remained unchanged!");
                    return View(model);
                }
            }

            using (Db db = new Db())
            {
                ServicesDTO services = db.Services.Find(model.Id);
                services.Name = model.Name;
                services.Price = model.Price;

                db.SaveChanges();
            }

            TempData["msg"] = "Service has been edit!";
            return RedirectToAction("Index");
        }

        public ActionResult DeleteServices(int id)
        {
            using (Db db = new Db())
            {
                ServicesDTO dto = db.Services.Find(id);
                db.Services.Remove(dto);
                db.SaveChanges();
            }
            TempData["msg"] = "You deleted service!";
            return RedirectToAction("Index");
        }
    }
}