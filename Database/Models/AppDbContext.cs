using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Database.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<PostModel> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>().ToTable("Users");
        modelBuilder.Entity<PostModel>().ToTable("Posts");
    }
}