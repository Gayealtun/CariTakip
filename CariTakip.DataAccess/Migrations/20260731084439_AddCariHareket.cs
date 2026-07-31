using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CariTakip.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCariHareket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Açiklama",
                table: "CariHareketler",
                newName: "Aciklama");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Aciklama",
                table: "CariHareketler",
                newName: "Açiklama");
        }
    }
}
