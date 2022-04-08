using Hotel.Models.Data;

namespace Hotel.Models.ViewModel
{
    public class BedsVM
    {
        public BedsVM()
        { }
        public BedsVM(BedsDTO row)
        {
            Id = row.Id;
            Name = row.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}