using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Nippon_Final_Project.Models;

public partial class TintingRecordsContext : DbContext
{
    public TintingRecordsContext()
    {
    }

    public TintingRecordsContext(DbContextOptions<TintingRecordsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Colourant> Colourants { get; set; }

    public virtual DbSet<LoginPage> LoginPages { get; set; }

    public virtual DbSet<MappingMaster> MappingMasters { get; set; }

    public virtual DbSet<Observation> Observations { get; set; }

    public virtual DbSet<RequestIdGeneratorObservation> RequestIdGeneratorObservations { get; set; }

    public virtual DbSet<RequestIdGeneratorTinting> RequestIdGeneratorTintings { get; set; }

    public virtual DbSet<Tinting> Tintings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost; Database=Tinting_Records; Username=postgres; Password=rak47675");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Colourant>(entity =>
        {
            entity.HasKey(e => e.Sno).HasName("Colourants_pkey");

            entity.Property(e => e.BatchNo).HasColumnName("Batch_No");
            entity.Property(e => e.CreatedOn).HasColumnName("Created_On");
            entity.Property(e => e.DispensingMachine).HasColumnName("Dispensing_Machine");
            entity.Property(e => e.Mfg).HasColumnName("MFG");
            entity.Property(e => e.RequestId).HasColumnName("Request_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");
        });

        modelBuilder.Entity<LoginPage>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("Master_page_pkey");

            entity.ToTable("Login_Page");

            entity.Property(e => e.UserId).HasColumnName("User_Id");
        });

        modelBuilder.Entity<MappingMaster>(entity =>
        {
            entity.HasKey(e => e.Sno).HasName("Mapping_Master_pkey");

            entity.ToTable("Mapping_Master");

            entity.Property(e => e.DepotName).HasColumnName("Depot_Name");
            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.UserName).HasColumnName("User_Name");
        });

        modelBuilder.Entity<Observation>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("Observation_pkey");

            entity.ToTable("Observation");

            entity.Property(e => e.RequestId).HasColumnName("Request_Id");
            entity.Property(e => e.BaseTintedAsPerHistoryFileInLit).HasColumnName("Base_Tinted_As_Per_History_File_In_Lit");
            entity.Property(e => e.BaseTintedAsPerReportInLit).HasColumnName("Base_Tinted_As_Per_Report_In_Lit");
            entity.Property(e => e.BrandingForDispensingMachine).HasColumnName("Branding_For_Dispensing_Machine");
            entity.Property(e => e.BrandingForGyroshakerMachine).HasColumnName("Branding_For_Gyroshaker_Machine");
            entity.Property(e => e.ColorantConsumedInLit).HasColumnName("Colorant_Consumed_In_Lit");
            entity.Property(e => e.ColorantPouredInCannistersInLit).HasColumnName("Colorant_Poured_In_Cannisters_In_Lit");
            entity.Property(e => e.CreatedOn).HasColumnName("Created_On");
            entity.Property(e => e.UserId).HasColumnName("User_Id");
        });

        modelBuilder.Entity<RequestIdGeneratorObservation>(entity =>
        {
            entity.HasKey(e => e.Sno).HasName("Request_Id_Generator_Observation_pkey");

            entity.ToTable("Request_Id_Generator_Observation");

            entity.Property(e => e.ObservationRequestIdDate).HasColumnName("Observation_Request_Id_Date");
            entity.Property(e => e.ObservationRequestIdNumber).HasColumnName("Observation_Request_Id_Number");
        });

        modelBuilder.Entity<RequestIdGeneratorTinting>(entity =>
        {
            entity.HasKey(e => e.Sno).HasName("Request_Id_Generator_pkey");

            entity.ToTable("Request_Id_Generator_Tinting");

            entity.Property(e => e.Sno).HasDefaultValueSql("nextval('\"Request_Id_Generator_Sno_seq\"'::regclass)");
            entity.Property(e => e.TintingRequestIdDate).HasColumnName("Tinting_Request_Id_Date");
            entity.Property(e => e.TintingRequestIdNumber).HasColumnName("Tinting_Request_Id_Number");
        });

        modelBuilder.Entity<Tinting>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("Tintings_pkey");

            entity.Property(e => e.RequestId).HasColumnName("Request_Id");
            entity.Property(e => e.BaseBatchNo).HasColumnName("Base_Batch_No");
            entity.Property(e => e.ColourShade).HasColumnName("Colour_Shade");
            entity.Property(e => e.CreatedOn).HasColumnName("Created_On");
            entity.Property(e => e.DispensingMachine).HasColumnName("Dispensing_Machine");
            entity.Property(e => e.ForProjectOrRetail).HasColumnName("For_Project_Or_Retail");
            entity.Property(e => e.ForSMProjectOrRetailOrBuka).HasColumnName("For_S&M_Project_Or_Retail_Or_BUKA");
            entity.Property(e => e.FormulationFor1LitrePackSize).HasColumnName("Formulation_For_1_Litre_Pack_Size");
            entity.Property(e => e.NameOfSalesPerson).HasColumnName("Name_Of_Sales_Person");
            entity.Property(e => e.NameOfTheProject).HasColumnName("Name_Of_The_Project");
            entity.Property(e => e.OriginalInvoice).HasColumnName("Original_Invoice");
            entity.Property(e => e.OtherObservations).HasColumnName("Other_Observations");
            entity.Property(e => e.QuantityTintedInLitres).HasColumnName("Quantity_Tinted_In_Litres");
            entity.Property(e => e.ShadeCode).HasColumnName("Shade_Code");
            entity.Property(e => e.ShadeMatchConfirmation).HasColumnName("Shade_Match_Confirmation");
            entity.Property(e => e.ShadeName).HasColumnName("Shade_Name");
            entity.Property(e => e.ShadePatch).HasColumnName("Shade_Patch");
            entity.Property(e => e.TintingInvoice).HasColumnName("Tinting_Invoice");
            entity.Property(e => e.UserId).HasColumnName("User_Id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
