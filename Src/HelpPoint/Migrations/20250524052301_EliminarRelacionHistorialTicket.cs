using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpPoint.Migrations;

/// <inheritdoc />
public partial class EliminarRelacionHistorialTicket : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_TicketHistorial_Tickets_TicketId",
            schema: "Ticket",
            table: "TicketHistorial");

        migrationBuilder.DropIndex(
            name: "IX_TicketHistorial_TicketId",
            schema: "Ticket",
            table: "TicketHistorial");

        migrationBuilder.CreateIndex(
            name: "IX_TicketComentarios_UserId",
            schema: "Ticket",
            table: "TicketComentarios",
            column: "UserId");

        migrationBuilder.AddForeignKey(
            name: "FK_TicketComentarios_Users_UserId",
            schema: "Ticket",
            table: "TicketComentarios",
            column: "UserId",
            principalSchema: "Users",
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_TicketComentarios_Users_UserId",
            schema: "Ticket",
            table: "TicketComentarios");

        migrationBuilder.DropIndex(
            name: "IX_TicketComentarios_UserId",
            schema: "Ticket",
            table: "TicketComentarios");

        migrationBuilder.CreateIndex(
            name: "IX_TicketHistorial_TicketId",
            schema: "Ticket",
            table: "TicketHistorial",
            column: "TicketId");

        migrationBuilder.AddForeignKey(
            name: "FK_TicketHistorial_Tickets_TicketId",
            schema: "Ticket",
            table: "TicketHistorial",
            column: "TicketId",
            principalSchema: "Ticket",
            principalTable: "Tickets",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
