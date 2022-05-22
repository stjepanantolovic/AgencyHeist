using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyHeist2.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "MemberStatus",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sex",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sex", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MainSkillID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SexID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Members_MemberStatus_StatusID",
                        column: x => x.StatusID,
                        principalTable: "MemberStatus",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Members_Sex_SexID",
                        column: x => x.SexID,
                        principalTable: "Sex",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Members_Skill_MainSkillID",
                        column: x => x.MainSkillID,
                        principalTable: "Skill",
                        principalColumn: "ID");
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
                name: "IX_Levels_Value",
                table: "Levels",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_Email",
                table: "Members",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_MainSkillID",
                table: "Members",
                column: "MainSkillID");

            migrationBuilder.CreateIndex(
                name: "IX_Members_SexID",
                table: "Members",
                column: "SexID");

            migrationBuilder.CreateIndex(
                name: "IX_Members_StatusID",
                table: "Members",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberSkillLevel_SkillLevelsID",
                table: "MemberSkillLevel",
                column: "SkillLevelsID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberStatus_Name",
                table: "MemberStatus",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sex_Name",
                table: "Sex",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Name",
                table: "Skill",
                column: "Name",
                unique: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberSkillLevel");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "SkillLevels");

            migrationBuilder.DropTable(
                name: "MemberStatus");

            migrationBuilder.DropTable(
                name: "Sex");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "Skill");
        }
    }
}
