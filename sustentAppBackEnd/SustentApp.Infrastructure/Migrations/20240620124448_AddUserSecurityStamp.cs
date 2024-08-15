using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SustentApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSecurityStamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Users",
                type: "varchar(36)",
                maxLength: 36,
                nullable: true
            );

            migrationBuilder.Sql("UPDATE Users SET SecurityStamp = uuid();");

            migrationBuilder.AlterColumn<string>(
                name: "SecurityStamp",
                table: "Users",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "(uuid())"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Users");
        }
    }
}
