﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAR.Migrations
{
    /// <inheritdoc />
    public partial class AddTooltipAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Tooltips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Tooltips",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Tooltips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Tooltips",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Tooltips");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Tooltips");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Tooltips");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Tooltips");
        }
    }
}
