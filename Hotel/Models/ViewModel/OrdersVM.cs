using Hotel.Models.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Hotel.Models.ViewModel
{
    public class OrdersVM
    {
        public OrdersVM()
        {

        }

        public OrdersVM(OrdersDTO row)
        {
            Id = row.Id;
            UserId = row.UserId;
            RoomId = row.RoomId;
            StartOrder = row.StartOrder;
            EndOrder = row.EndOrder;
            Price = row.Price;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        [DisplayName("RoomNumber")] public int RoomId { get; set; }
        public string StartOrder { get; set; }
        public string EndOrder { get; set; }
        public int Price { get; set; }

        public IEnumerable<SelectListItem> StartDate { get; set; }
        public IEnumerable<SelectListItem> EndDate { get; set; }
    }
}