using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class removeEnums3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aandoeningen_AspNetUsers_ErvaringsdeskundigeId",
                table: "Aandoeningen");

            migrationBuilder.DropForeignKey(
                name: "FK_Hulpmiddelen_AspNetUsers_ErvaringsdeskundigeId",
                table: "Hulpmiddelen");

            migrationBuilder.DropIndex(
                name: "IX_Hulpmiddelen_ErvaringsdeskundigeId",
                table: "Hulpmiddelen");

            migrationBuilder.DropIndex(
                name: "IX_Aandoeningen_ErvaringsdeskundigeId",
                table: "Aandoeningen");

            migrationBuilder.DropColumn(
                name: "ErvaringsdeskundigeId",
                table: "Hulpmiddelen");

            migrationBuilder.DropColumn(
                name: "ErvaringsdeskundigeId",
                table: "Aandoeningen");

            migrationBuilder.CreateTable(
                name: "AandoeningErvaringsdeskundige",
                columns: table => new
                {
                    AandoendingId = table.Column<int>(type: "int", nullable: false),
                    ErvaringsdeskundigenId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AandoeningErvaringsdeskundige", x => new { x.AandoendingId, x.ErvaringsdeskundigenId });
                    table.ForeignKey(
                        name: "FK_AandoeningErvaringsdeskundige_Aandoeningen_AandoendingId",
                        column: x => x.AandoendingId,
                        principalTable: "Aandoeningen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AandoeningErvaringsdeskundige_AspNetUsers_ErvaringsdeskundigenId",
                        column: x => x.ErvaringsdeskundigenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErvaringsdeskundigeHulpmiddel",
                columns: table => new
                {
                    ErvaringsdeskundigenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HulpmiddelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErvaringsdeskundigeHulpmiddel", x => new { x.ErvaringsdeskundigenId, x.HulpmiddelId });
                    table.ForeignKey(
                        name: "FK_ErvaringsdeskundigeHulpmiddel_AspNetUsers_ErvaringsdeskundigenId",
                        column: x => x.ErvaringsdeskundigenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ErvaringsdeskundigeHulpmiddel_Hulpmiddelen_HulpmiddelId",
                        column: x => x.HulpmiddelId,
                        principalTable: "Hulpmiddelen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AandoeningErvaringsdeskundige_ErvaringsdeskundigenId",
                table: "AandoeningErvaringsdeskundige",
                column: "ErvaringsdeskundigenId");

            migrationBuilder.CreateIndex(
                name: "IX_ErvaringsdeskundigeHulpmiddel_HulpmiddelId",
                table: "ErvaringsdeskundigeHulpmiddel",
                column: "HulpmiddelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AandoeningErvaringsdeskundige");

            migrationBuilder.DropTable(
                name: "ErvaringsdeskundigeHulpmiddel");

            migrationBuilder.AddColumn<string>(
                name: "ErvaringsdeskundigeId",
                table: "Hulpmiddelen",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ErvaringsdeskundigeId",
                table: "Aandoeningen",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hulpmiddelen_ErvaringsdeskundigeId",
                table: "Hulpmiddelen",
                column: "ErvaringsdeskundigeId");

            migrationBuilder.CreateIndex(
                name: "IX_Aandoeningen_ErvaringsdeskundigeId",
                table: "Aandoeningen",
                column: "ErvaringsdeskundigeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aandoeningen_AspNetUsers_ErvaringsdeskundigeId",
                table: "Aandoeningen",
                column: "ErvaringsdeskundigeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hulpmiddelen_AspNetUsers_ErvaringsdeskundigeId",
                table: "Hulpmiddelen",
                column: "ErvaringsdeskundigeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
