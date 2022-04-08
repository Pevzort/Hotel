using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models.Data
{
    [Table("Rooms")]
    public class RoomsDTO
    {
        [Key]public int Id { get; set; }
        public int Size { get; set; }
        public int Capacity { get; set; }
        public int BedsId { get; set; }
        public int ServicesId { get; set; }
        public int Price { get; set; }
        public string State { get; set; }
    }
}