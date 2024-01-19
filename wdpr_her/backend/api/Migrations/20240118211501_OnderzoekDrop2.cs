using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class OnderzoekDrop2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnderzoekOnderzoeksType");

            migrationBuilder.DropTable(
                name: "Onderzoek");

            migrationBuilder.CreateTable(
                name: "OnderzoekOld",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UitvoerderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Locatie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Beloning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnderzoeksData = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnderzoekOldOnderzoeksType");

            migrationBuilder.DropTable(
                name: "OnderzoekOld");

            migrationBuilder.CreateTable(
                name: "Onderzoek",
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
                    table.PrimaryKey("PK_Onderzoek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Onderzoek_AspNetUsers_UitvoerderId",
                        column: x => x.UitvoerderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OnderzoekOnderzoeksType",
                columns: table => new
                {
                    OnderzoekenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OnderzoeksTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnderzoekOnderzoeksType", x => new { x.OnderzoekenId, x.OnderzoeksTypeId });
                    table.ForeignKey(
                        name: "FK_OnderzoekOnderzoeksType_Onderzoek_OnderzoekenId",
                        column: x => x.OnderzoekenId,
                        principalTable: "Onderzoek",
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
                name: "IX_Onderzoek_UitvoerderId",
                table: "Onderzoek",
                column: "UitvoerderId");

            migrationBuilder.CreateIndex(
                name: "IX_OnderzoekOnderzoeksType_OnderzoeksTypeId",
                table: "OnderzoekOnderzoeksType",
                column: "OnderzoeksTypeId");
        }
    }
}
