using Hotel.Models.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Models.ViewModel
{
    public class UsersVM
    {
        public UsersVM()
        {

        }

        public UsersVM(UsersDTO row)
        {
            Id = row.Id;
            Nickname = row.Nickname;
            Password = row.Password;
            Fname = row.Fname;
            Lname = row.Lname;
            Birthday = row.Birthday;
            Email = row.Email;
            Role = row.Role;
        }

        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }
}