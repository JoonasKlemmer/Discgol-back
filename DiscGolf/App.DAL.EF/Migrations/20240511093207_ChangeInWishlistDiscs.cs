using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInWishlistDiscs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscInWishlist_Disc_DiscId",
                table: "DiscInWishlist");

            migrationBuilder.RenameColumn(
                name: "DiscId",
                table: "DiscInWishlist",
                newName: "DiscFromPageId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscInWishlist_DiscId",
                table: "DiscInWishlist",
                newName: "IX_DiscInWishlist_DiscFromPageId");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RefreshToken = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ExpirationDT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PreviousRefreshToken = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    PreviousExpirationDT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AppUserId",
                table: "RefreshTokens",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscInWishlist_DiscsFromPage_DiscFromPageId",
                table: "DiscInWishlist",
                column: "DiscFromPageId",
                principalTable: "DiscsFromPage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscInWishlist_DiscsFromPage_DiscFromPageId",
                table: "DiscInWishlist");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "DiscFromPageId",
                table: "DiscInWishlist",
                newName: "DiscId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscInWishlist_DiscFromPageId",
                table: "DiscInWishlist",
                newName: "IX_DiscInWishlist_DiscId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscInWishlist_Disc_DiscId",
                table: "DiscInWishlist",
                column: "DiscId",
                principalTable: "Disc",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
