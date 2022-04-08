using Hotel.Models.Data;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Models.ViewModel
{
    public class ServicesVM
    {
        public ServicesVM()
        {

        }

        public ServicesVM(ServicesDTO row)
        {
            Id = row.Id;
            Name = row.Name;
            Price = row.Price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}