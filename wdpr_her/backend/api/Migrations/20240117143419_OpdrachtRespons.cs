using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class OpdrachtRespons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpdrachtResponsEntries",
                columns: table => new
                {
                    ResponsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnderzoekId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VraagMetAntwoordenJSON = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GebruikerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpdrachtResponsEntries", x => x.ResponsId);
                    table.ForeignKey(
                        name: "FK_OpdrachtResponsEntries_AspNetUsers_GebruikerId",
                        column: x => x.GebruikerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpdrachtResponsEntries_GebruikerId",
                table: "OpdrachtResponsEntries",
                column: "GebruikerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpdrachtResponsEntries");
        }
    }
}
