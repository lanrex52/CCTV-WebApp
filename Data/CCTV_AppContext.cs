using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CCTV_App.Models
{
    public class CCTV_AppContext : DbContext
    {
        public CCTV_AppContext (DbContextOptions<CCTV_AppContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Audit> Audits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().ToTable("User");
            //modelBuilder.Entity<Audit>().ToTable("Audit");
           
        }
    }
}
