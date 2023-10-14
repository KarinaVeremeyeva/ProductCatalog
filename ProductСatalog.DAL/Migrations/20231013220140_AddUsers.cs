using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductCatalog.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "056096da-3246-40a7-af6f-726f5f4a74ee", "923594d9-e007-4845-9264-1a51a7c7c93c", "AdvancedUser", "ADVANCEDUSER" },
                    { "beea0094-0cde-4f04-812b-98c02f4f8e27", "c1a1ec7e-25fa-4ff3-92f9-88ecf33fb4ba", "User", "USER" },
                    { "e1110812-5f76-4889-93a7-b4c677c2d8dd", "ccf82068-a98c-4744-82a5-2bedcbb7f565", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2288a9b4-35ee-4c13-b863-36184e701e0a", 0, "a795af17-0139-4f9d-9221-736ab3ebe038", "admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEKD4WQNvNmjHS8gpZxvAvwue4W2cRYBPCO2tgz6PrrUDRTT4kaK5oW6sl0V5waAsdA==", null, false, "e9e4b045-8fff-4e03-9c27-cbbceb84128b", false, "admin@gmail.com" },
                    { "3758bb9b-3ad6-457c-9ff5-c88f338c7d48", 0, "4ee6561b-eeac-445c-9a05-c74775edb0ec", "user2@gmail.com", false, false, null, "USER2@GMAIL.COM", "USER2@GMAIL.COM", "AQAAAAEAACcQAAAAEHoRuKNEHKSUqRV3aF2VhptvuA3jXpHKYhKjrejWWLPFg2ZD3e1JyMB+GymZKzHdPg==", null, false, "fc0a4799-29c9-4165-a5f2-cfa21d3d65e7", false, "user2@gmail.com" },
                    { "5eb5d348-b220-4148-8f9b-570de6262aa8", 0, "6367f8f4-da04-4d52-ac98-7dc778f60540", "user1@gmail.com", false, false, null, "USER1@GMAIL.COM", "USER1@GMAIL.COM", "AQAAAAEAACcQAAAAEJDJjnV5zTRm2uyk3zsX2i9nxxYvRzNaZGB9ggOvFlEgg5ZHhBpoZ4EeKXJyiDPJrQ==", null, false, "19cf6ac8-5af2-4137-81d5-1fd787c97999", false, "user1@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "e1110812-5f76-4889-93a7-b4c677c2d8dd", "2288a9b4-35ee-4c13-b863-36184e701e0a" },
                    { "056096da-3246-40a7-af6f-726f5f4a74ee", "3758bb9b-3ad6-457c-9ff5-c88f338c7d48" },
                    { "beea0094-0cde-4f04-812b-98c02f4f8e27", "5eb5d348-b220-4148-8f9b-570de6262aa8" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e1110812-5f76-4889-93a7-b4c677c2d8dd", "2288a9b4-35ee-4c13-b863-36184e701e0a" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "056096da-3246-40a7-af6f-726f5f4a74ee", "3758bb9b-3ad6-457c-9ff5-c88f338c7d48" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "beea0094-0cde-4f04-812b-98c02f4f8e27", "5eb5d348-b220-4148-8f9b-570de6262aa8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "056096da-3246-40a7-af6f-726f5f4a74ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "beea0094-0cde-4f04-812b-98c02f4f8e27");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1110812-5f76-4889-93a7-b4c677c2d8dd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2288a9b4-35ee-4c13-b863-36184e701e0a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3758bb9b-3ad6-457c-9ff5-c88f338c7d48");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5eb5d348-b220-4148-8f9b-570de6262aa8");
        }
    }
}
