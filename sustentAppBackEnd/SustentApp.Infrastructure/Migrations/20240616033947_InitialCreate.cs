using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SustentApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Document = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    Address_Street = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Address_Number = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Address_Complement = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Address_Neighborhood = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Address_City = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Address_State = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    Address_ZipCode = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    Role = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(now())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
