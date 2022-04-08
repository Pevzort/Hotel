using Hotel.Models.Data;
using Hotel.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Hotel.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult Index()
        {
            List<OrdersVM> ordersList;

            using (Db db = new Db())
            {
                ordersList = db.Orders
                    .ToArray()
                    .OrderBy(x => x.Id)
                    .Select(x => new OrdersVM(x))
                    .ToList();
            }

            return View(ordersList);
        }

        public ActionResult DeleteOrders(int id)
        {
            using (Db db = new Db())
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