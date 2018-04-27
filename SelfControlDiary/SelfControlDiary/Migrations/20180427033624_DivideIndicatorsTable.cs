using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SelfControlDiary.Migrations
{
    public partial class DivideIndicatorsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bending",
                table: "IndicatorLists");

            migrationBuilder.DropColumn(
                name: "Incline",
                table: "IndicatorLists");

            migrationBuilder.DropColumn(
                name: "Press",
                table: "IndicatorLists");

            migrationBuilder.DropColumn(
                name: "Pulling",
                table: "IndicatorLists");

            migrationBuilder.DropColumn(
                name: "Run",
                table: "IndicatorLists");

            migrationBuilder.DropColumn(
                name: "Squatting",
                table: "IndicatorLists");

            migrationBuilder.CreateTable(
                name: "StandardControls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bending = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Incline = table.Column<int>(nullable: false),
                    Press = table.Column<int>(nullable: true),
                    Pulling = table.Column<int>(nullable: true),
                    Run = table.Column<int>(nullable: false),
                    Semestr = table.Column<int>(nullable: false),
                    Squatting = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardControls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandardControls_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StandardControls_StudentId",
                table: "StandardControls",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StandardControls");

            migrationBuilder.AddColumn<int>(
                name: "Bending",
                table: "IndicatorLists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Incline",
                table: "IndicatorLists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Press",
                table: "IndicatorLists",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pulling",
                table: "IndicatorLists",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Run",
                table: "IndicatorLists",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Squatting",
                table: "IndicatorLists",
                nullable: false,
                defaultValue: 0);
        }
    }
}
