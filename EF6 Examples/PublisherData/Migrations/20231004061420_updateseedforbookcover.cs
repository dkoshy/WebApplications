using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublisherData.Migrations
{
    public partial class updateseedforbookcover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Covers",
                keyColumn: "CoverId",
                keyValue: 1,
                column: "BookId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Covers",
                keyColumn: "CoverId",
                keyValue: 2,
                column: "BookId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Covers",
                keyColumn: "CoverId",
                keyValue: 3,
                column: "BookId",
                value: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Covers",
                keyColumn: "CoverId",
                keyValue: 1,
                column: "BookId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Covers",
                keyColumn: "CoverId",
                keyValue: 2,
                column: "BookId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Covers",
                keyColumn: "CoverId",
                keyValue: 3,
                column: "BookId",
                value: null);
        }
    }
}
