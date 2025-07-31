using RestAPI8.Models;
using Microsoft.EntityFrameworkCore;

namespace RestAPI8.Data
{
    public class UserContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public UserContext()
        {

        }
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users", "superhero");   
                entity.Property(e => e.Name).HasColumnName("name").IsRequired();
                entity.Property(e => e.Password).HasColumnName("password").IsRequired();
                entity.Property(e => e.Role).HasColumnName("role").IsRequired();
            });
        }
    }
}
