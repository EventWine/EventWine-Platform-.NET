
using ElixirControlPlatform.API.IAM.Domain.Model.Aggregates;
using ElixirControlPlatform.API.Profiles.Domain.Model.Aggregate;
using ElixirControlPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Aggregate;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Entities;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ElixirControlPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
   protected override void OnConfiguring(DbContextOptionsBuilder builder)
   {
      //Para campos de auditor (CreatedDate, UpdatedDate)
      builder.AddCreatedUpdatedInterceptor();
      base.OnConfiguring(builder);
   }
   
   protected override void OnModelCreating(ModelBuilder builder)
   {
      base.OnModelCreating(builder);
      
      //=================================================================================================
      //||                                    CONFIGURATION OF THE TABLES                              ||                              
      //=================================================================================================
      
      //=================================================================================================
      //===================================== 1. GONZALO BOUNDED CONTEXT ================================
      //---------------- CONFIGURATION BY BATCH ----------------
      builder.Entity<Batch>().HasKey(b => b.Id);
      builder.Entity<Batch>().Property(b => b.Id).IsRequired().ValueGeneratedOnAdd();
      
      builder.Entity<Batch>().Property(b => b.ProfileId).IsRequired();
      
      builder.Entity<Batch>().Property(b => b.VineyardCode).IsRequired().HasMaxLength(50);
      builder.Entity<Batch>().Property(b => b.GrapeVariety).IsRequired().HasMaxLength(50);
      builder.Entity<Batch>().Property(b => b.HarvestDate).IsRequired();
      builder.Entity<Batch>().Property(b => b.GrapeQuantity).IsRequired();
      builder.Entity<Batch>().Property(b => b.VineyardOrigin).IsRequired().HasMaxLength(50);
      builder.Entity<Batch>().Property(b => b.ProcessStartDate);
      builder.Entity<Batch>().Property(b => b.Status);
      
      //---------------- 
      builder.Entity<Batch>()
         .HasOne(b => b.Profile)
         .WithMany(p => p.Batches)
         .HasForeignKey(b => b.ProfileId)
         .OnDelete(DeleteBehavior.Cascade);
      
      
      
      //---------------- CONFIGURATION DE FERMENTATION ----------------
      builder.Entity<Fermentation>().HasKey(f => f.Id);
      builder.Entity<Fermentation>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
      
      builder.Entity<Fermentation>().Property(f => f.BatchId).IsRequired();
      builder.Entity<Fermentation>().Property(f => f.StartDate).IsRequired();
      builder.Entity<Fermentation>().Property(f => f.EndDate);
      builder.Entity<Fermentation>().Property(f => f.AverageTemperature).IsRequired();
      builder.Entity<Fermentation>().Property(f => f.InitialDensity).IsRequired();
      builder.Entity<Fermentation>().Property(f => f.InitialPh).IsRequired();
      builder.Entity<Fermentation>().Property(f => f.FinalDensity).IsRequired();
      builder.Entity<Fermentation>().Property(f => f.FinalPh).IsRequired();
      builder.Entity<Fermentation>().Property(f => f.ResidualSugar).IsRequired();
      //---------------- Relación uno a uno con batch y si elimino un batch se elimina el estado asociado a este ----------------
      builder.Entity<Batch>()
         .HasOne(b => b.Fermentation);
      
      //---------------- CONFIGURATION DE CLARIFICATION ----------------
      builder.Entity<Clarification>().HasKey(c => c.Id);
      builder.Entity<Clarification>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
      
      builder.Entity<Clarification>().Property(c => c.BatchId).IsRequired();
      builder.Entity<Clarification>().Property(c => c.ProductsUsed).IsRequired().HasMaxLength(50);
      builder.Entity<Clarification>().Property(c => c.ClarificationMethod).IsRequired().HasMaxLength(50);
      builder.Entity<Clarification>().Property(c => c.FiltrationDate);
      builder.Entity<Clarification>().Property(c => c.ClarityLevel).IsRequired();
      builder.Entity<Clarification>().Property(c => c.StartDate).IsRequired();
      builder.Entity<Clarification>().Property(c => c.EndDate).IsRequired();
      
      //---------------- Relación uno a uno con batch ----------------
      builder.Entity<Batch>()
         .HasOne(b => b.Clarification);
      
      
      //---------------- CONFIGURATION DE PRESSING ----------------
      builder.Entity<Pressing>().HasKey(p => p.Id);
      builder.Entity<Pressing>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
      
      builder.Entity<Pressing>().Property(p => p.BatchId).IsRequired();
      builder.Entity<Pressing>().Property(p => p.PressingDate).IsRequired();
      builder.Entity<Pressing>().Property(p => p.MustVolume).IsRequired();
      builder.Entity<Pressing>().Property(p => p.PressType).IsRequired().HasMaxLength(50);
      builder.Entity<Pressing>().Property(p => p.AppliedPressure).IsRequired();
      
      //---------------- Relación uno a uno con batch ----------------
      builder.Entity<Batch>()
         .HasOne(b => b.Pressing);
      
      
      //---------------- CONFIGURATION DE AGING ----------------
      builder.Entity<Aging>().HasKey(a => a.Id);
      builder.Entity<Aging>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
      
      builder.Entity<Aging>().Property(a => a.BatchId).IsRequired();
      builder.Entity<Aging>().Property(a => a.BarrelType).IsRequired().HasMaxLength(50);
      builder.Entity<Aging>().Property(a => a.StartDate).IsRequired();
      builder.Entity<Aging>().Property(a => a.EndDate);
      builder.Entity<Aging>().Property(a => a.AgingDurationMonths).IsRequired();
      builder.Entity<Aging>().Property(a => a.InspectionsPerformed).IsRequired();
      builder.Entity<Aging>().Property(a => a.InspectionResult).IsRequired().HasMaxLength(50);
      
      //---------------- Relación uno a uno con batch ----------------
      builder.Entity<Batch>()
         .HasOne(b => b.Aging);
      
      //---------------- CONFIGURATION DE BOTTLING ----------------
      builder.Entity<Bottling>().HasKey(z => z.Id);
      builder.Entity<Bottling>().Property(z => z.Id).IsRequired().ValueGeneratedOnAdd();
      
      builder.Entity<Bottling>().Property(z => z.BatchId).IsRequired();
      builder.Entity<Bottling>().Property(z => z.BottlingDate).IsRequired();
      builder.Entity<Bottling>().Property(z => z.BottleSizeMl).IsRequired();
      builder.Entity<Bottling>().Property(z => z.NumberOfBottles).IsRequired();
      builder.Entity<Bottling>().Property(z => z.LabelType).IsRequired().HasMaxLength(50);
      builder.Entity<Bottling>().Property(z => z.CorkType).IsRequired().HasMaxLength(50);
      
      //---------------- Relación uno a uno con batch ----------------
      builder.Entity<Batch>()
         .HasOne(b => b.Bottling);
      
      //---------------- CONFIGURATION DE PROFILES ----------------
      builder.Entity<Profile>().HasKey(p => p.Id);
      builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
      
      builder.Entity<Profile>().Property(p => p.CompanyName).IsRequired().HasMaxLength(50);
      builder.Entity<Profile>().Property(p => p.PhoneNumber).IsRequired().HasMaxLength(50);
      builder.Entity<Profile>().Property(p => p.RUC).IsRequired().HasMaxLength(50);
      
      builder.Entity<Profile>().OwnsOne(p => p.Name,
         n =>
         {
            n.WithOwner().HasForeignKey("Id");
            n.Property(p => p.FirstName).HasColumnName("FirstName");
            n.Property(p => p.LastName).HasColumnName("LastName");
         });

      builder.Entity<Profile>().OwnsOne(p => p.Email, e =>
      {
         e.WithOwner().HasForeignKey("Id");
         e.Property(a => a.Address).HasColumnName("EmailAddress");
      });

      builder.Entity<Profile>().OwnsOne(p => p.Address,
         a =>
         {
            a.WithOwner().HasForeignKey("Id");
            a.Property(s => s.Street).HasColumnName("AddressStreet");
            a.Property(s => s.Number).HasColumnName("AddressNumber");
            a.Property(s => s.City).HasColumnName("AddressCity");
            a.Property(s => s.Country).HasColumnName("AddressCountry");
         });
      
      // IAM Context
      builder.Entity<User>().HasKey(u => u.Id);
      builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
      builder.Entity<User>().Property(u => u.Username).IsRequired();
      builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
      
      // Relación uno a uno con la entidad Profile
      builder.Entity<User>()
         .HasOne(u => u.Profile)
         .WithOne(p => p.User)
         .HasForeignKey<Profile>(p => p.UserId)
         .OnDelete(DeleteBehavior.Cascade);

      
      
      
      
      //-----------------------------------------------------------------------------------------------
      //===================================== END GONZALO BOUNDED CONTEXT =============================
      //===============================================================================================
         
      
      
      //===================================== END VICENTE BOUNDED CONTEXT ===============================
      //=================================================================================================
   
      
      //Regals de mapped object relational (ORM)
      builder.UseSnakeCaseNamingConvention();
   }
}