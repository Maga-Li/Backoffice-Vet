using Microsoft.EntityFrameworkCore;
using ProjectoFinal_LR.Models;

namespace ProjectoFinal_LR.DatabaseContext
{
    public class MainDbContext : DbContext 
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Veterinarian> Veterinarians { get; set; }
        public DbSet<Specie> Species { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Motive> Motives { get; set; }
        public DbSet<Priority> Priorities { get; set; }

    }

}
