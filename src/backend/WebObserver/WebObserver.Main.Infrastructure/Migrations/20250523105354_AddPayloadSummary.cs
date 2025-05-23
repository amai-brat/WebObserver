using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebObserver.Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPayloadSummary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "payload_summary",
                table: "observing_entries",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "payload_summary",
                table: "observing_entries");
        }
    }
}
