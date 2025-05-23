using Microsoft.EntityFrameworkCore.Migrations;
using WebObserver.Main.Domain.Base;
using WebObserver.Main.Domain.Text;
using WebObserver.Main.Domain.YouTubePlaylist;

#nullable disable

namespace WebObserver.Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGenericsFromDomainModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_observing_entries_diff_text_diff_payload_last_diff_first_en",
                table: "observing_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_observing_entries_diff_you_tube_playlist_diff_payload_last_",
                table: "observing_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_text_payload_observing_entries_observing_entry_id",
                table: "text_payload");

            migrationBuilder.DropForeignKey(
                name: "fk_you_tube_playlist_payload_observing_entries_observing_entry",
                table: "you_tube_playlist_payload");

            migrationBuilder.DropTable(
                name: "diff_text_diff_payload");

            migrationBuilder.DropTable(
                name: "diff_you_tube_playlist_diff_payload");

            migrationBuilder.DropIndex(
                name: "ix_observing_entries_last_diff_first_entry_id_last_diff_second1",
                table: "observing_entries");

            migrationBuilder.DropColumn(
                name: "observing_entry_last_diff_first_entry_id",
                table: "observing_entries");

            migrationBuilder.DropColumn(
                name: "observing_entry_last_diff_second_entry_id",
                table: "observing_entries");

            migrationBuilder.AlterColumn<string>(
                name: "discriminator",
                table: "observing_entries",
                type: "character varying(34)",
                maxLength: 34,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(89)",
                oldMaxLength: 89);

            migrationBuilder.CreateTable(
                name: "text_diff",
                columns: table => new
                {
                    first_entry_id = table.Column<int>(type: "integer", nullable: false),
                    second_entry_id = table.Column<int>(type: "integer", nullable: false),
                    payload = table.Column<DiffPayload>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_text_diff", x => new { x.first_entry_id, x.second_entry_id });
                    table.ForeignKey(
                        name: "FK_text_diff_observing_entries_first_entry_id",
                        column: x => x.first_entry_id,
                        principalTable: "observing_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_text_diff_observing_entries_second_entry_id",
                        column: x => x.second_entry_id,
                        principalTable: "observing_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "you_tube_playlist_diff",
                columns: table => new
                {
                    first_entry_id = table.Column<int>(type: "integer", nullable: false),
                    second_entry_id = table.Column<int>(type: "integer", nullable: false),
                    payload = table.Column<DiffPayload>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_you_tube_playlist_diff", x => new { x.first_entry_id, x.second_entry_id });
                    table.ForeignKey(
                        name: "FK_you_tube_playlist_diff_observing_entries_first_entry_id",
                        column: x => x.first_entry_id,
                        principalTable: "observing_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_you_tube_playlist_diff_observing_entries_second_entry_id",
                        column: x => x.second_entry_id,
                        principalTable: "observing_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_text_diff_second_entry_id",
                table: "text_diff",
                column: "second_entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_you_tube_playlist_diff_second_entry_id",
                table: "you_tube_playlist_diff",
                column: "second_entry_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "text_diff");

            migrationBuilder.DropTable(
                name: "you_tube_playlist_diff");

            migrationBuilder.AlterColumn<string>(
                name: "discriminator",
                table: "observing_entries",
                type: "character varying(89)",
                maxLength: 89,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(34)",
                oldMaxLength: 34);

            migrationBuilder.AddColumn<int>(
                name: "observing_entry_last_diff_first_entry_id",
                table: "observing_entries",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "observing_entry_last_diff_second_entry_id",
                table: "observing_entries",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "diff_text_diff_payload",
                columns: table => new
                {
                    first_entry_id = table.Column<int>(type: "integer", nullable: false),
                    second_entry_id = table.Column<int>(type: "integer", nullable: false),
                    payload = table.Column<TextDiffPayload>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diff_text_diff_payload", x => new { x.first_entry_id, x.second_entry_id });
                    table.ForeignKey(
                        name: "FK_diff_text_diff_payload_observing_entries_first_entry_id",
                        column: x => x.first_entry_id,
                        principalTable: "observing_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_diff_text_diff_payload_observing_entries_second_entry_id",
                        column: x => x.second_entry_id,
                        principalTable: "observing_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "diff_you_tube_playlist_diff_payload",
                columns: table => new
                {
                    first_entry_id = table.Column<int>(type: "integer", nullable: false),
                    second_entry_id = table.Column<int>(type: "integer", nullable: false),
                    payload = table.Column<YouTubePlaylistDiffPayload>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diff_you_tube_playlist_diff_payload", x => new { x.first_entry_id, x.second_entry_id });
                    table.ForeignKey(
                        name: "FK_diff_you_tube_playlist_diff_payload_observing_entries_first~",
                        column: x => x.first_entry_id,
                        principalTable: "observing_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_diff_you_tube_playlist_diff_payload_observing_entries_secon~",
                        column: x => x.second_entry_id,
                        principalTable: "observing_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_observing_entries_last_diff_first_entry_id_last_diff_second1",
                table: "observing_entries",
                columns: new[] { "observing_entry_last_diff_first_entry_id", "observing_entry_last_diff_second_entry_id" });

            migrationBuilder.CreateIndex(
                name: "IX_diff_text_diff_payload_second_entry_id",
                table: "diff_text_diff_payload",
                column: "second_entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_diff_you_tube_playlist_diff_payload_second_entry_id",
                table: "diff_you_tube_playlist_diff_payload",
                column: "second_entry_id");

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

            migrationBuilder.AddForeignKey(
                name: "fk_text_payload_observing_entries_observing_entry_id",
                table: "text_payload",
                column: "observing_entry_id",
                principalTable: "observing_entries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_you_tube_playlist_payload_observing_entries_observing_entry",
                table: "you_tube_playlist_payload",
                column: "observing_entry_id",
                principalTable: "observing_entries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
