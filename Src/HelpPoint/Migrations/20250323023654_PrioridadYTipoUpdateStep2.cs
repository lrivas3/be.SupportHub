using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HelpPoint.Migrations;

/// <inheritdoc />
public partial class PrioridadYTipoUpdateStep2 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_SupportRequests_Tipo_TipoId",
            schema: "Support",
            table: "SupportRequests");

        migrationBuilder.DropForeignKey(
            name: "FK_Tickets_Estado_EstadoId",
            schema: "Ticket",
            table: "Tickets");

        migrationBuilder.DropForeignKey(
            name: "FK_Tickets_Tipo_TipoId",
            schema: "Ticket",
            table: "Tickets");

        migrationBuilder.DropTable(
            name: "EstadoSolicitudes",
            schema: "Support");

        migrationBuilder.DropIndex(
            name: "IX_SupportRequests_TipoId",
            schema: "Support",
            table: "SupportRequests");

        migrationBuilder.DropColumn(
            name: "CodigoEstado",
            schema: "Ticket",
            table: "Tickets");

        migrationBuilder.DropColumn(
            name: "TipoId",
            schema: "Support",
            table: "SupportRequests");

        migrationBuilder.RenameTable(
            name: "Tipo",
            schema: "Support",
            newName: "Tipo",
            newSchema: "Ticket");

        migrationBuilder.AddColumn<int>(
            name: "PrioridadId",
            schema: "Ticket",
            table: "Tickets",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            name: "EstadoId",
            schema: "Support",
            table: "SupportRequests",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "Prioridad",
            schema: "Ticket",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Nombre = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Prioridad", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "SupportEstado",
            schema: "Support",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Nombre = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SupportEstado", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Tickets_PrioridadId",
            schema: "Ticket",
            table: "Tickets",
            column: "PrioridadId");

        migrationBuilder.CreateIndex(
            name: "IX_SupportRequests_EstadoId",
            schema: "Support",
            table: "SupportRequests",
            column: "EstadoId");

        migrationBuilder.AddForeignKey(
            name: "FK_SupportRequests_SupportEstado_EstadoId",
            schema: "Support",
            table: "SupportRequests",
            column: "EstadoId",
            principalSchema: "Support",
            principalTable: "SupportEstado",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_Tickets_Estado_EstadoId",
            schema: "Ticket",
            table: "Tickets",
            column: "EstadoId",
            principalSchema: "Ticket",
            principalTable: "Estado",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_Tickets_Prioridad_PrioridadId",
            schema: "Ticket",
            table: "Tickets",
            column: "PrioridadId",
            principalSchema: "Ticket",
            principalTable: "Prioridad",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_Tickets_Tipo_TipoId",
            schema: "Ticket",
            table: "Tickets",
            column: "TipoId",
            principalSchema: "Ticket",
            principalTable: "Tipo",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_SupportRequests_SupportEstado_EstadoId",
            schema: "Support",
            table: "SupportRequests");

        migrationBuilder.DropForeignKey(
            name: "FK_Tickets_Estado_EstadoId",
            schema: "Ticket",
            table: "Tickets");

        migrationBuilder.DropForeignKey(
            name: "FK_Tickets_Prioridad_PrioridadId",
            schema: "Ticket",
            table: "Tickets");

        migrationBuilder.DropForeignKey(
            name: "FK_Tickets_Tipo_TipoId",
            schema: "Ticket",
            table: "Tickets");

        migrationBuilder.DropTable(
            name: "Prioridad",
            schema: "Ticket");

        migrationBuilder.DropTable(
            name: "SupportEstado",
            schema: "Support");

        migrationBuilder.DropIndex(
            name: "IX_Tickets_PrioridadId",
            schema: "Ticket",
            table: "Tickets");

        migrationBuilder.DropIndex(
            name: "IX_SupportRequests_EstadoId",
            schema: "Support",
            table: "SupportRequests");

        migrationBuilder.DropColumn(
            name: "PrioridadId",
            schema: "Ticket",
            table: "Tickets");

        migrationBuilder.DropColumn(
            name: "EstadoId",
            schema: "Support",
            table: "SupportRequests");

        migrationBuilder.RenameTable(
            name: "Tipo",
            schema: "Ticket",
            newName: "Tipo",
            newSchema: "Support");

        migrationBuilder.AddColumn<string>(
            name: "CodigoEstado",
            schema: "Ticket",
            table: "Tickets",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<int>(
            name: "TipoId",
            schema: "Support",
            table: "SupportRequests",
            type: "integer",
            nullable: true);

        migrationBuilder.CreateTable(
            name: "EstadoSolicitudes",
            schema: "Support",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                Codigo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EstadoSolicitudes", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_SupportRequests_TipoId",
            schema: "Support",
            table: "SupportRequests",
            column: "TipoId");

        migrationBuilder.AddForeignKey(
            name: "FK_SupportRequests_Tipo_TipoId",
            schema: "Support",
            table: "SupportRequests",
            column: "TipoId",
            principalSchema: "Support",
            principalTable: "Tipo",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Tickets_Estado_EstadoId",
            schema: "Ticket",
            table: "Tickets",
            column: "EstadoId",
            principalSchema: "Ticket",
            principalTable: "Estado",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Tickets_Tipo_TipoId",
            schema: "Ticket",
            table: "Tickets",
            column: "TipoId",
            principalSchema: "Support",
            principalTable: "Tipo",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
