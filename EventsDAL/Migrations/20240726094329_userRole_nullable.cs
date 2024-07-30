using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventsDAL.Migrations
{
    /// <inheritdoc />
    public partial class userRole_nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AlterColumn<int>(
                name: "UserRole",
                table: "Users",
                type: "int",
                nullable: true, 
                oldClrType: typeof(int),
                oldType: "int"); ;

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AlterColumn<int>(
                name: "UserRole",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

          
        }
    }
}
