using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Domain.Model;


namespace PrimeiraAPI.Infrastructure
{
    public class ConnectionContext: DbContext
    {
       
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlServer(
              "Server=CHRYSTIAN-0\\SQLEXPRESS;" +
              "Database=empresadb;" +
              "User Id=sa;" +
              "Password=123456;" +
              "TrustServerCertificate=True");
        
    }
}
