using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814

namespace WebAPIEcommerce.Migrations
{
    
    public partial class nullShippingAddress : Migration
    {
       
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3aefd94e-89f0-41e7-86d7-6502b194d2ef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bfd4f1f4-4f8f-4405-b32d-9f6a41b777de");

            migrationBuilder.AlterColumn<string>(
                name: "ShippingAddress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5756e69a-0455-4135-8926-c95a9609026f", null, "user", "USER" },
                    { "e54e4427-0c54-4ed7-a87d-3f2dbbc458c1", null, "Admin", "ADMIN" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5756e69a-0455-4135-8926-c95a9609026f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e54e4427-0c54-4ed7-a87d-3f2dbbc458c1");

            migrationBuilder.AlterColumn<string>(
                name: "ShippingAddress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3aefd94e-89f0-41e7-86d7-6502b194d2ef", null, "user", "USER" },
                    { "bfd4f1f4-4f8f-4405-b32d-9f6a41b777de", null, "Admin", "ADMIN" }
                });
        }
    }
}
