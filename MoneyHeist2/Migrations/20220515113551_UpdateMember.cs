using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyHeist2.Migrations
{
    public partial class UpdateMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MainSkillID",
                table: "Members",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SexID",
                table: "Members",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StatusID",
                table: "Members",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MemberStatus",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.ID);
                });

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
                name: "IX_MemberSkill_SkillsID",
                table: "MemberSkill",
                column: "SkillsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_MemberStatus_StatusID",
                table: "Members",
                column: "StatusID",
                principalTable: "MemberStatus",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Sex_SexID",
                table: "Members",
                column: "SexID",
                principalTable: "Sex",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Skill_MainSkillID",
                table: "Members",
                column: "MainSkillID",
                principalTable: "Skill",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_MemberStatus_StatusID",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Sex_SexID",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Skill_MainSkillID",
                table: "Members");

            migrationBuilder.DropTable(
                name: "MemberSkill");

            migrationBuilder.DropTable(
                name: "MemberStatus");

            migrationBuilder.DropTable(
                name: "Sex");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropIndex(
                name: "IX_Members_MainSkillID",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_SexID",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_StatusID",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MainSkillID",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "SexID",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "StatusID",
                table: "Members");
        }
    }
}
