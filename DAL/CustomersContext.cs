using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class CustomersContext : DbContext
    {
        public DbSet<Customer> Customer { get; set;  }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }

        public CustomersContext(DbContextOptions<CustomersContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.CustomerID)
                .HasDatabaseName("CustomerCode")
                .IsUnique();

                entity.HasMany(d => d.Invoice);


                entity.Property(e => e.FirstName)
                .HasColumnType("varchar(60)");
            });
        }
    }
}
