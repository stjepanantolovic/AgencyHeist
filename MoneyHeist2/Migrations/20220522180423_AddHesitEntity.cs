using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyHeist2.Migrations
{
    public partial class AddHesitEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Heists",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heists", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HeistSkillLevels",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillLevelID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HeistID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Members = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeistSkillLevels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HeistSkillLevels_Heists_HeistID",
                        column: x => x.HeistID,
                        principalTable: "Heists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeistSkillLevels_SkillLevels_SkillLevelID",
                        column: x => x.SkillLevelID,
                        principalTable: "SkillLevels",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Heists_Name",
                table: "Heists",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HeistSkillLevels_HeistID",
                table: "HeistSkillLevels",
                column: "HeistID");

            migrationBuilder.CreateIndex(
                name: "IX_HeistSkillLevels_SkillLevelID",
                table: "HeistSkillLevels",
                column: "SkillLevelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeistSkillLevels");

            migrationBuilder.DropTable(
                name: "Heists");
        }
    }
}
