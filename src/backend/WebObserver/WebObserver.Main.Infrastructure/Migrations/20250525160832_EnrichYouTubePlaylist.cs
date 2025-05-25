using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebObserver.Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EnrichYouTubePlaylist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE you_tube_playlist_item DROP CONSTRAINT pk_you_tube_playlist_item CASCADE"
            );

            migrationBuilder.RenameTable(
                name: "you_tube_playlist_item",
                newName: "you_tube_playlist_items");

            migrationBuilder.AddColumn<string>(
                name: "playlist_name",
                table: "observings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "saved_at",
                table: "you_tube_playlist_items",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "pk_you_tube_playlist_items",
                table: "you_tube_playlist_items",
                column: "id");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 1,
                column: "name",
                value: "Плейлист YouTube");

            migrationBuilder.AddForeignKey(
                name: "fk_unavailable_you_tube_playlist_item_you_tube_playlist_items_",
                table: "unavailable_you_tube_playlist_item",
                column: "current_item_id",
                principalTable: "you_tube_playlist_items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_unavailable_you_tube_playlist_item_you_tube_playlist_items_1",
                table: "unavailable_you_tube_playlist_item",
                column: "saved_item_id",
                principalTable: "you_tube_playlist_items",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_unavailable_you_tube_playlist_item_you_tube_playlist_items_",
                table: "unavailable_you_tube_playlist_item");

            migrationBuilder.DropForeignKey(
                name: "fk_unavailable_you_tube_playlist_item_you_tube_playlist_items_1",
                table: "unavailable_you_tube_playlist_item");

            migrationBuilder.DropPrimaryKey(
                name: "pk_you_tube_playlist_items",
                table: "you_tube_playlist_items");

            migrationBuilder.DropColumn(
                name: "playlist_name",
                table: "observings");

            migrationBuilder.DropColumn(
                name: "saved_at",
                table: "you_tube_playlist_items");

            migrationBuilder.RenameTable(
                name: "you_tube_playlist_items",
                newName: "you_tube_playlist_item");

            migrationBuilder.AddPrimaryKey(
                name: "pk_you_tube_playlist_item",
                table: "you_tube_playlist_item",
                column: "id");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 1,
                column: "name",
                value: "YouTube плейлист");

            migrationBuilder.AddForeignKey(
                name: "fk_unavailable_you_tube_playlist_item_you_tube_playlist_item_c",
                table: "unavailable_you_tube_playlist_item",
                column: "current_item_id",
                principalTable: "you_tube_playlist_item",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_unavailable_you_tube_playlist_item_you_tube_playlist_item_s",
                table: "unavailable_you_tube_playlist_item",
                column: "saved_item_id",
                principalTable: "you_tube_playlist_item",
                principalColumn: "id");
        }
    }
}
