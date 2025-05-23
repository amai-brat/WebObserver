using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebObserver.Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDiffSummary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "diff_summary",
                table: "observing_entries",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "diff_summary",
                table: "observing_entries");
        }
    }
}
