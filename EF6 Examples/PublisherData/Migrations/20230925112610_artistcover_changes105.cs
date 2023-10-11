using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublisherData.Migrations
{
    public partial class artistcover_changes105 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistCover");

            migrationBuilder.CreateTable(
                name: "ArtistCovers",
                columns: table => new
                {
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    CoverId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistCovers", x => new { x.ArtistId, x.CoverId });
                    table.ForeignKey(
                        name: "FK_ArtistCovers_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistCovers_Covers_CoverId",
                        column: x => x.CoverId,
                        principalTable: "Covers",
                        principalColumn: "CoverId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistCovers_CoverId",
                table: "ArtistCovers",
                column: "CoverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistCovers");

            migrationBuilder.CreateTable(
                name: "ArtistCover",
                columns: table => new
                {
                    ArtistsArtistId = table.Column<int>(type: "int", nullable: false),
                    CoversCoverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistCover", x => new { x.ArtistsArtistId, x.CoversCoverId });
                    table.ForeignKey(
                        name: "FK_ArtistCover_Artists_ArtistsArtistId",
                        column: x => x.ArtistsArtistId,
                        principalTable: "Artists",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistCover_Covers_CoversCoverId",
                        column: x => x.CoversCoverId,
                        principalTable: "Covers",
                        principalColumn: "CoverId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistCover_CoversCoverId",
                table: "ArtistCover",
                column: "CoversCoverId");
        }
    }
}
