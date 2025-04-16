using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MoviesApi.Models
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Movie>()
                .Ignore(x => x.StoreLine);

        }
        public DbSet<Genra> Genras { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
