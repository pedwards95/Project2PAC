using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace _2PAC.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class hangmannamefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 1,
                columns: new[] { "GameDescription", "GameName" },
                values: new object[] { "Test your programming memory skills to fill out the words! Study for a upcoming job interview while testing your friend's game at the same time! See if you can get the high score!", "Hangman" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 1,
                columns: new[] { "GameDescription", "GameName" },
                values: new object[] { "Test your programming memory skills to fill out this board! Study for a upcoming job interview while testing your friend's game at the same time! See if you can getthe high score!", "Memory Bingo" });
        }
    }
}
