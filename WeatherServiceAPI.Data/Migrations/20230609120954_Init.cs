using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherServiceAPI.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    IpAddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Addresses_IpAddressId",
                        column: x => x.IpAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    Conditions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WindSpeed = table.Column<double>(type: "float", nullable: false),
                    WindDirection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    LocationDataId = table.Column<int>(type: "int", nullable: false),
                    IpAddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weather_Addresses_IpAddressId",
                        column: x => x.IpAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Weather_Locations_LocationDataId",
                        column: x => x.LocationDataId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_IpAddressId",
                table: "Locations",
                column: "IpAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Weather_IpAddressId",
                table: "Weather",
                column: "IpAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Weather_LocationDataId",
                table: "Weather",
                column: "LocationDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weather");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
