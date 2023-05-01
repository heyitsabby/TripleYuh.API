using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddParentComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "parent_id",
                table: "comments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_comments_parent_id",
                table: "comments",
                column: "parent_id");

            migrationBuilder.AddForeignKey(
                name: "fk_comments_comments_parent_id",
                table: "comments",
                column: "parent_id",
                principalTable: "comments",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_comments_comments_parent_id",
                table: "comments");

            migrationBuilder.DropIndex(
                name: "ix_comments_parent_id",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "parent_id",
                table: "comments");
        }
    }
}
