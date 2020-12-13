using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiPSCourse.Migrations
{
    public partial class InitialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueName = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    Address3 = table.Column<string>(nullable: true),
                    CityTown = table.Column<string>(nullable: true),
                    StateProvince = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Speakers",
                columns: table => new
                {
                    SpeakerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    CompanyUrl = table.Column<string>(nullable: true),
                    BlogUrl = table.Column<string>(nullable: true),
                    Twitter = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.SpeakerId);
                });

            migrationBuilder.CreateTable(
                name: "Camps",
                columns: table => new
                {
                    CampId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Moniker = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: true),
                    EventDate = table.Column<DateTime>(nullable: false),
                    Length = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camps", x => x.CampId);
                    table.ForeignKey(
                        name: "FK_Camps_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Talks",
                columns: table => new
                {
                    TalkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Abstract = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    SpeakerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talks", x => x.TalkId);
                    table.ForeignKey(
                        name: "FK_Talks_Camps_CampId",
                        column: x => x.CampId,
                        principalTable: "Camps",
                        principalColumn: "CampId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Talks_Speakers_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "Speakers",
                        principalColumn: "SpeakerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "LocationId", "Address1", "Address2", "Address3", "CityTown", "Country", "PostalCode", "StateProvince", "VenueName" },
                values: new object[] { 1, "123 Ikorodu Road", null, null, "Ikorodu", "Nigeria", "123123", "LA", "Lagos Conservation Centre" });

            migrationBuilder.InsertData(
                table: "Speakers",
                columns: new[] { "SpeakerId", "BlogUrl", "Company", "CompanyUrl", "FirstName", "LastName", "MiddleName", "Twitter" },
                values: new object[] { 1, "https://github.com/buka4rill", "Buka Investments", "http://bukainvestments.com", "Ebuka", "Abraham", null, "buka4rill" });

            migrationBuilder.InsertData(
                table: "Speakers",
                columns: new[] { "SpeakerId", "BlogUrl", "Company", "CompanyUrl", "FirstName", "LastName", "MiddleName", "Twitter" },
                values: new object[] { 2, "https://adalovelace.com", "Women in Tech LLC", "http://womenintech.in", "Ada", "Lovelace", null, "adalovelace" });

            migrationBuilder.InsertData(
                table: "Camps",
                columns: new[] { "CampId", "EventDate", "Length", "LocationId", "Moniker", "Name" },
                values: new object[] { 1, new DateTime(2019, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "LAG2019", "Made in Lagos Camp" });

            migrationBuilder.InsertData(
                table: "Talks",
                columns: new[] { "TalkId", "Abstract", "CampId", "Level", "SpeakerId", "Title" },
                values: new object[] { 1, "Thinking of good sample data example is tiring.", 1, 200, 1, "Sample Data Made Easy" });

            migrationBuilder.InsertData(
                table: "Talks",
                columns: new[] { "TalkId", "Abstract", "CampId", "Level", "SpeakerId", "Title" },
                values: new object[] { 2, "Entity Framework from scratch in an hour.", 1, 100, 2, "Entity Framework from Scratch" });

            migrationBuilder.CreateIndex(
                name: "IX_Camps_LocationId",
                table: "Camps",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Talks_CampId",
                table: "Talks",
                column: "CampId");

            migrationBuilder.CreateIndex(
                name: "IX_Talks_SpeakerId",
                table: "Talks",
                column: "SpeakerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Talks");

            migrationBuilder.DropTable(
                name: "Camps");

            migrationBuilder.DropTable(
                name: "Speakers");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
