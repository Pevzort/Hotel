using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models.Data
{
    [Table("Orders")]
    public class OrdersDTO
    {
        [Key]public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public string StartOrder { get; set; }
        public string EndOrder { get; set; }
        public int Price { get; set; }
    }
}