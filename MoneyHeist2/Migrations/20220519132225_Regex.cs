using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyHeist2.Migrations
{
    public partial class Regex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberSkillLevel");

            migrationBuilder.DropTable(
                name: "SkillLevels");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Skill_Name",
                table: "Skill");

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Skill",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MemberSkill",
                columns: table => new
                {
                    MembersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberSkill", x => new { x.MembersID, x.SkillsID });
                    table.ForeignKey(
                        name: "FK_MemberSkill_Members_MembersID",
                        column: x => x.MembersID,
                        principalTable: "Members",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberSkill_Skill_SkillsID",
                        column: x => x.SkillsID,
                        principalTable: "Skill",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Name_Level",
                table: "Skill",
                columns: new[] { "Name", "Level" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberSkill_SkillsID",
                table: "MemberSkill",
                column: "SkillsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberSkill");

            migrationBuilder.DropIndex(
                name: "IX_Skill_Name_Level",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Skill");

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SkillLevels",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillLevels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SkillLevels_Levels_LevelID",
                        column: x => x.LevelID,
                        principalTable: "Levels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillLevels_Skill_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skill",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberSkillLevel",
                columns: table => new
                {
                    MembersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillLevelsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberSkillLevel", x => new { x.MembersID, x.SkillLevelsID });
                    table.ForeignKey(
                        name: "FK_MemberSkillLevel_Members_MembersID",
                        column: x => x.MembersID,
                        principalTable: "Members",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberSkillLevel_SkillLevels_SkillLevelsID",
                        column: x => x.SkillLevelsID,
                        principalTable: "SkillLevels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Name",
                table: "Skill",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Levels_Value",
                table: "Levels",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberSkillLevel_SkillLevelsID",
                table: "MemberSkillLevel",
                column: "SkillLevelsID");

            migrationBuilder.CreateIndex(
                name: "IX_SkillLevels_LevelID",
                table: "SkillLevels",
                column: "LevelID");

            migrationBuilder.CreateIndex(
                name: "IX_SkillLevels_SkillID_LevelID",
                table: "SkillLevels",
                columns: new[] { "SkillID", "LevelID" },
                unique: true);
        }
    }
}
