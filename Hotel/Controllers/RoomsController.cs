using Hotel.Models.Data;
using Hotel.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Hotel.Controllers
{
    public class RoomsController : Controller
    {
        // GET: Rooms
        public ActionResult Index()
        {
            List<RoomsVM> roomsList;

            using (Db db = new Db())
            {
                roomsList = db.Rooms
                    .ToArray()
                    .OrderBy(x => x.Id)
                    .Select(x => new RoomsVM(x))
                    .ToList();
            }

            return View(roomsList);
        }

        [HttpGet]
        public ActionResult CreateRooms()
        {
            RoomsVM model = new RoomsVM();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateRooms(RoomsVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Not valid!");
                return View(model);
            }

            using (Db db = new Db())
            {
                RoomsDTO rooms = new RoomsDTO();
                rooms.Size = model.Size;
                rooms.Capacity = model.Capacity;
                rooms.BedsId = model.BedsId;
                rooms.ServicesId = model.ServicesId;
                rooms.Price = model.Price;
                rooms.State = "Free";

                db.Rooms.Add(rooms);
                db.SaveChanges();
            }

            TempData["msg"] = "Room has been created!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditRooms(int id)
        {
            RoomsVM model;

            using (Db db = new Db())
            {
                RoomsDTO dto = db.Rooms.Find(id);

                model = new RoomsVM()
                {
                    Id = dto.Id,
                    Size = dto.Size,
                    Capacity = dto.Capacity,
                    BedsId = dto.BedsId,
                    ServicesId = dto.ServicesId,
                    Price = dto.Price,
                    State = dto.State
                };
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditRooms(RoomsVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Not valid!");
                return View(model);
            }

            using (Db db = new Db())
            {
                if (db.Rooms.Any(x => x.Size == model.Size && x.Capacity == model.Capacity && x.BedsId == model.BedsId && x.ServicesId == model.ServicesId && x.Price == model.Price))
                {
                    ModelState.AddModelError("", $"{model.Id} remained unchanged!");
                    return View(model);
                }
            }

            using (Db db = new Db())
            {
                RoomsDTO rooms = db.Rooms.Find(model.Id);
                rooms.Size = model.Size;
                rooms.Capacity = model.Capacity;
                rooms.BedsId = model.BedsId;
                rooms.ServicesId = model.ServicesId;
                rooms.Price = model.Price;
                rooms.State = "Free";

                db.SaveChanges();
            }

            TempData["msg"] = "Room has been edit!";
            return RedirectToAction("Index");
        }

        public ActionResult DeleteRooms(int id)
        {
            using (Db db = new Db())
            {
                RoomsDTO dto = db.Rooms.Find(id);
                db.Rooms.Remove(dto);
                db.SaveChanges();
            }
            TempData["msg"] = "You deleted room!";
            return RedirectToAction("Index");
        }
    }
}