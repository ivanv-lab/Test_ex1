using Microsoft.EntityFrameworkCore;

namespace Test_ex.Models
{
    public class AppDbContext:DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                _configuration.GetConnectionString("SQLServerConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Region)
                .WithMany(r => r.Patients)
                .HasForeignKey(p => p.RegionId);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Region)
                .WithMany(r => r.Doctors)
                .HasForeignKey(p => p.RegionId);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Specialization)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecializationId);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Cabinet)
                .WithMany(c => c.Doctors)
                .HasForeignKey(d => d.CabinetId);
        }
    }
}
