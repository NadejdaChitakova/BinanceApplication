using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BinanceApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumn_EventTime_SymbolsPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EventTime",
                table: "SymbolsPrice",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventTime",
                table: "SymbolsPrice");
        }
    }
}
