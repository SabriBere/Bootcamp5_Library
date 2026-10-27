using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TheLibrary_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddLibraryRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RolId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LibraryId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Turn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "yearOfBirth",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "RolId",
                table: "Users",
                newName: "LibraryBranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RolId",
                table: "Users",
                newName: "IX_Users_LibraryBranchId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Libraries",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Libraries",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LibraryBranchId",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LibraryId",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LibraryBranches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    LibraryId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibraryBranches_Libraries_LibraryId",
                        column: x => x.LibraryId,
                        principalTable: "Libraries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolUser",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RolUser_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Libraries_UserId",
                table: "Libraries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_LibraryBranchId",
                table: "Books",
                column: "LibraryBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_LibraryId",
                table: "Books",
                column: "LibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryBranches_LibraryId",
                table: "LibraryBranches",
                column: "LibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_RolUser_UsersId",
                table: "RolUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Libraries_LibraryId",
                table: "Books",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_LibraryBranches_LibraryBranchId",
                table: "Books",
                column: "LibraryBranchId",
                principalTable: "LibraryBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Libraries_Users_UserId",
                table: "Libraries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LibraryBranches_LibraryBranchId",
                table: "Users",
                column: "LibraryBranchId",
                principalTable: "LibraryBranches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Libraries_LibraryId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_LibraryBranches_LibraryBranchId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Libraries_Users_UserId",
                table: "Libraries");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_LibraryBranches_LibraryBranchId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "LibraryBranches");

            migrationBuilder.DropTable(
                name: "RolUser");

            migrationBuilder.DropIndex(
                name: "IX_Libraries_UserId",
                table: "Libraries");

            migrationBuilder.DropIndex(
                name: "IX_Books_LibraryBranchId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_LibraryId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Libraries");

            migrationBuilder.DropColumn(
                name: "LibraryBranchId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LibraryId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "LibraryBranchId",
                table: "Users",
                newName: "RolId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_LibraryBranchId",
                table: "Users",
                newName: "IX_Users_RolId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LibraryId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Turn",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "yearOfBirth",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Libraries",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RolId",
                table: "Users",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
