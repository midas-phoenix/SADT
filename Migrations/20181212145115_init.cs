using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SA_TEST.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlateNumber",
                columns: table => new
                {
                    PlateNumberID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    platenumber = table.Column<string>(nullable: false),
                    LGACode = table.Column<string>(nullable: true),
                    platenumberint = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Owner = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlateNumber", x => x.PlateNumberID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlateNumber");
        }
    }
}
