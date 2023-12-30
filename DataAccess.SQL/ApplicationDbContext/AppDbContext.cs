/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using DataAccess.SQL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.SQL.ApplicationDbContext
{
    #nullable disable
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PatientEntity>().HasQueryFilter(item => !item.IsDeleted);

            modelBuilder.Entity<PatientMovementEntity>().HasQueryFilter(item => !item.IsDeleted);

            modelBuilder.Entity<PatientEntity>()
                .HasOne(entity => entity.PatientHistory)
                .WithOne(entity => entity.Patient)
                .HasForeignKey<PatientHistoryEntity>(entity => entity.PatientId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=NRRT;Trusted_Connection=true; MultipleActiveResultSets=true");
        }

        public DbSet<PatientEntity> PatientEntities { get; set; }
        public DbSet<PatientMovementEntity> PatientMovementEntities { get; set; }
        public DbSet<PatientHistoryEntity> PatientHistoryEntities { get; set; }
    }
}
