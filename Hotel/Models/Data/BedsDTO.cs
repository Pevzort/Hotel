using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models.Data
{
    [Table("Beds")]
    public class BedsDTO
    {
        [Key]public int Id { get; set; }
        public string Name { get; set; }
    }
}