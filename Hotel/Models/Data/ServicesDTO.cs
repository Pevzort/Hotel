using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models.Data
{
    [Table("Services")]
    public class ServicesDTO
    {
        [Key]public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}