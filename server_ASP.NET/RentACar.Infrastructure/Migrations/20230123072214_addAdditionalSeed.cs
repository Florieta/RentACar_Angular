using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentACar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addAdditionalSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5af4facac8424694b91c57854ab6b598",
                column: "ConcurrencyStamp",
                value: "fc67cdcc-2057-4ef6-ab00-6f929c086ff0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d86dba5034324ec481562264fecc1d3b",
                column: "ConcurrencyStamp",
                value: "ad3b1b61-0332-4bd5-9a7b-c147ac78f17e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c6e570fd-d889-4a67-a36a-0ecbe758bc2c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb6b564d-fc92-457a-80df-3f5a0a4ded7b", "AQAAAAEAACcQAAAAEFOa5FfnoB+Ab5ZuKF5m0sqP3c/tL2glh1CBQtfHD59rSBk2XeeQHKNo2BgXY324TA==", "f6f03e98-dabf-4b68-bec0-32ecf1d446aa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d3211a8d-efde-4a19-8087-79cde4679276",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "969cf910-954f-48ea-ae83-2b3fe1cd8e56", "AQAAAAEAACcQAAAAEJVdeaZ8LVU4Y0FJMUS0eQwN+CN64zFfI6tNHo0eVoAJXfPDWTFort6wQT0t89pElQ==", "669a49bf-bfcf-41f6-aca6-d8f8a64f6ae8" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fuel",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                column: "Fuel",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "Fuel",
                value: 3);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "IsDeleted" },
                values: new object[,]
                {
                    { 4, "Passenger Van", false },
                    { 5, "Electric", false },
                    { 6, "Intermediate SUV", false },
                    { 7, "Luxury", false }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "IsDeleted", "LocationName" },
                values: new object[,]
                {
                    { 4, "Bulgaria, Sofia, 1000", false, "Sofia Center" },
                    { 5, "Bulgaria, Burgas", false, "Burgas Airport" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5af4facac8424694b91c57854ab6b598",
                column: "ConcurrencyStamp",
                value: "079416ee-a6ce-47b9-a49f-782f23d30a67");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d86dba5034324ec481562264fecc1d3b",
                column: "ConcurrencyStamp",
                value: "02082484-8239-4238-bcee-2c81b12dd3f7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c6e570fd-d889-4a67-a36a-0ecbe758bc2c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93f0489e-f4f9-410b-965f-6e022d075c1b", "AQAAAAEAACcQAAAAEPI2k+aZC7bhBODAIT9lAfcARMsT9lDeoBHCaht/FuCzrNyLxrjK/ZFX4KeKN3g6tQ==", "c49c2c88-5f1e-4157-8516-df734418a59d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d3211a8d-efde-4a19-8087-79cde4679276",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dfeeffd4-9b54-4b6b-950c-bd00523bf44b", "AQAAAAEAACcQAAAAEEoN12KXz6WcxjIypP+ByXycLgBnGBsfBuqdYLp0H+r920008Z3YFLAq+btJQrvc/w==", "9259885f-0265-4709-b58e-37487d440362" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fuel",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                column: "Fuel",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "Fuel",
                value: 4);
        }
    }
}
