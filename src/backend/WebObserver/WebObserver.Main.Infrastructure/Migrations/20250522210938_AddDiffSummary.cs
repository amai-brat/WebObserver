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
                name: "summary",
                table: "diff_you_tube_playlist_diff_payload",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "summary",
                table: "diff_text_diff_payload",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "summary",
                table: "diff_you_tube_playlist_diff_payload");

            migrationBuilder.DropColumn(
                name: "summary",
                table: "diff_text_diff_payload");
        }
    }
}
