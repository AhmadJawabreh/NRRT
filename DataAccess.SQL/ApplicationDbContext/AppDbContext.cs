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

            modelBuilder.Entity<TeamEntity>().HasQueryFilter(item => !item.IsDeleted);

            modelBuilder.Entity<TeamMemberEntity>().HasQueryFilter(item => !item.IsDeleted);

            modelBuilder.Entity<TeamEntity>()
                .HasMany(e => e.TeamMembers)
                .WithOne(e => e.TeamEntity)
                .HasForeignKey(e => e.TeamId)
                .IsRequired();

            modelBuilder.Entity<PatientEntity>()
                .HasOne(entity => entity.PatientHistory)
                .WithOne(entity => entity.Patient)
                .HasForeignKey<PatientHistoryEntity>(entity => entity.PatientId);


            modelBuilder.Entity<AcuteKidneyInjuryRiskAssessmentEntity>()
                .HasOne(entity => entity.Patient)
                .WithOne(entity => entity.AcuteKidneyInjuryRiskAssessmentEntity)
                .HasForeignKey<AcuteKidneyInjuryRiskAssessmentEntity>(entity => entity.PatientId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:nrrt-db-sql-server.database.windows.net,1433;Initial Catalog=nrrt;Persist Security Info=False;User ID=nrrt;Password=Hot1007128427;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<TeamEntity> TeamEntities { get; set; }

        public DbSet<TeamMemberEntity> TeamMemberEntities { get; set; }

        public DbSet<PatientEntity> PatientEntities { get; set; }

        public DbSet<PatientMovementEntity> PatientMovementEntities { get; set; }

        public DbSet<PatientHistoryEntity> PatientHistoryEntities { get; set; }

        public DbSet<AcuteKidneyInjuryRiskAssessmentEntity> AcuteKidneyInjuryRiskAssessmentEntities { get; set; }
    }
}
