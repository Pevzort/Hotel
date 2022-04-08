using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hotel.Models.Data
{
    public class Db : DbContext
    {
        public Db() : base("name=Db")
        {

        }

        public DbSet<BedsDTO> Beds { get; set; }
        public DbSet<OrdersDTO> Orders { get; set; }
        public DbSet<RoomsDTO> Rooms { get; set; }
        public DbSet<ServicesDTO> Services { get; set; }
        public DbSet<UsersDTO> Users { get; set; }

        public DbSet<Hotel.Models.ViewModel.BedsVM> BedsVMs { get; set; }

        public DbSet<Hotel.Models.ViewModel.ServicesVM> ServicesVMs { get; set; }

        public DbSet<Hotel.Models.ViewModel.RoomsVM> RoomsVMs { get; set; }

        public DbSet<Hotel.Models.ViewModel.OrdersVM> OrdersVMs { get; set; }

        public DbSet<Hotel.Models.ViewModel.UsersVM> UsersVMs { get; set; }
    }
}