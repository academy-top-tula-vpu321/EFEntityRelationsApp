using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFEntityRelationsApp
{
    public class CompaniesContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer(@"Data Source=Y5-0\MSSQLSERVER01;Initial Catalog=CompaniesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Passport>()
            //            .HasKey(p => new { p.Series, p.Number });

            //modelBuilder.Entity<Employee>()
            //            .HasOne(e => e.Passport)
            //            .WithOne(p => p.Employee)
            //            .HasForeignKey<Passport>(p => p.Id);
            //            //.HasForeignKey<Passport>(p => p.EmployeeId);

            //modelBuilder.Entity<Employee>()
            //            .ToTable("Employees");
            //modelBuilder.Entity<Passport>()
            //            .ToTable("Employees");

            modelBuilder.Entity<Employee>()
                        .OwnsOne(e => e.Passport);


            modelBuilder.Entity<Employee>()
                        .HasOne(e => e.Company)
                        .WithMany(c => c.Employees)
                        .HasForeignKey(e => e.CompanyId)
                        .OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<Employee>()
            //            .HasMany(e => e.Projects)
            //            .WithMany(p => p.Employees)
            //            .UsingEntity(ne => ne.ToTable("EmployeeProject"));

            modelBuilder.Entity<Project>()
                        .HasMany(p => p.Employees)
                        .WithMany(e => e.Projects)
                        .UsingEntity<EmployeeProject>(
                            l => l.HasOne(ep => ep.Employee)
                                  .WithMany(e => e.EmployeeProjects)
                                  .HasForeignKey(ep => ep.EmployeeId),
                            r => r.HasOne(ep => ep.Project)
                                  .WithMany(p => p.EmployeeProjects)
                                  .HasForeignKey(ep => ep.ProjectId),
                            m =>
                            {
                                m.Property(ep => ep.StartDate).HasDefaultValueSql("GETDATE()");
                                m.HasKey(i => new { i.EmployeeId, i.ProjectId });
                        });


            modelBuilder.Entity<Company>(
                builder => { builder.ComplexProperty(c => c.Language); }
                );
            modelBuilder.Entity<Project>(
                builder => { builder.ComplexProperty(p => p.Language); }
                );
                        

            //modelBuilder.Entity<Employee>()
            //            .Property(e => e.Company)
            //            .IsRequired();
        }
    }
}
