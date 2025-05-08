using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebObserver.Main.Domain.Text;
using WebObserver.Main.Domain.YouTubePlaylist;

#nullable disable

namespace WebObserver.Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "templates",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    discriminator = table.Column<string>(type: "character varying(34)", maxLength: 34, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_templates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "you_tube_playlist_item",
                columns: table => new
                {
                    video_id = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    position = table.Column<long>(type: "bigint", nullable: false),
                    thumbnail_url = table.Column<string>(type: "text", nullable: true),
                    published_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    video_owner_channel_title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_you_tube_playlist_item", x => x.video_id);
                });

            migrationBuilder.CreateTable(
                name: "observings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    started_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ended_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    template_id = table.Column<int>(type: "integer", nullable: false),
                    cron_expression = table.Column<string>(type: "text", nullable: false),
                    discriminator = table.Column<string>(type: "character varying(34)", maxLength: 34, nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    url = table.Column<string>(type: "text", nullable: true),
                    playlist_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_observings", x => x.id);
                    table.ForeignKey(
                        name: "fk_observings_templates_template_id",
                        column: x => x.template_id,
                        principalTable: "templates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_observings_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

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
                });

            migrationBuilder.CreateTable(
                name: "observing_entries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    observing_id = table.Column<int>(type: "integer", nullable: false),
                    occured_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    discriminator = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: false),
                    observing_entry_payload_observing_id = table.Column<int>(type: "integer", nullable: true),
                    observing_entry_last_diff_first_entry_id = table.Column<int>(type: "integer", nullable: true),
                    observing_entry_last_diff_second_entry_id = table.Column<int>(type: "integer", nullable: true),
                    payload_observing_id = table.Column<int>(type: "integer", nullable: true),
                    last_diff_first_entry_id = table.Column<int>(type: "integer", nullable: true),
                    last_diff_second_entry_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_observing_entries", x => x.id);
                    table.ForeignKey(
                        name: "fk_observing_entries_observings_observing_id",
                        column: x => x.observing_id,
                        principalTable: "observings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "text_payload",
                columns: table => new
                {
                    observing_id = table.Column<int>(type: "integer", nullable: false),
                    text = table.Column<string>(type: "text", maxLength: -1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_text_payload", x => x.observing_id);
                    table.ForeignKey(
                        name: "FK_text_payload_observing_entries_observing_id",
                        column: x => x.observing_id,
                        principalTable: "observing_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "you_tube_playlist_payload",
                columns: table => new
                {
                    observing_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_you_tube_playlist_payload", x => x.observing_id);
                    table.ForeignKey(
                        name: "FK_you_tube_playlist_payload_observing_entries_observing_id",
                        column: x => x.observing_id,
                        principalTable: "observing_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "you_tube_playlist_item_you_tube_playlist_payload",
                columns: table => new
                {
                    items_video_id = table.Column<string>(type: "text", nullable: false),
                    you_tube_playlist_payload_observing_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_you_tube_playlist_item_you_tube_playlist_payload", x => new { x.items_video_id, x.you_tube_playlist_payload_observing_id });
                    table.ForeignKey(
                        name: "fk_you_tube_playlist_item_you_tube_playlist_payload_you_tube_p",
                        column: x => x.items_video_id,
                        principalTable: "you_tube_playlist_item",
                        principalColumn: "video_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_you_tube_playlist_item_you_tube_playlist_payload_you_tube_p1",
                        column: x => x.you_tube_playlist_payload_observing_id,
                        principalTable: "you_tube_playlist_payload",
                        principalColumn: "observing_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_diff_text_diff_payload_second_entry_id",
                table: "diff_text_diff_payload",
                column: "second_entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_diff_you_tube_playlist_diff_payload_second_entry_id",
                table: "diff_you_tube_playlist_diff_payload",
                column: "second_entry_id");

            migrationBuilder.CreateIndex(
                name: "ix_observing_entries_last_diff_first_entry_id_last_diff_second",
                table: "observing_entries",
                columns: new[] { "last_diff_first_entry_id", "last_diff_second_entry_id" });

            migrationBuilder.CreateIndex(
                name: "ix_observing_entries_last_diff_first_entry_id_last_diff_second1",
                table: "observing_entries",
                columns: new[] { "observing_entry_last_diff_first_entry_id", "observing_entry_last_diff_second_entry_id" });

            migrationBuilder.CreateIndex(
                name: "ix_observing_entries_observing_id",
                table: "observing_entries",
                column: "observing_id");

            migrationBuilder.CreateIndex(
                name: "ix_observing_entries_payload_observing_id",
                table: "observing_entries",
                column: "payload_observing_id");

            migrationBuilder.CreateIndex(
                name: "ix_observing_entries_payload_observing_id1",
                table: "observing_entries",
                column: "observing_entry_payload_observing_id");

            migrationBuilder.CreateIndex(
                name: "ix_observings_template_id",
                table: "observings",
                column: "template_id");

            migrationBuilder.CreateIndex(
                name: "ix_observings_user_id",
                table: "observings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_you_tube_playlist_item_you_tube_playlist_payload_you_tube_p",
                table: "you_tube_playlist_item_you_tube_playlist_payload",
                column: "you_tube_playlist_payload_observing_id");

            migrationBuilder.AddForeignKey(
                name: "FK_diff_text_diff_payload_observing_entries_first_entry_id",
                table: "diff_text_diff_payload",
                column: "first_entry_id",
                principalTable: "observing_entries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_diff_text_diff_payload_observing_entries_second_entry_id",
                table: "diff_text_diff_payload",
                column: "second_entry_id",
                principalTable: "observing_entries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_diff_you_tube_playlist_diff_payload_observing_entries_first~",
                table: "diff_you_tube_playlist_diff_payload",
                column: "first_entry_id",
                principalTable: "observing_entries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_diff_you_tube_playlist_diff_payload_observing_entries_secon~",
                table: "diff_you_tube_playlist_diff_payload",
                column: "second_entry_id",
                principalTable: "observing_entries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_observing_entries_text_payload_payload_observing_id",
                table: "observing_entries",
                column: "observing_entry_payload_observing_id",
                principalTable: "text_payload",
                principalColumn: "observing_id");

            migrationBuilder.AddForeignKey(
                name: "fk_observing_entries_you_tube_playlist_payload_payload_observi",
                table: "observing_entries",
                column: "payload_observing_id",
                principalTable: "you_tube_playlist_payload",
                principalColumn: "observing_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_text_payload_observing_entries_observing_id",
                table: "text_payload");

            migrationBuilder.DropForeignKey(
                name: "FK_you_tube_playlist_payload_observing_entries_observing_id",
                table: "you_tube_playlist_payload");

            migrationBuilder.DropTable(
                name: "diff_text_diff_payload");

            migrationBuilder.DropTable(
                name: "diff_you_tube_playlist_diff_payload");

            migrationBuilder.DropTable(
                name: "you_tube_playlist_item_you_tube_playlist_payload");

            migrationBuilder.DropTable(
                name: "you_tube_playlist_item");

            migrationBuilder.DropTable(
                name: "observing_entries");

            migrationBuilder.DropTable(
                name: "observings");

            migrationBuilder.DropTable(
                name: "text_payload");

            migrationBuilder.DropTable(
                name: "you_tube_playlist_payload");

            migrationBuilder.DropTable(
                name: "templates");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
