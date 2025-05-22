using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebObserver.Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DiffChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "discriminator",
                table: "observing_entries",
                type: "character varying(89)",
                maxLength: 89,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(55)",
                oldMaxLength: 55);

            migrationBuilder.AddForeignKey(
                name: "fk_observing_entries_diff_text_diff_payload_last_diff_first_en",
                table: "observing_entries",
                columns: new[] { "observing_entry_last_diff_first_entry_id", "observing_entry_last_diff_second_entry_id" },
                principalTable: "diff_text_diff_payload",
                principalColumns: new[] { "first_entry_id", "second_entry_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_observing_entries_diff_you_tube_playlist_diff_payload_last_",
                table: "observing_entries",
                columns: new[] { "last_diff_first_entry_id", "last_diff_second_entry_id" },
                principalTable: "diff_you_tube_playlist_diff_payload",
                principalColumns: new[] { "first_entry_id", "second_entry_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_observing_entries_diff_text_diff_payload_last_diff_first_en",
                table: "observing_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_observing_entries_diff_you_tube_playlist_diff_payload_last_",
                table: "observing_entries");

            migrationBuilder.AlterColumn<string>(
                name: "discriminator",
                table: "observing_entries",
                type: "character varying(55)",
                maxLength: 55,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(89)",
                oldMaxLength: 89);
        }
    }
}
