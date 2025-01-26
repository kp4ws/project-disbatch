using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectDisbatch.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforProjectTypesandDepartments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("994e60b1-778b-4f76-b9f0-ee15c91dd63e"), "1B", "Responsible for experimental design", "Experimental" },
                    { new Guid("bb7376ed-d846-4a13-962f-0e5f6e7370c5"), "1A", "Responsible for taking on new inventions", "New Inventions" },
                    { new Guid("fe4846ac-0060-4cb1-a2ba-74270ad8055c"), "1C", "Responsible for research", "Research" }
                });

            migrationBuilder.InsertData(
                table: "ProjectTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("b0770281-e108-4329-83a4-513cd3146eca"), "Geoscience" },
                    { new Guid("c1b2b207-d7cc-4fff-a637-0fcac3aa7dce"), "Engineering" },
                    { new Guid("fb07258c-7bd6-4905-8b66-a5fef02fae07"), "Rocketry" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("994e60b1-778b-4f76-b9f0-ee15c91dd63e"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("bb7376ed-d846-4a13-962f-0e5f6e7370c5"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("fe4846ac-0060-4cb1-a2ba-74270ad8055c"));

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: new Guid("b0770281-e108-4329-83a4-513cd3146eca"));

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: new Guid("c1b2b207-d7cc-4fff-a637-0fcac3aa7dce"));

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: new Guid("fb07258c-7bd6-4905-8b66-a5fef02fae07"));
        }
    }
}
