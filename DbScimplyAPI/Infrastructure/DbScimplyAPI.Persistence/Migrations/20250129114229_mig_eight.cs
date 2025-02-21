using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbScimplyAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_eight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTwoFactor",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "TwoFactorCode",
                table: "Admins");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTwoFactor",
                table: "Admins",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TwoFactorCode",
                table: "Admins",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
