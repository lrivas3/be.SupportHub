using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HelpPoint.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    private static readonly string[] columns = ["Id", "Name", "NormalizedName"];

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "Support");

        migrationBuilder.EnsureSchema(
            name: "Ticket");

        migrationBuilder.EnsureSchema(
            name: "Users");

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

        migrationBuilder.CreateTable(
            name: "Menus",
            schema: "Support",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                Url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                Icon = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                ParentId = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: true),
                OrderIndex = table.Column<int>(type: "integer", nullable: false),
                IsActive = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Menus", x => x.Id);
                table.ForeignKey(
                    name: "FK_Menus_Menus_ParentId",
                    column: x => x.ParentId,
                    principalSchema: "Support",
                    principalTable: "Menus",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Roles",
            schema: "Users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                NormalizedName = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Roles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Tags",
            schema: "Ticket",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                Nombre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tags", x => x.Id);
            });

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

        migrationBuilder.CreateTable(
            name: "Unidades",
            schema: "Support",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Nombre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Unidades", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            schema: "Users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                UserName = table.Column<string>(type: "text", nullable: false),
                Email = table.Column<string>(type: "text", nullable: false),
                EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                PhoneNumber = table.Column<string>(type: "text", nullable: false),
                LockOutEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                LockOutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                AccessFailedCount = table.Column<int>(type: "integer", nullable: false),
                PasswordHash = table.Column<string>(type: "text", nullable: false),
                ManagerId = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "RoleMenus",
            schema: "Support",
            columns: table => new
            {
                RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                MenuId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RoleMenus", x => new { x.RoleId, x.MenuId });
                table.ForeignKey(
                    name: "FK_RoleMenus_Menus_MenuId",
                    column: x => x.MenuId,
                    principalSchema: "Support",
                    principalTable: "Menus",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "RoleClaims",
            schema: "Users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                ClaimType = table.Column<string>(type: "text", nullable: false),
                ClaimValue = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RoleClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_RoleClaims_Roles_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "Users",
                    principalTable: "Roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Empleados",
            schema: "Support",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                Nombre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                Estado = table.Column<bool>(type: "boolean", nullable: false),
                UnidadId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Empleados", x => x.Id);
                table.ForeignKey(
                    name: "FK_Empleados_Unidades_UnidadId",
                    column: x => x.UnidadId,
                    principalSchema: "Support",
                    principalTable: "Unidades",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "UserRoles",
            schema: "Users",
            columns: table => new
            {
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                RoleId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    name: "FK_UserRoles_Roles_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "Users",
                    principalTable: "Roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_UserRoles_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "Users",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "SupportRequests",
            schema: "Support",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                Titulo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                Descripcion = table.Column<string>(type: "text", nullable: false),
                Prioridad = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                Tipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                EstadoId = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FechaResolucion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                EmpleadoId = table.Column<Guid>(type: "uuid", nullable: true),
                Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                TokenVerificacion = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SupportRequests", x => x.Id);
                table.ForeignKey(
                    name: "FK_SupportRequests_Empleados_EmpleadoId",
                    column: x => x.EmpleadoId,
                    principalSchema: "Support",
                    principalTable: "Empleados",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.SetNull);
                table.ForeignKey(
                    name: "FK_SupportRequests_EstadoSolicitudes_EstadoId",
                    column: x => x.EstadoId,
                    principalSchema: "Support",
                    principalTable: "EstadoSolicitudes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Tickets",
            schema: "Ticket",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                OrdenEnTablero = table.Column<int>(type: "integer", nullable: true),
                Titulo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                Descripcion = table.Column<string>(type: "text", nullable: true),
                CodigoEstado = table.Column<string>(type: "character varying(10)", nullable: false),
                PrioridadId = table.Column<Guid>(type: "uuid", nullable: false),
                FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FechaCierre = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                SupportRequestId = table.Column<Guid>(type: "uuid", nullable: true),
                CreatedByUserId = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                EstadoId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tickets", x => x.Id);
                table.ForeignKey(
                    name: "FK_Tickets_SupportRequests_SupportRequestId",
                    column: x => x.SupportRequestId,
                    principalSchema: "Support",
                    principalTable: "SupportRequests",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Tickets_TicketEstados_CodigoEstado",
                    column: x => x.CodigoEstado,
                    principalSchema: "Ticket",
                    principalTable: "TicketEstados",
                    principalColumn: "Codigo",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Tickets_TicketPrioridades_PrioridadId",
                    column: x => x.PrioridadId,
                    principalSchema: "Ticket",
                    principalTable: "TicketPrioridades",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Notificaciones",
            schema: "Ticket",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                TicketId = table.Column<Guid>(type: "uuid", nullable: true),
                UserId = table.Column<Guid>(type: "uuid", nullable: true),
                TipoNotificacion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                FechaEnvio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                ExitoEnvio = table.Column<bool>(type: "boolean", nullable: false),
                Mensaje = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Notificaciones", x => x.Id);
                table.ForeignKey(
                    name: "FK_Notificaciones_Tickets_TicketId",
                    column: x => x.TicketId,
                    principalSchema: "Ticket",
                    principalTable: "Tickets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Notificaciones_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "Users",
                    principalTable: "Users",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "TicketAsignaciones",
            schema: "Ticket",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                TicketId = table.Column<Guid>(type: "uuid", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                FechaAsignacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                TiempoEmpleadoMinutos = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TicketAsignaciones", x => x.Id);
                table.ForeignKey(
                    name: "FK_TicketAsignaciones_Tickets_TicketId",
                    column: x => x.TicketId,
                    principalSchema: "Ticket",
                    principalTable: "Tickets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "TicketComentarios",
            schema: "Ticket",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                TicketId = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                Comentario = table.Column<string>(type: "text", nullable: false),
                FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TicketComentarios", x => x.Id);
                table.ForeignKey(
                    name: "FK_TicketComentarios_Tickets_TicketId",
                    column: x => x.TicketId,
                    principalSchema: "Ticket",
                    principalTable: "Tickets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "TicketHistorial",
            schema: "Ticket",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                TicketId = table.Column<Guid>(type: "uuid", nullable: false),
                FechaCambio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CampoModificado = table.Column<string>(type: "text", nullable: true),
                ValorAnterior = table.Column<string>(type: "text", nullable: true),
                ValorNuevo = table.Column<string>(type: "text", nullable: true),
                ChangedByUserId = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TicketHistorial", x => x.Id);
                table.ForeignKey(
                    name: "FK_TicketHistorial_Tickets_TicketId",
                    column: x => x.TicketId,
                    principalSchema: "Ticket",
                    principalTable: "Tickets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "TicketTags",
            schema: "Ticket",
            columns: table => new
            {
                TicketId = table.Column<Guid>(type: "uuid", nullable: false),
                TagId = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TicketTags", x => new { x.TicketId, x.TagId });
                table.ForeignKey(
                    name: "FK_TicketTags_Tags_TagId",
                    column: x => x.TagId,
                    principalSchema: "Ticket",
                    principalTable: "Tags",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_TicketTags_Tickets_TicketId",
                    column: x => x.TicketId,
                    principalSchema: "Ticket",
                    principalTable: "Tickets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            schema: "Users",
            table: "Roles",
            columns: columns,
            values: new object[,]
            {
                { new Guid("01956042-3344-70a8-99d7-7a337595c1ea"), "Admin", "ADMIN" },
                { new Guid("01956042-3344-7953-b575-59d8f088a283"), "AreaManager", "AREAMANAGER" },
                { new Guid("01956042-3344-7a85-8117-020290a145f9"), "SupportStaff", "SUPPORTSTAFF" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Empleados_UnidadId",
            schema: "Support",
            table: "Empleados",
            column: "UnidadId");

        migrationBuilder.CreateIndex(
            name: "IX_Menus_ParentId",
            schema: "Support",
            table: "Menus",
            column: "ParentId");

        migrationBuilder.CreateIndex(
            name: "IX_Notificaciones_TicketId",
            schema: "Ticket",
            table: "Notificaciones",
            column: "TicketId");

        migrationBuilder.CreateIndex(
            name: "IX_Notificaciones_UserId",
            schema: "Ticket",
            table: "Notificaciones",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_RoleClaims_RoleId",
            schema: "Users",
            table: "RoleClaims",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_RoleMenus_MenuId",
            schema: "Support",
            table: "RoleMenus",
            column: "MenuId");

        migrationBuilder.CreateIndex(
            name: "IX_SupportRequests_EmpleadoId",
            schema: "Support",
            table: "SupportRequests",
            column: "EmpleadoId");

        migrationBuilder.CreateIndex(
            name: "IX_SupportRequests_EstadoId",
            schema: "Support",
            table: "SupportRequests",
            column: "EstadoId");

        migrationBuilder.CreateIndex(
            name: "IX_TicketAsignaciones_TicketId",
            schema: "Ticket",
            table: "TicketAsignaciones",
            column: "TicketId");

        migrationBuilder.CreateIndex(
            name: "IX_TicketComentarios_TicketId",
            schema: "Ticket",
            table: "TicketComentarios",
            column: "TicketId");

        migrationBuilder.CreateIndex(
            name: "IX_TicketHistorial_TicketId",
            schema: "Ticket",
            table: "TicketHistorial",
            column: "TicketId");

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
            name: "IX_Tickets_SupportRequestId",
            schema: "Ticket",
            table: "Tickets",
            column: "SupportRequestId");

        migrationBuilder.CreateIndex(
            name: "IX_TicketTags_TagId",
            schema: "Ticket",
            table: "TicketTags",
            column: "TagId");

        migrationBuilder.CreateIndex(
            name: "IX_UserRoles_RoleId",
            schema: "Users",
            table: "UserRoles",
            column: "RoleId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Notificaciones",
            schema: "Ticket");

        migrationBuilder.DropTable(
            name: "RoleClaims",
            schema: "Users");

        migrationBuilder.DropTable(
            name: "RoleMenus",
            schema: "Support");

        migrationBuilder.DropTable(
            name: "TicketAsignaciones",
            schema: "Ticket");

        migrationBuilder.DropTable(
            name: "TicketComentarios",
            schema: "Ticket");

        migrationBuilder.DropTable(
            name: "TicketHistorial",
            schema: "Ticket");

        migrationBuilder.DropTable(
            name: "TicketTags",
            schema: "Ticket");

        migrationBuilder.DropTable(
            name: "UserRoles",
            schema: "Users");

        migrationBuilder.DropTable(
            name: "Menus",
            schema: "Support");

        migrationBuilder.DropTable(
            name: "Tags",
            schema: "Ticket");

        migrationBuilder.DropTable(
            name: "Tickets",
            schema: "Ticket");

        migrationBuilder.DropTable(
            name: "Roles",
            schema: "Users");

        migrationBuilder.DropTable(
            name: "Users",
            schema: "Users");

        migrationBuilder.DropTable(
            name: "SupportRequests",
            schema: "Support");

        migrationBuilder.DropTable(
            name: "TicketEstados",
            schema: "Ticket");

        migrationBuilder.DropTable(
            name: "TicketPrioridades",
            schema: "Ticket");

        migrationBuilder.DropTable(
            name: "Empleados",
            schema: "Support");

        migrationBuilder.DropTable(
            name: "EstadoSolicitudes",
            schema: "Support");

        migrationBuilder.DropTable(
            name: "Unidades",
            schema: "Support");
    }
}
