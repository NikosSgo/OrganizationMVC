using Microsoft.EntityFrameworkCore;
using OrganizationMVC.DAL.Entities;

namespace OrganizationMVC.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<OrganizationEntity> Organizations { get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Один-ко-многим: Organization -> Employees
        modelBuilder.Entity<EmployeeEntity>()
            .HasOne(e => e.Organization)
            .WithMany(o => o.Employees)
            .HasForeignKey(e => e.OrganizationId)
            .OnDelete(DeleteBehavior.Cascade); // если организация удаляется, удаляем сотрудников
    }
}
