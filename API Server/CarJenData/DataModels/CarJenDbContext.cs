using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CarJenData.DataModels;

public partial class CarJenDbContext : DbContext
{
    public CarJenDbContext()
    {
    }

    public CarJenDbContext(DbContextOptions<CarJenDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AirbagCondition> AirbagConditions { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentsWatcherLog> AppointmentsWatcherLogs { get; set; }

    public virtual DbSet<BodyCondition> BodyConditions { get; set; }

    public virtual DbSet<BrakeFluidLeakage> BrakeFluidLeakages { get; set; }

    public virtual DbSet<BrakePad> BrakePads { get; set; }

    public virtual DbSet<BrakePipe> BrakePipes { get; set; }

    public virtual DbSet<BrakeRotor> BrakeRotors { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarDocumentation> CarDocumentations { get; set; }

    public virtual DbSet<CarInspection> CarInspections { get; set; }

    public virtual DbSet<CarTeamUpdate> CarTeamUpdates { get; set; }

    public virtual DbSet<CoolantLeakage> CoolantLeakages { get; set; }

    public virtual DbSet<CoolingCondition> CoolingConditions { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<ElectricalCharging> ElectricalChargings { get; set; }

    public virtual DbSet<ElectricalWiring> ElectricalWirings { get; set; }

    public virtual DbSet<EngineAuthenticity> EngineAuthenticities { get; set; }

    public virtual DbSet<EngineOilLeakage> EngineOilLeakages { get; set; }

    public virtual DbSet<EngineSound> EngineSounds { get; set; }

    public virtual DbSet<EngineTemperature> EngineTemperatures { get; set; }

    public virtual DbSet<EngineVibration> EngineVibrations { get; set; }

    public virtual DbSet<FrontLight> FrontLights { get; set; }

    public virtual DbSet<HeatingCondition> HeatingConditions { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<ImageCollection> ImageCollections { get; set; }

    public virtual DbSet<InspectionPayment> InspectionPayments { get; set; }

    public virtual DbSet<MainFee> MainFees { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<PaintCondition> PaintConditions { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PreApprovedCarsView> PreApprovedCarsViews { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<RearLight> RearLights { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<ReportUnitFee> ReportUnitFees { get; set; }

    public virtual DbSet<Resume> Resumes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Seller> Sellers { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamMember> TeamMembers { get; set; }

    public virtual DbSet<TeamMemberCountView> TeamMemberCountViews { get; set; }

    public virtual DbSet<TireDepth> TireDepths { get; set; }

    public virtual DbSet<TirePressure> TirePressures { get; set; }

    public virtual DbSet<Trim> Trims { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=CarJenDB;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_CI_AS");

        modelBuilder.Entity<AirbagCondition>(entity =>
        {
            entity.HasKey(e => e.AirbagId).HasName("PK__AirbagCo__CE560F7802F129FA");

            entity.Property(e => e.AirbagId).HasColumnName("AirbagID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.AirbagConditions)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AirbagCon__CarIn__6C390A4C");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA2A9C6C731");

            entity.ToTable(tb => tb.HasTrigger("trg_InsteadOfDeleteAppointment"));

            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.AppointmentDate).HasColumnType("datetime");
            entity.Property(e => e.CarDocumentationId).HasColumnName("CarDocumentationID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.PublishFeeId).HasColumnName("PublishFeeID");

            entity.HasOne(d => d.CarDocumentation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.CarDocumentationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__CarDo__3E1D39E1");

            entity.HasOne(d => d.PublishFee).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PublishFeeId)
                .HasConstraintName("FK__Appointme__Publi__4589517F");
        });

        modelBuilder.Entity<AppointmentsWatcherLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Appointm__3214EC077B2304A9");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LogType).HasMaxLength(50);
        });

        modelBuilder.Entity<BodyCondition>(entity =>
        {
            entity.HasKey(e => e.BodyId).HasName("PK__BodyCond__8545D8B5B5495CFD");

            entity.Property(e => e.BodyId).HasColumnName("BodyID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.BodyConditions)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BodyCondi__CarIn__5555A4F4");
        });

        modelBuilder.Entity<BrakeFluidLeakage>(entity =>
        {
            entity.HasKey(e => e.BrakeFluidLeakageId).HasName("PK__BrakeFlu__247425EBB8D8944D");

            entity.Property(e => e.BrakeFluidLeakageId).HasColumnName("BrakeFluidLeakageID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.BrakeFluidLeakages)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BrakeFlui__CarIn__7A8729A3");
        });

        modelBuilder.Entity<BrakePad>(entity =>
        {
            entity.HasKey(e => e.PadId).HasName("PK__BrakePad__F27F84A2E6CFB8B5");

            entity.Property(e => e.PadId).HasColumnName("PadID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.BrakePads)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BrakePads__CarIn__49E3F248");
        });

        modelBuilder.Entity<BrakePipe>(entity =>
        {
            entity.HasKey(e => e.PipeId).HasName("PK__BrakePip__30B4C3BDFB8CC0E2");

            entity.Property(e => e.PipeId).HasColumnName("PipeID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.BrakePipes)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BrakePipe__CarIn__4F9CCB9E");
        });

        modelBuilder.Entity<BrakeRotor>(entity =>
        {
            entity.HasKey(e => e.RotorId).HasName("PK__BrakeRot__4A2F453BDB658E3E");

            entity.Property(e => e.RotorId).HasColumnName("RotorID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.BrakeRotors)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BrakeRoto__CarIn__4CC05EF3");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__Brands__DAD4F3BE2711A9A4");

            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.Brand1)
                .HasMaxLength(50)
                .HasColumnName("Brand");
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK__Cars__68A0340EA5F5229E");

            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.Color).HasMaxLength(30);
            entity.Property(e => e.PlateNumber).HasMaxLength(30);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.RegistrationExp).HasColumnType("datetime");
            entity.Property(e => e.TechInspectionExp).HasColumnType("datetime");
            entity.Property(e => e.TrimId).HasColumnName("TrimID");

            entity.HasOne(d => d.Trim).WithMany(p => p.Cars)
                .HasForeignKey(d => d.TrimId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cars__TrimID__72C60C4A");
        });

        modelBuilder.Entity<CarDocumentation>(entity =>
        {
            entity.HasKey(e => e.CarDocumentationId).HasName("PK__CarDocum__11F479E3F47B2B64");

            entity.Property(e => e.CarDocumentationId).HasColumnName("CarDocumentationID");
            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.SellerId).HasColumnName("SellerID");

            entity.HasOne(d => d.Car).WithMany(p => p.CarDocumentations)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarDocume__CarID__7C4F7684");

            entity.HasOne(d => d.Seller).WithMany(p => p.CarDocumentations)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarDocume__Selle__7B5B524B");
        });

        modelBuilder.Entity<CarInspection>(entity =>
        {
            entity.HasKey(e => e.CarInspectionId).HasName("PK__CarInspe__3777546AFAEB0CCA");

            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");
            entity.Property(e => e.CarDocumentationId).HasColumnName("CarDocumentationID");
            entity.Property(e => e.ExpDate).HasColumnType("datetime");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");

            entity.HasOne(d => d.CarDocumentation).WithMany(p => p.CarInspections)
                .HasForeignKey(d => d.CarDocumentationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarInspec__CarDo__1332DBDC");

            entity.HasOne(d => d.Team).WithMany(p => p.CarInspections)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__CarInspec__TeamI__4FD1D5C8");
        });

        modelBuilder.Entity<CarTeamUpdate>(entity =>
        {
            entity.HasKey(e => e.CarTeamUpdateId).HasName("PK__CarTeamU__0DBA31519B550294");

            entity.Property(e => e.CarTeamUpdateId).HasColumnName("CarTeamUpdateID");
            entity.Property(e => e.CarDocumentationId).HasColumnName("CarDocumentationID");
            entity.Property(e => e.CheckedDate).HasColumnType("datetime");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");

            entity.HasOne(d => d.CarDocumentation).WithMany(p => p.CarTeamUpdates)
                .HasForeignKey(d => d.CarDocumentationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarTeamUp__CarDo__7F2BE32F");

            entity.HasOne(d => d.Team).WithMany(p => p.CarTeamUpdates)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarTeamUp__TeamI__00200768");
        });

        modelBuilder.Entity<CoolantLeakage>(entity =>
        {
            entity.HasKey(e => e.CoolantLeakageId).HasName("PK__CoolantL__3CC253BD8724FA91");

            entity.Property(e => e.CoolantLeakageId).HasColumnName("CoolantLeakageID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.CoolantLeakages)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CoolantLe__CarIn__77AABCF8");
        });

        modelBuilder.Entity<CoolingCondition>(entity =>
        {
            entity.HasKey(e => e.CoolingId).HasName("PK__CoolingC__5083E7F99D137DA6");

            entity.Property(e => e.CoolingId).HasColumnName("CoolingID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.CoolingConditions)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CoolingCo__CarIn__668030F6");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B8FD700D26");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");

            entity.HasOne(d => d.Person).WithMany(p => p.Customers)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Perso__5535A963");
        });

        modelBuilder.Entity<ElectricalCharging>(entity =>
        {
            entity.HasKey(e => e.ChargingId).HasName("PK__Electric__2850AF7B1ABA88EB");

            entity.ToTable("ElectricalCharging");

            entity.Property(e => e.ChargingId).HasColumnName("ChargingID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.ElectricalChargings)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Electrica__CarIn__5DEAEAF5");
        });

        modelBuilder.Entity<ElectricalWiring>(entity =>
        {
            entity.HasKey(e => e.WiringId).HasName("PK__Electric__B29B05680E70D680");

            entity.ToTable("ElectricalWiring");

            entity.Property(e => e.WiringId).HasColumnName("WiringID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.ElectricalWirings)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Electrica__CarIn__60C757A0");
        });

        modelBuilder.Entity<EngineAuthenticity>(entity =>
        {
            entity.HasKey(e => e.AuthenticityId).HasName("PK__EngineAu__C809EF89D8310B5E");

            entity.Property(e => e.AuthenticityId).HasColumnName("AuthenticityID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.EngineAuthenticities)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EngineAut__CarIn__4707859D");
        });

        modelBuilder.Entity<EngineOilLeakage>(entity =>
        {
            entity.HasKey(e => e.EngineOilLeakageId).HasName("PK__EngineOi__B1119D9916F63B72");

            entity.Property(e => e.EngineOilLeakageId).HasColumnName("EngineOilLeakageID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.EngineOilLeakages)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EngineOil__CarIn__74CE504D");
        });

        modelBuilder.Entity<EngineSound>(entity =>
        {
            entity.HasKey(e => e.SoundId).HasName("PK__EngineSo__17B8290109AC248D");

            entity.Property(e => e.SoundId).HasColumnName("SoundID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.EngineSounds)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EngineSou__CarIn__414EAC47");
        });

        modelBuilder.Entity<EngineTemperature>(entity =>
        {
            entity.HasKey(e => e.TemperatureId).HasName("PK__EngineTe__B8D7DAAEA24C858E");

            entity.Property(e => e.TemperatureId).HasColumnName("TemperatureID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.EngineTemperatures)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EngineTem__CarIn__63A3C44B");
        });

        modelBuilder.Entity<EngineVibration>(entity =>
        {
            entity.HasKey(e => e.VibrationId).HasName("PK__EngineVi__99380BDCDA87834F");

            entity.Property(e => e.VibrationId).HasColumnName("VibrationID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.EngineVibrations)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EngineVib__CarIn__442B18F2");
        });

        modelBuilder.Entity<FrontLight>(entity =>
        {
            entity.HasKey(e => e.FrontLightId).HasName("PK__FrontLig__C4FAF3EABE0B7F98");

            entity.Property(e => e.FrontLightId).HasColumnName("FrontLightID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.FrontLights)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FrontLigh__CarIn__5832119F");
        });

        modelBuilder.Entity<HeatingCondition>(entity =>
        {
            entity.HasKey(e => e.HeatingId).HasName("PK__HeatingC__881F114FFDFD61F9");

            entity.Property(e => e.HeatingId).HasColumnName("HeatingID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.HeatingConditions)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HeatingCo__CarIn__695C9DA1");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Images__7516F4EC873455BC");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.ImageCollectionId).HasColumnName("ImageCollectionID");
            entity.Property(e => e.ImagePath).HasMaxLength(300);

            entity.HasOne(d => d.ImageCollection).WithMany(p => p.Images)
                .HasForeignKey(d => d.ImageCollectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Images__ImageCol__787EE5A0");
        });

        modelBuilder.Entity<ImageCollection>(entity =>
        {
            entity.HasKey(e => e.ImageCollectionId).HasName("PK__ImageCol__8AFB51841680B2B6");

            entity.Property(e => e.ImageCollectionId).HasColumnName("ImageCollectionID");
            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Car).WithMany(p => p.ImageCollections)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ImageColl__CarID__75A278F5");
        });

        modelBuilder.Entity<InspectionPayment>(entity =>
        {
            entity.HasKey(e => e.InspectionPaymentId).HasName("PK__Inspecti__BBAC6EF798880866");

            entity.Property(e => e.InspectionPaymentId).HasColumnName("InspectionPaymentID");
            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.InspectionFeeId).HasColumnName("InspectionFeeID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");

            entity.HasOne(d => d.Appointment).WithMany(p => p.InspectionPayments)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inspectio__Appoi__40F9A68C");

            entity.HasOne(d => d.InspectionFee).WithMany(p => p.InspectionPayments)
                .HasForeignKey(d => d.InspectionFeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inspectio__Inspe__09746778");
        });

        modelBuilder.Entity<MainFee>(entity =>
        {
            entity.HasKey(e => e.MainFeeId).HasName("PK__Fees__B387B2093FB85560");

            entity.Property(e => e.MainFeeId).HasColumnName("MainFeeID");
            entity.Property(e => e.Amount).HasColumnType("smallmoney");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PK__Models__E8D7A1CCE87700C5");

            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.Model1)
                .HasMaxLength(50)
                .HasColumnName("Model");

            entity.HasOne(d => d.Brand).WithMany(p => p.Models)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Models__BrandID__6D0D32F4");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__Packages__322035EC593FBFA6");

            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.Title).HasMaxLength(30);

            entity.HasOne(d => d.CreatedByUserNavigation).WithMany(p => p.Packages)
                .HasForeignKey(d => d.CreatedByUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Packages__Create__5FB337D6");
        });

        modelBuilder.Entity<PaintCondition>(entity =>
        {
            entity.HasKey(e => e.PaintId).HasName("PK__PaintCon__7367B46A5AE24517");

            entity.Property(e => e.PaintId).HasColumnName("PaintID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.PaintConditions)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaintCond__CarIn__52793849");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__People__AA2FFB858CC7D76A");

            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Image).HasMaxLength(300);
            entity.Property(e => e.JoinDate).HasColumnType("datetime");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(64);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<PreApprovedCarsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("PreApprovedCarsView");

            entity.Property(e => e.Brand).HasMaxLength(50);
            entity.Property(e => e.CarOwner)
                .HasMaxLength(101)
                .HasColumnName("Car owner");
            entity.Property(e => e.FileId).HasColumnName("File ID");
            entity.Property(e => e.Fuel)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.Model).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Trim).HasMaxLength(50);
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK__Purchase__6B0A6BDE6BE743AA");

            entity.Property(e => e.PurchaseId).HasColumnName("PurchaseID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.PurchaseDate).HasColumnType("datetime");
            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.ReportPrice).HasColumnType("smallmoney");

            entity.HasOne(d => d.Customer).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Purchases__Custo__07C12930");

            entity.HasOne(d => d.Report).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.ReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Purchases__Repor__06CD04F7");
        });

        modelBuilder.Entity<RearLight>(entity =>
        {
            entity.HasKey(e => e.RearLightId).HasName("PK__RearLigh__5B1A7BC9C9286C88");

            entity.Property(e => e.RearLightId).HasColumnName("RearLightID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.RearLights)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RearLight__CarIn__5B0E7E4A");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Reports__D5BD48E5862D5C5A");

            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.CarDocumentationId).HasColumnName("CarDocumentationID");
            entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

            entity.HasOne(d => d.CarDocumentation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.CarDocumentationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reports__CarDocu__02FC7413");
        });

        modelBuilder.Entity<ReportUnitFee>(entity =>
        {
            entity.HasKey(e => e.ReportUnitFeeId).HasName("PK__ReportUn__AF15BAF27C9273FF");

            entity.Property(e => e.ReportUnitFeeId).HasColumnName("ReportUnitFeeID");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.UnitFeeId).HasColumnName("UnitFeeID");

            entity.HasOne(d => d.Package).WithMany(p => p.ReportUnitFees)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReportUni__Packa__628FA481");

            entity.HasOne(d => d.UnitFee).WithMany(p => p.ReportUnitFees)
                .HasForeignKey(d => d.UnitFeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReportUni__UnitF__6383C8BA");
        });

        modelBuilder.Entity<Resume>(entity =>
        {
            entity.HasKey(e => e.ResumeId).HasName("PK__Resumes__D7D7A317DAEDE31D");

            entity.Property(e => e.ResumeId).HasColumnName("ResumeID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");
            entity.Property(e => e.Resume1)
                .HasMaxLength(350)
                .HasColumnName("Resume");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.Resumes)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Resumes__CarInsp__4924D839");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A546403C7");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Permission).HasDefaultValue(-1);
            entity.Property(e => e.RoleTitle).HasMaxLength(150);
            entity.Property(e => e.Salary).HasColumnType("smallmoney");
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.HasKey(e => e.SellerId).HasName("PK__Sellers__7FE3DBA1724240D1");

            entity.Property(e => e.SellerId).HasColumnName("SellerID");
            entity.Property(e => e.Earnings).HasColumnType("smallmoney");
            entity.Property(e => e.NationalNumber).HasMaxLength(30);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");

            entity.HasOne(d => d.Person).WithMany(p => p.Sellers)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sellers__PersonI__52593CB8");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__Subscrip__9A2B24BD33B04B1A");

            entity.Property(e => e.SubscriptionId).HasColumnName("SubscriptionID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.ReportUnitFeeId).HasColumnName("ReportUnitFeeID");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.SubscriptionDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subscript__Custo__66603565");

            entity.HasOne(d => d.ReportUnitFee).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.ReportUnitFeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subscript__Repor__68487DD7");

            entity.HasOne(d => d.Service).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subscript__Servi__6754599E");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__Teams__123AE7B98A9355A8");

            entity.Property(e => e.TeamId).HasColumnName("TeamID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.TeamCode).HasMaxLength(30);
        });

        modelBuilder.Entity<TeamMember>(entity =>
        {
            entity.HasKey(e => e.TeamMemberId).HasName("PK__TeamMmbe__2C8633ADB1BDCC67");

            entity.Property(e => e.TeamMemberId).HasColumnName("TeamMemberID");
            entity.Property(e => e.ExitDate).HasColumnType("datetime");
            entity.Property(e => e.JoinDate).HasColumnType("datetime");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamMembers)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TeamMmber__TeamI__59FA5E80");

            entity.HasOne(d => d.User).WithMany(p => p.TeamMembers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TeamMmber__UserI__5AEE82B9");
        });

        modelBuilder.Entity<TeamMemberCountView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TeamMemberCountView");

            entity.Property(e => e.TeamId).HasColumnName("TeamID");
            entity.Property(e => e.TotalMembers).HasColumnName("Total members");
        });

        modelBuilder.Entity<TireDepth>(entity =>
        {
            entity.HasKey(e => e.DepthId).HasName("PK__TireDept__7761EFF04CE2EBE3");

            entity.Property(e => e.DepthId).HasColumnName("DepthID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.TireDepths)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TireDepth__CarIn__6F1576F7");
        });

        modelBuilder.Entity<TirePressure>(entity =>
        {
            entity.HasKey(e => e.PressureId).HasName("PK__TirePres__82C76B015F7137D2");

            entity.Property(e => e.PressureId).HasColumnName("PressureID");
            entity.Property(e => e.CarInspectionId).HasColumnName("CarInspectionID");

            entity.HasOne(d => d.CarInspection).WithMany(p => p.TirePressures)
                .HasForeignKey(d => d.CarInspectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TirePress__CarIn__71F1E3A2");
        });

        modelBuilder.Entity<Trim>(entity =>
        {
            entity.HasKey(e => e.TrimId).HasName("PK__Trims__50ABD2C4D6FBADF5");

            entity.Property(e => e.TrimId).HasColumnName("TrimID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.Trim1)
                .HasMaxLength(50)
                .HasColumnName("Trim");

            entity.HasOne(d => d.Model).WithMany(p => p.Trims)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trims__ModelID__21A0F6C4");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC4118D639");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.NationalNumber).HasMaxLength(30);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.InverseCreatedByUser)
                .HasForeignKey(d => d.CreatedByUserId)
                .HasConstraintName("FK__Users__CreatedBy__4F7CD00D");

            entity.HasOne(d => d.Person).WithMany(p => p.Users)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__PersonID__4D94879B");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleID__4E88ABD4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
