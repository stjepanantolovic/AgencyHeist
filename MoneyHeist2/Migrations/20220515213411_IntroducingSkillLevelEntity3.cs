using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyHeist2.Migrations
{
    public partial class IntroducingSkillLevelEntity3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillSkillLevel_SkillLevel_SkillLevelsID",
                table: "SkillSkillLevel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SkillLevel",
                table: "SkillLevel");

            migrationBuilder.RenameTable(
                name: "SkillLevel",
                newName: "SkillLevels");

            migrationBuilder.RenameIndex(
                name: "IX_SkillLevel_Value",
                table: "SkillLevels",
                newName: "IX_SkillLevels_Value");

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "SkillLevels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkillLevels",
                table: "SkillLevels",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillSkillLevel_SkillLevels_SkillLevelsID",
                table: "SkillSkillLevel",
                column: "SkillLevelsID",
                principalTable: "SkillLevels",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillSkillLevel_SkillLevels_SkillLevelsID",
                table: "SkillSkillLevel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SkillLevels",
                table: "SkillLevels");

            migrationBuilder.RenameTable(
                name: "SkillLevels",
                newName: "SkillLevel");

            migrationBuilder.RenameIndex(
                name: "IX_SkillLevels_Value",
                table: "SkillLevel",
                newName: "IX_SkillLevel_Value");

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "SkillLevel",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkillLevel",
                table: "SkillLevel",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillSkillLevel_SkillLevel_SkillLevelsID",
                table: "SkillSkillLevel",
                column: "SkillLevelsID",
                principalTable: "SkillLevel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
