using Hotel.Models.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Models.ViewModel
{
    public class RoomsVM
    {
        public RoomsVM()
        {

        }

        public RoomsVM(RoomsDTO row)
        {
            Id = row.Id;
            Size = row.Size;
            Capacity = row.Capacity;
            BedsId = row.BedsId;
            ServicesId = row.ServicesId;
            Price = row.Price;
            State = row.State;
        }

        [DisplayName("RoomNumber")] public int Id { get; set; }
        public int Size { get; set; }
        public int Capacity { get; set; }
        public int BedsId { get; set; }
        public int ServicesId { get; set; }
        public int Price { get; set; }
        public string State { get; set; }

        public string Bed { get; set; }
        public string Service { get; set; }
    }
}