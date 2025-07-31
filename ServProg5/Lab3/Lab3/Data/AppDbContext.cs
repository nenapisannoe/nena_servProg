using Lab3.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Lab3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Testimonial> Testimonials { get; set; }
    }
}
    