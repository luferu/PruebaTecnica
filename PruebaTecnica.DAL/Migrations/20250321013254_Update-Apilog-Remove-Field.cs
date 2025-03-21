using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaTecnica.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApilogRemoveField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponseHeaders",
                table: "ApiLogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponseHeaders",
                table: "ApiLogs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
