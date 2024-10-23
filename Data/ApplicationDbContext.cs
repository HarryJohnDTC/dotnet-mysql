using Microsoft.EntityFrameworkCore;
using DotnetMysql.Models;

namespace DotnetMysql.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<User> Users { get; set; }  // Ajout de la table Users
    }
}
