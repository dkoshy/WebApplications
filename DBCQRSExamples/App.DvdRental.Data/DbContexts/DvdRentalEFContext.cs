
using App.DvdRental.Domain.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace App.DvdRental.Data.DbContexts
{

    public class DvdRentalEFContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
    }

}
