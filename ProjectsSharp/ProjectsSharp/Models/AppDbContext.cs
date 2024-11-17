namespace ProjectsSharp.Models;

using Microsoft.EntityFrameworkCore;
using Service;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Tache> Taches { get; set; }  
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tache>().ToTable("Taches");
    }
}
