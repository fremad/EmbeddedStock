using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmbeddedStockByPros.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentType> ComponentTypes { get; set; }
        public DbSet<CategoryComponenttypebinding> CategoryComponenttypebindings { get; set; }
        public DbSet<ESImage> EsImages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryComponenttypebinding>()
                .HasKey(bc => new { bc.CategoryId, bc.ComponentTypeId });


            modelBuilder.Entity<CategoryComponenttypebinding>()
                .HasOne(bc => bc.Category)
                .WithMany(b => b.CategoryComponenttypebindings)
                .HasForeignKey(bc => bc.CategoryId);


            modelBuilder.Entity<CategoryComponenttypebinding>()
                .HasOne(bc => bc.ComponentType)
                .WithMany(c => c.CategoryComponenttypebindings)
                .HasForeignKey(bc => bc.ComponentTypeId);
        }

    }
}
