using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpPoint.Migrations;

/// <inheritdoc />
public partial class addRoleMenuReference : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddForeignKey(
            name: "FK_RoleMenus_Roles_RoleId",
            schema: "Support",
            table: "RoleMenus",
            column: "RoleId",
            principalSchema: "Users",
            principalTable: "Roles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_RoleMenus_Roles_RoleId",
            schema: "Support",
            table: "RoleMenus");
    }
}
