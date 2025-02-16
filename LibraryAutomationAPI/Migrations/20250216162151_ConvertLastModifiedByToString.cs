using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAutomationAPI.Migrations
{
    /// <inheritdoc />
    public partial class ConvertLastModifiedByToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_LastModifiedBy",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_LastModifiedBy",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LastModifiedBy",
                table: "Books",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Books_LastModifiedBy",
                table: "Books",
                column: "LastModifiedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_LastModifiedBy",
                table: "Books",
                column: "LastModifiedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
