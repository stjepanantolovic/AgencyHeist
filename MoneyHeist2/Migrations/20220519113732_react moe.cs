using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyHeist2.Migrations
{
    public partial class reactmoe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberSkill");

            migrationBuilder.DropTable(
                name: "SkillSkillLevel");

            migrationBuilder.DropIndex(
                name: "IX_SkillLevels_Value",
                table: "SkillLevels");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "SkillLevels");

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "SkillLevels",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<Guid>(
                name: "LevelID",
                table: "SkillLevels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SkillID",
                table: "SkillLevels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Skill",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Sex",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "MemberStatus",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Members",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

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
                name: "IX_SkillLevels_LevelID",
                table: "SkillLevels",
                column: "LevelID");

            migrationBuilder.CreateIndex(
                name: "IX_SkillLevels_SkillID_LevelID",
                table: "SkillLevels",
                columns: new[] { "SkillID", "LevelID" },
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

            migrationBuilder.AddForeignKey(
                name: "FK_SkillLevels_Levels_LevelID",
                table: "SkillLevels",
                column: "LevelID",
                principalTable: "Levels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillLevels_Skill_SkillID",
                table: "SkillLevels",
                column: "SkillID",
                principalTable: "Skill",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillLevels_Levels_LevelID",
                table: "SkillLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillLevels_Skill_SkillID",
                table: "SkillLevels");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "MemberSkillLevel");

            migrationBuilder.DropIndex(
                name: "IX_SkillLevels_LevelID",
                table: "SkillLevels");

            migrationBuilder.DropIndex(
                name: "IX_SkillLevels_SkillID_LevelID",
                table: "SkillLevels");

            migrationBuilder.DropColumn(
                name: "LevelID",
                table: "SkillLevels");

            migrationBuilder.DropColumn(
                name: "SkillID",
                table: "SkillLevels");

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "SkillLevels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "SkillLevels",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Skill",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Sex",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "MemberStatus",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Members",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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
                        name: "FK_SkillSkillLevel_SkillLevels_SkillLevelsID",
                        column: x => x.SkillLevelsID,
                        principalTable: "SkillLevels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkillLevels_Value",
                table: "SkillLevels",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberSkill_SkillsID",
                table: "MemberSkill",
                column: "SkillsID");

            migrationBuilder.CreateIndex(
                name: "IX_SkillSkillLevel_SkillsID",
                table: "SkillSkillLevel",
                column: "SkillsID");
        }
    }
}
