using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventsDAL.Migrations
{
    /// <inheritdoc />
    public partial class Create_db_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffId);
                    table.ForeignKey(
                        name: "FK_Staffs_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventAllocations",
                columns: table => new
                {
                    EventAllocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventAllocations", x => x.EventAllocationId);
                    table.ForeignKey(
                        name: "FK_EventAllocations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventAllocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventAllocations_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TopicsCovered",
                columns: table => new
                {
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TopicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicsCovered", x => x.TopicId);
                    table.ForeignKey(
                        name: "FK_TopicsCovered_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicsCovered_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicsCovered_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "EventDate", "EventDescription", "EventName" },
                values: new object[,]
                {
                    { new Guid("89b19ae5-39ad-49bd-8753-37d6f1110558"), new DateTime(2024, 6, 27, 14, 43, 29, 960, DateTimeKind.Local).AddTicks(6425), "This is an annual event held by microsoft", "Microsoft Build" },
                    { new Guid("c1161c51-9731-4b24-8188-e9e45e8a7ea2"), new DateTime(2024, 6, 27, 14, 43, 29, 960, DateTimeKind.Local).AddTicks(6410), "This is an annual event held by microsoft", "Ignite" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "LocationName" },
                values: new object[,]
                {
                    { new Guid("43b7455c-0092-4091-931c-8f5fd7135bf3"), "Seatle" },
                    { new Guid("5957c463-248c-456f-89b7-e65bdfe13b96"), "Mumbai" },
                    { new Guid("82ced29a-ec93-4001-92dc-0d11b286f327"), "LosAngels" },
                    { new Guid("ff252c41-8faf-4dac-b6db-9f2d99d314ad"), "Chennai" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventAllocations_EventId",
                table: "EventAllocations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventAllocations_LocationId",
                table: "EventAllocations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EventAllocations_StaffId",
                table: "EventAllocations",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_LocationId",
                table: "Staffs",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicsCovered_EventId",
                table: "TopicsCovered",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicsCovered_LocationId",
                table: "TopicsCovered",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicsCovered_StaffId",
                table: "TopicsCovered",
                column: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventAllocations");

            migrationBuilder.DropTable(
                name: "TopicsCovered");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
