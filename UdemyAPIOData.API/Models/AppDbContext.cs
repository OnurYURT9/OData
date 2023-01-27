using Microsoft.EntityFrameworkCore;
using System;

namespace UdemyAPIOData.API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
             {

             }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
 
}