using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace eTickets.Data
{
    public class AppDbContext : DbContext
    {
        private static readonly string DB_NAME = @"eTickets";
        private static readonly string DB_SERVER = @"LAPTOP-35V804DV";
        private static readonly string DB_USERNAME = @"LAPTOP-35V804DV/Jayed";
        private static readonly string DB_PASSWORD = @"";

        private static readonly string _connectionString = $@"Server={DB_SERVER};Database={DB_NAME};User Id={DB_USERNAME};Password={DB_PASSWORD};Trusted_Connection=True;TrustServerCertificate=true;";
        private readonly string _migrationAssembly;
        public AppDbContext()
        {
            _migrationAssembly = Assembly.GetExecutingAssembly().GetName().Name;
         
        }

        public AppDbContext(DbContextOptions<AppDbContext> options, string migrationAssembly) : base(options)
        {
            _migrationAssembly = migrationAssembly;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString, (x) => x.MigrationsAssembly(_migrationAssembly));
                base.OnConfiguring(optionsBuilder);
            }
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.ActorId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
    }
}
