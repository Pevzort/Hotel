using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models.Data
{
    [Table("Users")]
    public class UsersDTO
    {
        [Key]public int Id { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}