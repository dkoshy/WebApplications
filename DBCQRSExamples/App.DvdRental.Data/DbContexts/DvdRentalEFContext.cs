
using App.DvdRental.Domain.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace App.DvdRental.Data.DbContexts
{

    public class DvdRentalEFContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DvdRentalEFContext(DbContextOptions<DvdRentalEFContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>
            (c =>{
                c.Property(p=>p.Category_Id).HasColumnName("category_id");
                c.Property(p=> p.Name).HasColumnName("name");
                c.Property(p=>p.Last_Update).HasColumnName("last_update");
                c.ToTable("category");
            });
                
                 
                
                
                        
        }
    }

}
