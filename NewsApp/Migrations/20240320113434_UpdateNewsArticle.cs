using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NewsApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNewsArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2105c835-9788-4387-a18b-2a2dabc0c810");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30b9f610-02ff-4f79-9fdd-8cd9343fb686");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b124ba2-149a-4f30-9f49-ee4851575bf7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7cea7c8f-98c2-4113-92d2-5a47603a3dc0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a43e65f2-7131-4ad2-9371-fd6c20a71a9e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4cc57c6-1855-496b-b7b8-f54957e66752");

            migrationBuilder.AddColumn<string>(
                name: "HeadImagePath",
                table: "NewsArticle",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "02b77e0e-dead-4099-91a4-bfe4a74cfa7d", null, "Owner", "OWNER" },
                    { "1f950fcd-98be-4193-91ab-c6cb60487ffb", null, "Simple", "SIMPLE" },
                    { "55c06146-68c9-404f-bf9a-270974a79b21", null, "ChiefEditor", "CHIEFEDITOR" },
                    { "5774d700-a287-4cd9-a22a-474f916059be", null, "Administrator", "ADMINISTRATOR" },
                    { "99696eff-4bad-4e43-b1e7-de28307ce36d", null, "Editor", "EDITOR" },
                    { "9c22bc75-3c93-4db6-bc1e-f0fb8508735e", null, "Donator", "DONATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02b77e0e-dead-4099-91a4-bfe4a74cfa7d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f950fcd-98be-4193-91ab-c6cb60487ffb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55c06146-68c9-404f-bf9a-270974a79b21");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5774d700-a287-4cd9-a22a-474f916059be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99696eff-4bad-4e43-b1e7-de28307ce36d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c22bc75-3c93-4db6-bc1e-f0fb8508735e");

            migrationBuilder.DropColumn(
                name: "HeadImagePath",
                table: "NewsArticle");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2105c835-9788-4387-a18b-2a2dabc0c810", null, "Administrator", "ADMINISTRATOR" },
                    { "30b9f610-02ff-4f79-9fdd-8cd9343fb686", null, "Simple", "SIMPLE" },
                    { "4b124ba2-149a-4f30-9f49-ee4851575bf7", null, "Owner", "OWNER" },
                    { "7cea7c8f-98c2-4113-92d2-5a47603a3dc0", null, "Editor", "EDITOR" },
                    { "a43e65f2-7131-4ad2-9371-fd6c20a71a9e", null, "Donator", "DONATOR" },
                    { "c4cc57c6-1855-496b-b7b8-f54957e66752", null, "ChiefEditor", "CHIEFEDITOR" }
                });
        }
    }
}
