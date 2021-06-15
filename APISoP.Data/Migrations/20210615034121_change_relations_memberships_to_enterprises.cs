using Microsoft.EntityFrameworkCore.Migrations;

namespace APISoP.Data.Migrations
{
    public partial class change_relations_memberships_to_enterprises : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Enterprises_MembershipId",
                table: "Enterprises");

            migrationBuilder.CreateIndex(
                name: "IX_Enterprises_MembershipId",
                table: "Enterprises",
                column: "MembershipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Enterprises_MembershipId",
                table: "Enterprises");

            migrationBuilder.CreateIndex(
                name: "IX_Enterprises_MembershipId",
                table: "Enterprises",
                column: "MembershipId",
                unique: true);
        }
    }
}
