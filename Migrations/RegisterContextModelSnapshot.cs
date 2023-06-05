﻿// <auto-generated />
using System;
using IAR.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IAR.Migrations
{
    [DbContext(typeof(RegisterContext))]
    partial class RegisterContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IAR.Models.Asset", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("AccessedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int?>("BackEndPlatformID")
                        .HasColumnType("int");

                    b.Property<string>("BackEndPlatformLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Children")
                        .HasColumnType("bit");

                    b.Property<bool>("Complaints")
                        .HasColumnType("bit");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("CriminalConviction")
                        .HasColumnType("bit");

                    b.Property<string>("DataOwnerAccountName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DataStewardAccountName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DataSubjects")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EducationEmployment")
                        .HasColumnType("bit");

                    b.Property<string>("ExecutiveSponsorAccountName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("FinancialDetails")
                        .HasColumnType("bit");

                    b.Property<int?>("FrontEndPlatformID")
                        .HasColumnType("int");

                    b.Property<string>("FrontEndPlatformLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GoodsServices")
                        .HasColumnType("bit");

                    b.Property<bool>("HealthSafetySecurity")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("LawfulBasisConsent")
                        .HasColumnType("bit");

                    b.Property<string>("LawfulBasisConsentDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LawfulBasisContract")
                        .HasColumnType("bit");

                    b.Property<string>("LawfulBasisContractDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LawfulBasisLegalObligation")
                        .HasColumnType("bit");

                    b.Property<string>("LawfulBasisLegalObligationDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LawfulBasisLegitimateInterest")
                        .HasColumnType("bit");

                    b.Property<string>("LawfulBasisLegitimateInterestDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LawfulBasisVitalInterest")
                        .HasColumnType("bit");

                    b.Property<string>("LawfulBasisVitalInterestDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LifestyleSocial")
                        .HasColumnType("bit");

                    b.Property<string>("MaintainedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NextReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PersonalData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PersonalDetails")
                        .HasColumnType("bit");

                    b.Property<int?>("PhysicalLocation")
                        .HasColumnType("int");

                    b.Property<string>("Provider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Restricted")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("ReviewCycleMonths")
                        .HasColumnType("tinyint");

                    b.Property<string>("ReviewedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SecondaryPurpose")
                        .HasColumnType("bit");

                    b.Property<string>("SecondaryPurposeDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SpecialBiometric")
                        .HasColumnType("bit");

                    b.Property<bool>("SpecialGenetic")
                        .HasColumnType("bit");

                    b.Property<bool>("SpecialHealth")
                        .HasColumnType("bit");

                    b.Property<bool>("SpecialPoliticalOpinion")
                        .HasColumnType("bit");

                    b.Property<bool>("SpecialRacialEthnic")
                        .HasColumnType("bit");

                    b.Property<bool>("SpecialReligiousPhilosophical")
                        .HasColumnType("bit");

                    b.Property<bool>("SpecialSexual")
                        .HasColumnType("bit");

                    b.Property<bool>("SpecialTradeUnion")
                        .HasColumnType("bit");

                    b.Property<bool>("SubjectToDPA")
                        .HasColumnType("bit");

                    b.Property<bool>("SupplierDetails")
                        .HasColumnType("bit");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("VisualImages")
                        .HasColumnType("bit");

                    b.Property<int?>("Volume")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("BackEndPlatformID");

                    b.HasIndex("DataOwnerAccountName");

                    b.HasIndex("DataStewardAccountName");

                    b.HasIndex("ExecutiveSponsorAccountName");

                    b.HasIndex("FrontEndPlatformID");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("IAR.Models.AuditLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuditData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Table")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TableID")
                        .HasColumnType("int");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("AuditLog");
                });

            modelBuilder.Entity("IAR.Models.BackEndPlatform", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("BackEndPlatforms");
                });

            modelBuilder.Entity("IAR.Models.FrontEndPlatform", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("FrontEndPlatforms");
                });

            modelBuilder.Entity("IAR.Models.Note", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("AssetID")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoteText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("AssetID");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("IAR.Models.RetentionPeriod", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("AssetID")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RetainedData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RetentionPeriodMonths")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("AssetID");

                    b.ToTable("RetentionPeriods");
                });

            modelBuilder.Entity("IAR.Models.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("AccountName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("IAR.Models.ThirdParty", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("AssetID")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Location")
                        .HasColumnType("int");

                    b.Property<string>("LocationDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThirdPartyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Use")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AssetID");

                    b.ToTable("ThirdParties");
                });

            modelBuilder.Entity("IAR.Models.Tooltip", b =>
                {
                    b.Property<string>("FieldName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TooltipText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("FieldName");

                    b.ToTable("Tooltips");
                });

            modelBuilder.Entity("IAR.Models.User", b =>
                {
                    b.Property<string>("AccountName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IAR.Models.Asset", b =>
                {
                    b.HasOne("IAR.Models.BackEndPlatform", "BackEndPlatform")
                        .WithMany("Assets")
                        .HasForeignKey("BackEndPlatformID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("IAR.Models.User", "DataOwner")
                        .WithMany("DataOwnerAssets")
                        .HasForeignKey("DataOwnerAccountName")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("IAR.Models.User", "DataSteward")
                        .WithMany("DataStewardAssets")
                        .HasForeignKey("DataStewardAccountName")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("IAR.Models.User", "ExecutiveSponsor")
                        .WithMany("ExecutiveSponsorAssets")
                        .HasForeignKey("ExecutiveSponsorAccountName")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("IAR.Models.FrontEndPlatform", "FrontEndPlatform")
                        .WithMany("Assets")
                        .HasForeignKey("FrontEndPlatformID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("BackEndPlatform");

                    b.Navigation("DataOwner");

                    b.Navigation("DataSteward");

                    b.Navigation("ExecutiveSponsor");

                    b.Navigation("FrontEndPlatform");
                });

            modelBuilder.Entity("IAR.Models.Note", b =>
                {
                    b.HasOne("IAR.Models.Asset", "Asset")
                        .WithMany("Notes")
                        .HasForeignKey("AssetID");

                    b.Navigation("Asset");
                });

            modelBuilder.Entity("IAR.Models.RetentionPeriod", b =>
                {
                    b.HasOne("IAR.Models.Asset", "Asset")
                        .WithMany("RetentionPeriods")
                        .HasForeignKey("AssetID");

                    b.Navigation("Asset");
                });

            modelBuilder.Entity("IAR.Models.ThirdParty", b =>
                {
                    b.HasOne("IAR.Models.Asset", "Asset")
                        .WithMany("ThirdParties")
                        .HasForeignKey("AssetID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Asset");
                });

            modelBuilder.Entity("IAR.Models.Asset", b =>
                {
                    b.Navigation("Notes");

                    b.Navigation("RetentionPeriods");

                    b.Navigation("ThirdParties");
                });

            modelBuilder.Entity("IAR.Models.BackEndPlatform", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("IAR.Models.FrontEndPlatform", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("IAR.Models.User", b =>
                {
                    b.Navigation("DataOwnerAssets");

                    b.Navigation("DataStewardAssets");

                    b.Navigation("ExecutiveSponsorAssets");
                });
#pragma warning restore 612, 618
        }
    }
}
