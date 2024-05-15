using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIEcommerce.Migrations
{
    /// <inheritdoc />
    public partial class editFirstNameInTableUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Products",
                table: "AspNetUsers",
                newName: "FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "Products");
        }
    }
}
