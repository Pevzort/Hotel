using Hotel.Models.Data;
using Hotel.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Hotel.Controllers
{
    public class UserOrderController : Controller
    {
        // GET: UserOrder
        public ActionResult Index()
        {
            List<OrdersVM> ordersList;

            using (Db db = new Db())
            {
                ordersList = db.Orders
                    .ToArray()
                    .OrderBy(x => x.Id)
                    .Where(x=>x.UserId == Convert.ToInt32(Session["UserId"]))
                    .Select(x => new OrdersVM(x))
                    .ToList();
            }

            return View(ordersList);
        }

        public ActionResult RoomsList()
        {
            List<RoomsVM> roomsList;

            using (Db db = new Db())
            {
                roomsList = db.Rooms
                    .ToArray()
                    .OrderBy(x => x.ServicesId)
                    .Where(x=>x.State=="Free")
                    .Select(x => new RoomsVM(x))
                    .ToList();

                foreach(RoomsVM room in roomsList)
                {
                    room.Bed = db.Beds.Find(room.BedsId).Name;
                    room.Service = db.Services.Find(room.ServicesId).Name;
                }

            }

            return View(roomsList);
        }

        [HttpGet]
        public ActionResult CreateOrder(int id)
        {
            OrdersVM model = new OrdersVM();

            List<string> dates = new List<string>()
            {
                DateTime.Now.Day.ToString() +":"+ DateTime.Now.Month.ToString() +":"+ DateTime.Now.Year.ToString(),
                (DateTime.Now.Day+1).ToString() +":"+ DateTime.Now.Month.ToString() +":"+ DateTime.Now.Year.ToString(),
                (DateTime.Now.Day+2).ToString() +":"+ DateTime.Now.Month.ToString() +":"+ DateTime.Now.Year.ToString(),
                (DateTime.Now.Day+3).ToString() +":"+ DateTime.Now.Month.ToString() +":"+ DateTime.Now.Year.ToString(),
                (DateTime.Now.Day+4).ToString() +":"+ DateTime.Now.Month.ToString() +":"+ DateTime.Now.Year.ToString(),
                (DateTime.Now.Day+5).ToString() +":"+ DateTime.Now.Month.ToString() +":"+ DateTime.Now.Year.ToString(),
            };

            model.StartDate = new SelectList(dates);
            model.EndDate = new SelectList(dates);

            using (Db db= new Db())
            {
                model.UserId = Convert.ToInt32(Session["UserId"]);
                model.RoomId = id;
                model.StartOrder = DateTime.Now.Day.ToString() + ":" + DateTime.Now.Month.ToString() + ":" + DateTime.Now.Year.ToString();
                model.EndOrder = (DateTime.Now.Day + 1).ToString() + ":" + DateTime.Now.Month.ToString() + ":" + DateTime.Now.Year.ToString();
                model.Price = 0;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateOrder(OrdersVM model)
        {
            if (Convert.ToInt32(model.EndOrder.Split(':')[0]) - Convert.ToInt32(model.StartOrder.Split(':')[0]) < 1) 
            {
                ModelState.AddModelError("", "");
                return View(model);
            }

            using (Db db = new Db()) 
            {
                OrdersDTO order = new OrdersDTO();


                order.UserId = Convert.ToInt32(Session["UserId"]);
                order.RoomId = model.RoomId;
                order.StartOrder = model.StartOrder;
                order.EndOrder = model.EndOrder;
                order.Price = (Convert.ToInt32(model.EndOrder.Split(':')[0]) - Convert.ToInt32(model.StartOrder.Split(':')[0])) * 
                               db.Rooms.Find(model.RoomId).Price +
                               (Convert.ToInt32(model.StartOrder.Split(':')[0]) * db.Services.Find(db.Rooms.Find(model.RoomId).ServicesId).Price);
                

                db.Orders.Add(order);
                db.SaveChanges();


                RoomsDTO room = db.Rooms.Find(model.RoomId);
                room.State = "Ordered";
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteOrder(int id)
        {
            using (Db db= new Db())
            {
                OrdersDTO dto = db.Orders.Find(id);
                RoomsDTO room = db.Rooms.Find(dto.RoomId);
                room.State = "Free";
                db.Orders.Remove(dto);
                db.SaveChanges();
            }
            TempData["msg"] = "You deleted order!";
            return RedirectToAction("Index");
        }
    }
}