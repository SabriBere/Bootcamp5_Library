using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheLibrary_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class FixUserLibraryRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libraries_Users_UserId",
                table: "Libraries");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryBranches_Libraries_LibraryId",
                table: "LibraryBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_LibraryBranches_LibraryBranchId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "RolUser");

            migrationBuilder.DropIndex(
                name: "IX_Users_LibraryBranchId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Libraries_UserId",
                table: "Libraries");

            migrationBuilder.DropColumn(
                name: "LibraryBranchId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Libraries");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkShift",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LibraryId",
                table: "LibraryBranches",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "LibraryUser",
                columns: table => new
                {
                    LibrariesId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryUser", x => new { x.LibrariesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_LibraryUser_Libraries_LibrariesId",
                        column: x => x.LibrariesId,
                        principalTable: "Libraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibraryUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LibraryUser_UsersId",
                table: "LibraryUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryBranches_Libraries_LibraryId",
                table: "LibraryBranches",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryBranches_Libraries_LibraryId",
                table: "LibraryBranches");

            migrationBuilder.DropTable(
                name: "LibraryUser");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WorkShift",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "LibraryBranchId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LibraryId",
                table: "LibraryBranches",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Libraries",
                type: "integer",
                nullable: true);

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
                name: "IX_Users_LibraryBranchId",
                table: "Users",
                column: "LibraryBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Libraries_UserId",
                table: "Libraries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RolUser_UsersId",
                table: "RolUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Libraries_Users_UserId",
                table: "Libraries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryBranches_Libraries_LibraryId",
                table: "LibraryBranches",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LibraryBranches_LibraryBranchId",
                table: "Users",
                column: "LibraryBranchId",
                principalTable: "LibraryBranches",
                principalColumn: "Id");
        }
    }
}
