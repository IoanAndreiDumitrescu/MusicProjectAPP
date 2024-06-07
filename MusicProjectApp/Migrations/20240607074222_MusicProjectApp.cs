using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicProjectApp.Migrations
{
    /// <inheritdoc />
    public partial class MusicProjectApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albumes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Genero = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: true),
                    Titulo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albumes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artistas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Genero = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artistas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Canciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ArtistaId = table.Column<int>(type: "int", nullable: true),
                    AlbumId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Canciones_Albumes",
                        column: x => x.AlbumId,
                        principalTable: "Albumes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Canciones_Artistas",
                        column: x => x.ArtistaId,
                        principalTable: "Artistas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Festival",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ArtistaId = table.Column<int>(type: "int", nullable: true),
                    Ciudad = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    FechaInicio = table.Column<DateOnly>(type: "date", nullable: true),
                    FechaFinal = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Festival", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Festival_Artistas",
                        column: x => x.ArtistaId,
                        principalTable: "Artistas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Canciones_AlbumId",
                table: "Canciones",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Canciones_ArtistaId",
                table: "Canciones",
                column: "ArtistaId");

            migrationBuilder.CreateIndex(
                name: "IX_Festival_ArtistaId",
                table: "Festival",
                column: "ArtistaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Canciones");

            migrationBuilder.DropTable(
                name: "Festival");

            migrationBuilder.DropTable(
                name: "Albumes");

            migrationBuilder.DropTable(
                name: "Artistas");
        }
    }
}
