using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectDisbatch.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "DepartmentId", "Description", "Name", "ProjectTypeId" },
                values: new object[,]
                {
                    { new Guid("6b452b93-d55f-41f7-8cd3-d20f9726372d"), new Guid("994e60b1-778b-4f76-b9f0-ee15c91dd63e"), "", "Project 2", new Guid("c1b2b207-d7cc-4fff-a637-0fcac3aa7dce") },
                    { new Guid("9a9cb972-3f9f-4e78-8459-bd52a0b2853a"), new Guid("fe4846ac-0060-4cb1-a2ba-74270ad8055c"), "", "Project 1", new Guid("c1b2b207-d7cc-4fff-a637-0fcac3aa7dce") },
                    { new Guid("ffc3920f-1e35-41dd-9f19-465019868fa3"), new Guid("bb7376ed-d846-4a13-962f-0e5f6e7370c5"), "", "Project 3", new Guid("b0770281-e108-4329-83a4-513cd3146eca") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("6b452b93-d55f-41f7-8cd3-d20f9726372d"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("9a9cb972-3f9f-4e78-8459-bd52a0b2853a"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("ffc3920f-1e35-41dd-9f19-465019868fa3"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
