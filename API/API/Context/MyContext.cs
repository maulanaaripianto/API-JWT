using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext()
        {
        }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<AccountRoles> AccountRoles { get; set; }
        public DbSet<Roles> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.StatAccount)
                .WithOne(b => b.StatEmployee)
                .HasForeignKey<Account>(b => b.NIK);
            
            modelBuilder.Entity<Account>()
                .HasOne(a => a.StatProfiling)
                .WithOne(b => b.StatAccount)
                .HasForeignKey<Profiling>(b => b.NIK);

            modelBuilder.Entity<Education>()
                .HasMany(c => c.Profilings)
                .WithOne(e => e.Education);

            modelBuilder.Entity<University>()
               .HasMany(c => c.Educations)
               .WithOne(e => e.University);
            
            modelBuilder.Entity<Account>()
                .HasMany(c => c.AccountRoles)
                .WithOne(e => e.Account);

            modelBuilder.Entity<Roles>()
               .HasMany(c => c.AccountRoles)
               .WithOne(e => e.Roles);
        }
    }
}
