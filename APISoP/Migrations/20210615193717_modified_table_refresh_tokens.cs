using Microsoft.EntityFrameworkCore.Migrations;

namespace APISoP.Migrations
{
    public partial class modified_table_refresh_tokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "RefreshTokens",
                newName: "ExpireDate");

            migrationBuilder.AddColumn<string>(
                name: "EnterpriseId",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreId",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnterpriseId",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "ExpireDate",
                table: "RefreshTokens",
                newName: "ExpiryDate");
        }
    }
}
