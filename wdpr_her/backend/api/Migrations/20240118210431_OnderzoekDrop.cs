using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class OnderzoekDrop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Onderzoeken_AspNetUsers_UitvoerderId",
                table: "Onderzoeken");

            migrationBuilder.DropForeignKey(
                name: "FK_OnderzoekOnderzoeksType_Onderzoeken_OnderzoekenId",
                table: "OnderzoekOnderzoeksType");

            migrationBuilder.DropTable(
                name: "OpdrachtResponsEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Onderzoeken",
                table: "Onderzoeken");

            migrationBuilder.RenameTable(
                name: "Onderzoeken",
                newName: "Onderzoek");

            migrationBuilder.RenameIndex(
                name: "IX_Onderzoeken_UitvoerderId",
                table: "Onderzoek",
                newName: "IX_Onderzoek_UitvoerderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Onderzoek",
                table: "Onderzoek",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Onderzoek_AspNetUsers_UitvoerderId",
                table: "Onderzoek",
                column: "UitvoerderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OnderzoekOnderzoeksType_Onderzoek_OnderzoekenId",
                table: "OnderzoekOnderzoeksType",
                column: "OnderzoekenId",
                principalTable: "Onderzoek",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Onderzoek_AspNetUsers_UitvoerderId",
                table: "Onderzoek");

            migrationBuilder.DropForeignKey(
                name: "FK_OnderzoekOnderzoeksType_Onderzoek_OnderzoekenId",
                table: "OnderzoekOnderzoeksType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Onderzoek",
                table: "Onderzoek");

            migrationBuilder.RenameTable(
                name: "Onderzoek",
                newName: "Onderzoeken");

            migrationBuilder.RenameIndex(
                name: "IX_Onderzoek_UitvoerderId",
                table: "Onderzoeken",
                newName: "IX_Onderzoeken_UitvoerderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Onderzoeken",
                table: "Onderzoeken",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OpdrachtResponsEntries",
                columns: table => new
                {
                    ResponsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GebruikerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OnderzoekId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VraagMetAntwoordenJSON = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Onderzoeken_AspNetUsers_UitvoerderId",
                table: "Onderzoeken",
                column: "UitvoerderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OnderzoekOnderzoeksType_Onderzoeken_OnderzoekenId",
                table: "OnderzoekOnderzoeksType",
                column: "OnderzoekenId",
                principalTable: "Onderzoeken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
