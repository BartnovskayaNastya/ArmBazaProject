using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ArmBazaProject.BDModels;
using ArmBazaProject.Models;

namespace ArmBazaProject
{
    class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Category> Categories {get; set;}
        public DbSet<Points> Points { get; set; }

    }
}
