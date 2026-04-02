// Infrastructure/Persistence/AppDbContext.cs
using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
// using EmployeeManagement.Domain.Entities;

public class AppDbContext : DbContext
{
    public DbSet<Employee> Employees => Set<Employee>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Email).IsUnique();

            // Use snake_case for PostgreSQL (optional but recommended)
            entity.ToTable("employees");
        });
    }
}