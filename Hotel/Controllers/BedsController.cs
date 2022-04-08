using Hotel.Models.Data;
using Hotel.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Hotel.Controllers
{
    public class BedsController : Controller
    {
        // GET: Beds
        public ActionResult Index()
        {
            List<BedsVM> bedsList;

            using (Db db = new Db())
            {
                bedsList = db.Beds
                    .ToArray()
                    .OrderBy(x => x.Id)
                    .Select(x => new BedsVM(x))
                    .ToList();
            }

            return View(bedsList);
        }

        [HttpGet]
        public ActionResult CreateBeds()
        {
            BedsVM model = new BedsVM();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateBeds(BedsVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Not valid!");
                return View(model);
            }

            using (Db db = new Db())
            {
                if (db.Beds.Any(x=>x.Name == model.Name))
                {
                    ModelState.AddModelError("", $"{model.Name} already exist!");
                    return View(model);
                }
            }

            using (Db db = new Db())
            {
                BedsDTO beds = new BedsDTO();
                beds.Name = model.Name;

                db.Beds.Add(beds);
                db.SaveChanges();
            }

            TempData["msg"] = "Bed has been created!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditBeds(int id)
        {
            BedsVM model;

            using (Db db = new Db())
            {
                BedsDTO dto = db.Beds.Find(id);

                model = new BedsVM()
                {
                    Id = dto.Id,
                    Name = dto.Name
                };
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditBeds(BedsVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Not valid!");
                return View(model);
            }

            using (Db db = new Db())
            {
                if (db.Beds.Any(x=>x.Name == model.Name))
                {
                    ModelState.AddModelError("", $"{model.Name} remained unchanged!");
                    return View(model);
                }
            }

            using (Db db = new Db())
            {
                BedsDTO beds = db.Beds.Find(model.Id);
                beds.Name = model.Name;

                db.SaveChanges();
            }

            TempData["msg"] = "Bed has been edit!";
            return RedirectToAction("Index");
        }

        public ActionResult DeleteBeds(int id)
        {
            using (Db db = new Db())
            {
                BedsDTO dto = db.Beds.Find(id);
                db.Beds.Remove(dto);
                db.SaveChanges();
            }
            TempData["msg"] = "You deleted bed!";
            return RedirectToAction("Index");
        }
    }
}