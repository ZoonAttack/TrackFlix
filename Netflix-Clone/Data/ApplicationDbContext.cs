using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Netflix_Clone.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<UserMovie> UserMovies { get; set; }

    public DbSet<Show> Shows { get; set; }

    public DbSet<UserShow> UserShows { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserMovie>()
            .HasKey(um => new { um.UserId, um.MovieId });

        builder.Entity<UserMovie>()
            .HasOne(um => um.User)
            .WithMany(u => u.Movies)
            .HasForeignKey(um => um.UserId);

        builder.Entity<UserMovie>()
            .HasOne(um => um.Movie)
            .WithMany(m => m.UserMovies)
            .HasForeignKey(um => um.MovieId);

        builder.Entity<UserShow>()
           .HasKey(um => new { um.UserId, um.ShowId });

        builder.Entity<UserShow>()
            .HasOne(um => um.User)
            .WithMany(u => u.Shows)
            .HasForeignKey(um => um.UserId);

        builder.Entity<UserShow>()
            .HasOne(um => um.Show)
            .WithMany(m => m.UserShows)
            .HasForeignKey(um => um.ShowId);
    }
}
