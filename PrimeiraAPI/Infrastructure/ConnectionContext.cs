using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Domain.Model.CompanyAggregate;
using PrimeiraAPI.Domain.Model.EmployeeAggregate;

namespace PrimeiraAPI.Infrastructure
{
    public class ConnectionContext: DbContext
    {
       
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlServer(
              "Server=CHRYSTIAN-0\\SQLEXPRESS;" +
              "Database=empresadb;" +
              "User Id=sa;" +
              "Password=123456;" +
              "TrustServerCertificate=True");
        
    }
}
