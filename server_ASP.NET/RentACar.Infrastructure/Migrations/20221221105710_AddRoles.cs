using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentACar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5af4facac8424694b91c57854ab6b598", "079416ee-a6ce-47b9-a49f-782f23d30a67", "Renter", "RENTER" },
                    { "d86dba5034324ec481562264fecc1d3b", "02082484-8239-4238-bcee-2c81b12dd3f7", "Dealer", "DEALER" }
                });

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

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "5af4facac8424694b91c57854ab6b598", "c6e570fd-d889-4a67-a36a-0ecbe758bc2c" },
                    { "d86dba5034324ec481562264fecc1d3b", "d3211a8d-efde-4a19-8087-79cde4679276" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5af4facac8424694b91c57854ab6b598", "c6e570fd-d889-4a67-a36a-0ecbe758bc2c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d86dba5034324ec481562264fecc1d3b", "d3211a8d-efde-4a19-8087-79cde4679276" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5af4facac8424694b91c57854ab6b598");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d86dba5034324ec481562264fecc1d3b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c6e570fd-d889-4a67-a36a-0ecbe758bc2c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f9d60bd8-593b-45d2-b86b-8ac7823f14dd", "AQAAAAEAACcQAAAAEDUHJXnIGhlpz6dqCpzDPBGr7cjOM/WGa3dxnGTFEYpkSkaUj0o2ouVEFpYvUMxUUw==", "5b446532-6bcd-4bec-b49a-f9ce9f8263ea" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d3211a8d-efde-4a19-8087-79cde4679276",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afbca656-e897-463f-b805-413fac1f7355", "AQAAAAEAACcQAAAAEHMp/IZ9hl8G16ACqOuwCXnQpEcneSSroLx1QYBVFyHBa4/TCxjKUH0LKLUCuX5k2g==", "acb099da-8efa-4072-acc5-8f6e8efd1633" });
        }
    }
}
