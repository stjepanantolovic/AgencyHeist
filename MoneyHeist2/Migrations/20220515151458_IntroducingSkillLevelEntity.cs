using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyHeist2.Migrations
{
    public partial class IntroducingSkillLevelEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Skill_Name",
                table: "Skill");

            migrationBuilder.DropIndex(
                name: "IX_MemberStatus_Name",
                table: "MemberStatus");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Skill");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Skill",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MemberStatus",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Members",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SkillLevel",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillLevel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SkillSkillLevel",
                columns: table => new
                {
                    SkillLevelsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillSkillLevel", x => new { x.SkillLevelsID, x.SkillsID });
                    table.ForeignKey(
                        name: "FK_SkillSkillLevel_Skill_SkillsID",
                        column: x => x.SkillsID,
                        principalTable: "Skill",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillSkillLevel_SkillLevel_SkillLevelsID",
                        column: x => x.SkillLevelsID,
                        principalTable: "SkillLevel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Name",
                table: "Skill",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberStatus_Name",
                table: "MemberStatus",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_Email",
                table: "Members",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SkillLevel_Value",
                table: "SkillLevel",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SkillSkillLevel_SkillsID",
                table: "SkillSkillLevel",
                column: "SkillsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkillSkillLevel");

            migrationBuilder.DropTable(
                name: "SkillLevel");

            migrationBuilder.DropIndex(
                name: "IX_Skill_Name",
                table: "Skill");

            migrationBuilder.DropIndex(
                name: "IX_MemberStatus_Name",
                table: "MemberStatus");

            migrationBuilder.DropIndex(
                name: "IX_Members_Email",
                table: "Members");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Skill",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Skill",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MemberStatus",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Name",
                table: "Skill",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MemberStatus_Name",
                table: "MemberStatus",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }
    }
}
