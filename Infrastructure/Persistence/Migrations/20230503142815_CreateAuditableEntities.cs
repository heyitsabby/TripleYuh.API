using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateAuditableEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "comments");

            migrationBuilder.RenameColumn(
                name: "modified",
                table: "comments",
                newName: "updated");

            migrationBuilder.AddColumn<DateTime>(
                name: "archived",
                table: "posts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "posts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted",
                table: "posts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deleted_by",
                table: "posts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updated_by",
                table: "posts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "archived",
                table: "comments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "comments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted",
                table: "comments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deleted_by",
                table: "comments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updated_by",
                table: "comments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "archived",
                table: "accounts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "accounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted",
                table: "accounts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deleted_by",
                table: "accounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updated_by",
                table: "accounts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "archived",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "deleted_by",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "archived",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "deleted_by",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "archived",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "deleted_by",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "accounts");

            migrationBuilder.RenameColumn(
                name: "updated",
                table: "comments",
                newName: "modified");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "comments",
                type: "text",
                nullable: false,
                defaultValue: "Active");
        }
    }
}
