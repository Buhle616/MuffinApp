using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MuffinApp.Models
{
    public class MuffinDbcontext : DbContext
    {
        public MuffinDbcontext() : base("MuffinDB") { }
        public DbSet<MuffinItems> muffinitem { get; set; }
        public DbSet<Orders> orders { get; set; }
    }
}