using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeePortal.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentUuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EmploymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Uuid);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentUuid",
                        column: x => x.DepartmentUuid,
                        principalTable: "Departments",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentUuid",
                table: "Employees",
                column: "DepartmentUuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
