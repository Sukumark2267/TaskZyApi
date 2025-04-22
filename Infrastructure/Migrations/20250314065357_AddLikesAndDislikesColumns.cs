using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLikesAndDislikesColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Work_Categories_CategoryId",
                table: "Work");

            migrationBuilder.DropIndex(
                name: "IX_Work_CategoryId",
                table: "Work");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Work");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "Password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Work",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Work_CategoryId",
                table: "Work",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Work_Categories_CategoryId",
                table: "Work",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");
        }
    }
}
