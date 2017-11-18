using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ifc_web_viewer.Data.Migrations
{
    public partial class UserShimei : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserShimei",
                table: "AspNetUsers",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserShimeiKana",
                table: "AspNetUsers",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserShimei",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserShimeiKana",
                table: "AspNetUsers");
        }
    }
}
