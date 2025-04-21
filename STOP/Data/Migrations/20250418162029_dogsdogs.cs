using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STOP.Migrations
{
    /// <inheritdoc />
    public partial class dogsdogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Dog",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Dog_OwnerId",
                table: "Dog",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dog_AspNetUsers_OwnerId",
                table: "Dog",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dog_AspNetUsers_OwnerId",
                table: "Dog");

            migrationBuilder.DropIndex(
                name: "IX_Dog_OwnerId",
                table: "Dog");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Dog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
