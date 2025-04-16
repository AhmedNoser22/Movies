using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesApi.Migrations
{
    /// <inheritdoc />
    public partial class fal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genras_genraId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_genraId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "genraId",
                table: "Movies");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreId",
                table: "Movies",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genras_GenreId",
                table: "Movies",
                column: "GenreId",
                principalTable: "Genras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genras_GenreId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_GenreId",
                table: "Movies");

            migrationBuilder.AddColumn<byte>(
                name: "genraId",
                table: "Movies",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_genraId",
                table: "Movies",
                column: "genraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genras_genraId",
                table: "Movies",
                column: "genraId",
                principalTable: "Genras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
