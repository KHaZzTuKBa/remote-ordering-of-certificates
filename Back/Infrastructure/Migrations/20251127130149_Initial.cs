using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    FullRequestStatus = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FilePath = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    receivingFormat = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentRequests",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentRequests", x => new { x.StudentId, x.RequestId });
                    table.ForeignKey(
                        name: "FK_StudentRequests_RequestInformation_RequestId",
                        column: x => x.RequestId,
                        principalTable: "RequestInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentRequests_RequestId",
                table: "StudentRequests",
                column: "RequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentRequests");

            migrationBuilder.DropTable(
                name: "RequestInformation");
        }
    }
}
