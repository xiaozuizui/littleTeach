using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using littlemm.teach.Base;

namespace littlemm.teach.Model
{
    public class ClassDb : DbContext
    {

        

        public DbSet<Student> Students { set; get; }
       
        public DbSet<Class> Classes { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite("Filename=classInfo.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(c => c.Id);

        }
    }
}
