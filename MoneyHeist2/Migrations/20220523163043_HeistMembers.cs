using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyHeist2.Migrations
{
    public partial class HeistMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeistMember",
                columns: table => new
                {
                    HeistsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeistMember", x => new { x.HeistsID, x.MembersID });
                    table.ForeignKey(
                        name: "FK_HeistMember_Heists_HeistsID",
                        column: x => x.HeistsID,
                        principalTable: "Heists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeistMember_Members_MembersID",
                        column: x => x.MembersID,
                        principalTable: "Members",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeistMember_MembersID",
                table: "HeistMember",
                column: "MembersID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeistMember");
        }
    }
}
