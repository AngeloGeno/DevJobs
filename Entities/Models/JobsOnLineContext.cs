using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models;

public partial class JobsOnLineContext : DbContext
{
    public JobsOnLineContext()
    {
    }

    public JobsOnLineContext(DbContextOptions<JobsOnLineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Applicant> Applicants { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<ApplicationStatus> ApplicationStatuses { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<QualificationLevel> QualificationLevels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-P0GCM4T\\SQLEXPRESS;Initial Catalog=JobsOnLine;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Applicant>(entity =>
        {
            entity.ToTable("Applicant");

            entity.Property(e => e.ApplicantId).HasColumnName("ApplicantID");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.QualificationLevelId).HasColumnName("QualificationLevelID");

            entity.HasOne(d => d.QualificationLevel).WithMany(p => p.Applicants)
                .HasForeignKey(d => d.QualificationLevelId)
                .HasConstraintName("FK_Applicant_QualificationLevel");
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.ToTable("Application");

            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.ApplicantId).HasColumnName("ApplicantID");
            entity.Property(e => e.ApplicationStatusId).HasColumnName("ApplicationStatusID");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.JobId).HasColumnName("JobID");

            entity.HasOne(d => d.Applicant).WithMany(p => p.Applications)
                .HasForeignKey(d => d.ApplicantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_Applicant");

            entity.HasOne(d => d.ApplicationStatus).WithMany(p => p.Applications)
                .HasForeignKey(d => d.ApplicationStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_ApplicationStatus");

            entity.HasOne(d => d.Job).WithMany(p => p.Applications)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_Job");
        });

        modelBuilder.Entity<ApplicationStatus>(entity =>
        {
            entity.HasKey(e => e.ApplicationSatusId);

            entity.ToTable("ApplicationStatus");

            entity.Property(e => e.ApplicationSatusId).HasColumnName("ApplicationSatusID");
            entity.Property(e => e.ApplicationStatusType).HasMaxLength(50);
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.ToTable("Job");

            entity.Property(e => e.JobId).HasColumnName("JobID");
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.JobTitle).HasMaxLength(50);
            entity.Property(e => e.PositionLevel).HasMaxLength(50);
        });

        modelBuilder.Entity<QualificationLevel>(entity =>
        {
            entity.ToTable("QualificationLevel");

            entity.Property(e => e.QualificationLevelId).HasColumnName("QualificationLevelID");
            entity.Property(e => e.QualificationLevelName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
