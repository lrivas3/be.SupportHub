using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HelpPoint.Migrations;

/// <inheritdoc />
public partial class PrioridadYTipoUpdateStep1 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_SupportRequests_EstadoSolicitudes_EstadoId",
            schema: "Support",
            table: "SupportRequests");

        migrationBuilder.DropForeignKey(
            name: "FK_Tickets_TicketEstados_CodigoEstado",
            schema: "Ticket",
            table: "Tickets");

        migrationBuilder.DropForeignKey(
            name: "FK_Tickets_TicketPrioridades_PrioridadId",
            schema: "Ticket",
            table: "Tickets");

        migrationBuilder.DropTable(
            name: "TicketEstados",
            schema: "Ticket");

        migrationBuilder.DropTable(
            name: "TicketPrioridades",
            schema: "Ticket");

        migrationBuilder.DropIndex(
            name: "IX_Tickets_CodigoEstado",
            schema: "Ticket",
            table: "Tickets");

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

        migrationBuilder.DropColumn(
            name: "Prioridad",
            schema: "Support",
            table: "SupportRequests");

        migrationBuilder.DropColumn(
            name: "Tipo",
            schema: "Support",
            table: "SupportRequests");

        migrationBuilder.AlterColumn<string>(
            name: "CodigoEstado",
            schema: "Ticket",
            table: "Tickets",
            type: "text",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(10)");

        migrationBuilder.AddColumn<int>(
            name: "TipoId",
            schema: "Ticket",
            table: "Tickets",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            name: "TipoId",
            schema: "Support",
            table: "SupportRequests",
            type: "integer",
            nullable: true);

        migrationBuilder.CreateTable(
            name: "Estado",
            schema: "Ticket",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                NombreEstado = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                Descripcion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Estado", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Tipo",
            schema: "Support",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Nombre = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tipo", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Tickets_EstadoId",
            schema: "Ticket",
            table: "Tickets",
            column: "EstadoId");

        migrationBuilder.CreateIndex(
            name: "IX_Tickets_TipoId",
            schema: "Ticket",
            table: "Tickets",
            column: "TipoId");

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

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
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
            name: "Estado",
            schema: "Ticket");

        migrationBuilder.DropTable(
            name: "Tipo",
            schema: "Support");

        migrationBuilder.DropIndex(
            name: "IX_Tickets_EstadoId",
            schema: "Ticket",
            table: "Tickets");

        migrationBuilder.DropIndex(
            name: "IX_Tickets_TipoId",
            schema: "Ticket",
            table: "Tickets");

        migrationBuilder.DropIndex(
            name: "IX_SupportRequests_TipoId",
            schema: "Support",
            table: "SupportRequests");

        migrationBuilder.DropColumn(
            name: "TipoId",
            schema: "Ticket",
            table: "Tickets");

        migrationBuilder.DropColumn(
            name: "TipoId",
            schema: "Support",
            table: "SupportRequests");

        migrationBuilder.AlterColumn<string>(
            name: "CodigoEstado",
            schema: "Ticket",
            table: "Tickets",
            type: "character varying(10)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AddColumn<Guid>(
            name: "PrioridadId",
            schema: "Ticket",
            table: "Tickets",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<Guid>(
            name: "EstadoId",
            schema: "Support",
            table: "SupportRequests",
            type: "uuid",
            maxLength: 36,
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<string>(
            name: "Prioridad",
            schema: "Support",
            table: "SupportRequests",
            type: "character varying(10)",
            maxLength: 10,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "Tipo",
            schema: "Support",
            table: "SupportRequests",
            type: "character varying(50)",
            maxLength: 50,
            nullable: false,
            defaultValue: "");

        migrationBuilder.CreateTable(
            name: "TicketEstados",
            schema: "Ticket",
            columns: table => new
            {
                Codigo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                Descripcion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TicketEstados", x => x.Codigo);
            });

        migrationBuilder.CreateTable(
            name: "TicketPrioridades",
            schema: "Ticket",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Codigo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TicketPrioridades", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Tickets_CodigoEstado",
            schema: "Ticket",
            table: "Tickets",
            column: "CodigoEstado");

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
            name: "FK_SupportRequests_EstadoSolicitudes_EstadoId",
            schema: "Support",
            table: "SupportRequests",
            column: "EstadoId",
            principalSchema: "Support",
            principalTable: "EstadoSolicitudes",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_Tickets_TicketEstados_CodigoEstado",
            schema: "Ticket",
            table: "Tickets",
            column: "CodigoEstado",
            principalSchema: "Ticket",
            principalTable: "TicketEstados",
            principalColumn: "Codigo",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_Tickets_TicketPrioridades_PrioridadId",
            schema: "Ticket",
            table: "Tickets",
            column: "PrioridadId",
            principalSchema: "Ticket",
            principalTable: "TicketPrioridades",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }
}
