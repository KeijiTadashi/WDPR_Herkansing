using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class removeEnums4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beperkingen_AspNetUsers_ErvaringsdeskundigeId",
                table: "Beperkingen");

            migrationBuilder.DropForeignKey(
                name: "FK_Onderzoeken_OnderzoeksTypes_OnderzoeksTypeId",
                table: "Onderzoeken");

            migrationBuilder.DropForeignKey(
                name: "FK_OnderzoeksTypes_AspNetUsers_ErvaringsdeskundigeId",
                table: "OnderzoeksTypes");

            migrationBuilder.DropIndex(
                name: "IX_OnderzoeksTypes_ErvaringsdeskundigeId",
                table: "OnderzoeksTypes");

            migrationBuilder.DropIndex(
                name: "IX_Onderzoeken_OnderzoeksTypeId",
                table: "Onderzoeken");

            migrationBuilder.DropIndex(
                name: "IX_Beperkingen_ErvaringsdeskundigeId",
                table: "Beperkingen");

            migrationBuilder.DropColumn(
                name: "ErvaringsdeskundigeId",
                table: "OnderzoeksTypes");

            migrationBuilder.DropColumn(
                name: "OnderzoeksTypeId",
                table: "Onderzoeken");

            migrationBuilder.DropColumn(
                name: "ErvaringsdeskundigeId",
                table: "Beperkingen");

            migrationBuilder.CreateTable(
                name: "BeperkingErvaringsdeskundige",
                columns: table => new
                {
                    BeperkingId = table.Column<int>(type: "int", nullable: false),
                    ErvaringsdeskundigenId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeperkingErvaringsdeskundige", x => new { x.BeperkingId, x.ErvaringsdeskundigenId });
                    table.ForeignKey(
                        name: "FK_BeperkingErvaringsdeskundige_AspNetUsers_ErvaringsdeskundigenId",
                        column: x => x.ErvaringsdeskundigenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeperkingErvaringsdeskundige_Beperkingen_BeperkingId",
                        column: x => x.BeperkingId,
                        principalTable: "Beperkingen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErvaringsdeskundigeOnderzoeksType",
                columns: table => new
                {
                    ErvaringsdeskundigenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VoorkeurOnderzoekId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErvaringsdeskundigeOnderzoeksType", x => new { x.ErvaringsdeskundigenId, x.VoorkeurOnderzoekId });
                    table.ForeignKey(
                        name: "FK_ErvaringsdeskundigeOnderzoeksType_AspNetUsers_ErvaringsdeskundigenId",
                        column: x => x.ErvaringsdeskundigenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ErvaringsdeskundigeOnderzoeksType_OnderzoeksTypes_VoorkeurOnderzoekId",
                        column: x => x.VoorkeurOnderzoekId,
                        principalTable: "OnderzoeksTypes",
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
                name: "IX_BeperkingErvaringsdeskundige_ErvaringsdeskundigenId",
                table: "BeperkingErvaringsdeskundige",
                column: "ErvaringsdeskundigenId");

            migrationBuilder.CreateIndex(
                name: "IX_ErvaringsdeskundigeOnderzoeksType_VoorkeurOnderzoekId",
                table: "ErvaringsdeskundigeOnderzoeksType",
                column: "VoorkeurOnderzoekId");

            migrationBuilder.CreateIndex(
                name: "IX_OnderzoekOnderzoeksType_OnderzoeksTypeId",
                table: "OnderzoekOnderzoeksType",
                column: "OnderzoeksTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeperkingErvaringsdeskundige");

            migrationBuilder.DropTable(
                name: "ErvaringsdeskundigeOnderzoeksType");

            migrationBuilder.DropTable(
                name: "OnderzoekOnderzoeksType");

            migrationBuilder.AddColumn<string>(
                name: "ErvaringsdeskundigeId",
                table: "OnderzoeksTypes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OnderzoeksTypeId",
                table: "Onderzoeken",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ErvaringsdeskundigeId",
                table: "Beperkingen",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OnderzoeksTypes_ErvaringsdeskundigeId",
                table: "OnderzoeksTypes",
                column: "ErvaringsdeskundigeId");

            migrationBuilder.CreateIndex(
                name: "IX_Onderzoeken_OnderzoeksTypeId",
                table: "Onderzoeken",
                column: "OnderzoeksTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Beperkingen_ErvaringsdeskundigeId",
                table: "Beperkingen",
                column: "ErvaringsdeskundigeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beperkingen_AspNetUsers_ErvaringsdeskundigeId",
                table: "Beperkingen",
                column: "ErvaringsdeskundigeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Onderzoeken_OnderzoeksTypes_OnderzoeksTypeId",
                table: "Onderzoeken",
                column: "OnderzoeksTypeId",
                principalTable: "OnderzoeksTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OnderzoeksTypes_AspNetUsers_ErvaringsdeskundigeId",
                table: "OnderzoeksTypes",
                column: "ErvaringsdeskundigeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
