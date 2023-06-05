using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAR.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Table = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableID = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BackEndPlatforms",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackEndPlatforms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FrontEndPlatforms",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrontEndPlatforms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tooltips",
                columns: table => new
                {
                    FieldName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TooltipText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tooltips", x => x.FieldName);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    AccountName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.AccountName);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecutiveSponsorAccountName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataOwnerAccountName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataStewardAccountName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastReviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewCycleMonths = table.Column<byte>(type: "tinyint", nullable: false),
                    NextReviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BackEndPlatformID = table.Column<int>(type: "int", nullable: true),
                    BackEndPlatformLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrontEndPlatformID = table.Column<int>(type: "int", nullable: true),
                    FrontEndPlatformLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalLocation = table.Column<int>(type: "int", nullable: true),
                    Volume = table.Column<int>(type: "int", nullable: true),
                    PersonalData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Restricted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaintainedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LawfulBasisConsent = table.Column<bool>(type: "bit", nullable: false),
                    LawfulBasisConsentDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LawfulBasisContract = table.Column<bool>(type: "bit", nullable: false),
                    LawfulBasisContractDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LawfulBasisLegalObligation = table.Column<bool>(type: "bit", nullable: false),
                    LawfulBasisLegalObligationDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LawfulBasisVitalInterest = table.Column<bool>(type: "bit", nullable: false),
                    LawfulBasisVitalInterestDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LawfulBasisLegitimateInterest = table.Column<bool>(type: "bit", nullable: false),
                    LawfulBasisLegitimateInterestDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialRacialEthnic = table.Column<bool>(type: "bit", nullable: false),
                    SpecialPoliticalOpinion = table.Column<bool>(type: "bit", nullable: false),
                    SpecialReligiousPhilosophical = table.Column<bool>(type: "bit", nullable: false),
                    SpecialTradeUnion = table.Column<bool>(type: "bit", nullable: false),
                    SpecialGenetic = table.Column<bool>(type: "bit", nullable: false),
                    SpecialBiometric = table.Column<bool>(type: "bit", nullable: false),
                    SpecialHealth = table.Column<bool>(type: "bit", nullable: false),
                    SpecialSexual = table.Column<bool>(type: "bit", nullable: false),
                    CriminalConviction = table.Column<bool>(type: "bit", nullable: false),
                    Children = table.Column<bool>(type: "bit", nullable: false),
                    SubjectToDPA = table.Column<bool>(type: "bit", nullable: false),
                    DataSubjects = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryPurpose = table.Column<bool>(type: "bit", nullable: false),
                    SecondaryPurposeDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonalDetails = table.Column<bool>(type: "bit", nullable: false),
                    GoodsServices = table.Column<bool>(type: "bit", nullable: false),
                    SupplierDetails = table.Column<bool>(type: "bit", nullable: false),
                    FinancialDetails = table.Column<bool>(type: "bit", nullable: false),
                    LifestyleSocial = table.Column<bool>(type: "bit", nullable: false),
                    Complaints = table.Column<bool>(type: "bit", nullable: false),
                    EducationEmployment = table.Column<bool>(type: "bit", nullable: false),
                    HealthSafetySecurity = table.Column<bool>(type: "bit", nullable: false),
                    VisualImages = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Assets_BackEndPlatforms_BackEndPlatformID",
                        column: x => x.BackEndPlatformID,
                        principalTable: "BackEndPlatforms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Assets_FrontEndPlatforms_FrontEndPlatformID",
                        column: x => x.FrontEndPlatformID,
                        principalTable: "FrontEndPlatforms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Assets_Users_DataOwnerAccountName",
                        column: x => x.DataOwnerAccountName,
                        principalTable: "Users",
                        principalColumn: "AccountName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assets_Users_DataStewardAccountName",
                        column: x => x.DataStewardAccountName,
                        principalTable: "Users",
                        principalColumn: "AccountName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assets_Users_ExecutiveSponsorAccountName",
                        column: x => x.ExecutiveSponsorAccountName,
                        principalTable: "Users",
                        principalColumn: "AccountName",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetID = table.Column<int>(type: "int", nullable: false),
                    NoteText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notes_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "RetentionPeriods",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetID = table.Column<int>(type: "int", nullable: false),
                    RetainedData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RetentionPeriodMonths = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetentionPeriods", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RetentionPeriods_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ThirdParties",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetID = table.Column<int>(type: "int", nullable: false),
                    ThirdPartyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Use = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<int>(type: "int", nullable: true),
                    LocationDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdParties", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ThirdParties_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_BackEndPlatformID",
                table: "Assets",
                column: "BackEndPlatformID");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_DataOwnerAccountName",
                table: "Assets",
                column: "DataOwnerAccountName");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_DataStewardAccountName",
                table: "Assets",
                column: "DataStewardAccountName");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ExecutiveSponsorAccountName",
                table: "Assets",
                column: "ExecutiveSponsorAccountName");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_FrontEndPlatformID",
                table: "Assets",
                column: "FrontEndPlatformID");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_AssetID",
                table: "Notes",
                column: "AssetID");

            migrationBuilder.CreateIndex(
                name: "IX_RetentionPeriods_AssetID",
                table: "RetentionPeriods",
                column: "AssetID");

            migrationBuilder.CreateIndex(
                name: "IX_ThirdParties_AssetID",
                table: "ThirdParties",
                column: "AssetID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "RetentionPeriods");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "ThirdParties");

            migrationBuilder.DropTable(
                name: "Tooltips");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "BackEndPlatforms");

            migrationBuilder.DropTable(
                name: "FrontEndPlatforms");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
