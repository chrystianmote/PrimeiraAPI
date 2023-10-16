using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Domain.Model.CompanyAggregate;
using PrimeiraAPI.Domain.Model.EmployeeAggregate;

namespace PrimeiraAPI.Infrastructure
{
    public class ConnectionContext : DbContext
    {
        public ConnectionContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder mbuilder)
        {
            mbuilder.Entity<Employee>(e =>
            {
                e.ToTable("Employee");

                e.HasKey(em => em.Id);

                e.Property(em => em.Name)
                .HasMaxLength(200)
                .HasColumnType("varchar(200)");

                e.Property(em => em.Age)
                .HasColumnType("int");

                e.Property(em => em.Photo);

            });

            mbuilder.Entity<Company>(e =>
            {
                e.ToTable("Company");

                e.HasKey(cp => cp.Id);

                e.Property(cp => cp.Name)
                .HasMaxLength(200)
                .HasColumnType("varchar(200)");

                e.Property(cp => cp.StartDate)
                .HasColumnName("Start_Date");


            });
        }
    }
}
