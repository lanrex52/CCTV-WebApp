using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CCTV_App.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Audits");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNo",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Audits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Audits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Audits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Audits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MobileNo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Audits");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Audits");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Audits");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Audits");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Audits",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
