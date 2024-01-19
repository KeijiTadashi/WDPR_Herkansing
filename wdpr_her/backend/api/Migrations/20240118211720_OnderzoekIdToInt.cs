using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class OnderzoekIdToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnderzoekOldOnderzoeksType");

            migrationBuilder.DropTable(
                name: "OnderzoekOld");

            migrationBuilder.CreateTable(
                name: "Onderzoeken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UitvoerderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Locatie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Beloning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnderzoeksData = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onderzoeken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Onderzoeken_AspNetUsers_UitvoerderId",
                        column: x => x.UitvoerderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpdrachtResponsEntries",
                columns: table => new
                {
                    ResponsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OnderzoekId = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "OnderzoekOnderzoeksType",
                columns: table => new
                {
                    OnderzoekenId = table.Column<int>(type: "int", nullable: false),
                    OnderzoeksTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnderzoekOnderzoeksType", x => new { x.OnderzoekenId, x.OnderzoeksTypeId });
                    table.ForeignKey(
                        name: "FK_OnderzoekOnderzoeksType_Onderzoeken_OnderzoekenId",
                        column: x => x.OnderzoekenId,
                        principalTable: "Onderzoeken",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OnderzoekOnderzoeksType_OnderzoeksTypes_OnderzoeksTypeId",
                        column: x => x.OnderzoeksTypeId,
                        principalTable: "OnderzoeksTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Onderzoeken_UitvoerderId",
                table: "Onderzoeken",
                column: "UitvoerderId");

            migrationBuilder.CreateIndex(
                name: "IX_OnderzoekOnderzoeksType_OnderzoeksTypeId",
                table: "OnderzoekOnderzoeksType",
                column: "OnderzoeksTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OpdrachtResponsEntries_GebruikerId",
                table: "OpdrachtResponsEntries",
                column: "GebruikerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnderzoekOnderzoeksType");

            migrationBuilder.DropTable(
                name: "OpdrachtResponsEntries");

            migrationBuilder.DropTable(
                name: "Onderzoeken");

            migrationBuilder.CreateTable(
                name: "OnderzoekOld",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UitvoerderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Beloning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Locatie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnderzoeksData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnderzoekOld", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnderzoekOld_AspNetUsers_UitvoerderId",
                        column: x => x.UitvoerderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OnderzoekOldOnderzoeksType",
                columns: table => new
                {
                    OnderzoekenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OnderzoeksTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnderzoekOldOnderzoeksType", x => new { x.OnderzoekenId, x.OnderzoeksTypeId });
                    table.ForeignKey(
                        name: "FK_OnderzoekOldOnderzoeksType_OnderzoekOld_OnderzoekenId",
                        column: x => x.OnderzoekenId,
                        principalTable: "OnderzoekOld",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OnderzoekOldOnderzoeksType_OnderzoeksTypes_OnderzoeksTypeId",
                        column: x => x.OnderzoeksTypeId,
                        principalTable: "OnderzoeksTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OnderzoekOld_UitvoerderId",
                table: "OnderzoekOld",
                column: "UitvoerderId");

            migrationBuilder.CreateIndex(
                name: "IX_OnderzoekOldOnderzoeksType_OnderzoeksTypeId",
                table: "OnderzoekOldOnderzoeksType",
                column: "OnderzoeksTypeId");
        }
    }
}
