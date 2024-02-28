using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupermercadoMDDII.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Idcategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Idcategoria);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Idcliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cedula = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    puntosRecompensa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Idcliente);
                });

            migrationBuilder.CreateTable(
                name: "Configuracion ",
                columns: table => new
                {
                    Idconfiguracion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recurso = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    propiedad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    valor = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuracion ", x => x.Idconfiguracion);
                });

            migrationBuilder.CreateTable(
                name: "Negocio",
                columns: table => new
                {
                    Idnegocio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ruc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    direccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    porcentajeImpuesto = table.Column<decimal>(type: "decimal(8,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Negocio", x => x.Idnegocio);
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    Idproveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ruc = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.Idproveedor);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Idusuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cedula = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sueldo = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    esActivo = table.Column<bool>(type: "bit", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Idusuario);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Idproducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigoBarra = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    marca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Idcategoria = table.Column<int>(type: "int", nullable: false),
                    Idproveedor = table.Column<int>(type: "int", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    esActivo = table.Column<bool>(type: "bit", nullable: false),
                    fechaCaducidad = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Idproducto);
                    table.ForeignKey(
                        name: "FK_Producto_Categoria_Idcategoria",
                        column: x => x.Idcategoria,
                        principalTable: "Categoria",
                        principalColumn: "Idcategoria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Producto_Proveedor_Idproveedor",
                        column: x => x.Idproveedor,
                        principalTable: "Proveedor",
                        principalColumn: "Idproveedor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Asistencia",
                columns: table => new
                {
                    Idasistencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idusuario = table.Column<int>(type: "int", nullable: false),
                    ingreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    salida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    horasExtra = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencia", x => x.Idasistencia);
                    table.ForeignKey(
                        name: "FK_Asistencia_Usuario_Idusuario",
                        column: x => x.Idusuario,
                        principalTable: "Usuario",
                        principalColumn: "Idusuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    Idventa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idusuario = table.Column<int>(type: "int", nullable: false),
                    Idcliente = table.Column<int>(type: "int", nullable: false),
                    subTotal = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    impuestoTotal = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta", x => x.Idventa);
                    table.ForeignKey(
                        name: "FK_Venta_Cliente_Idcliente",
                        column: x => x.Idcliente,
                        principalTable: "Cliente",
                        principalColumn: "Idcliente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Venta_Usuario_Idusuario",
                        column: x => x.Idusuario,
                        principalTable: "Usuario",
                        principalColumn: "Idusuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Idpedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idproducto = table.Column<int>(type: "int", nullable: false),
                    Idproveedor = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    precioU = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    fechaPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Idpedido);
                    table.ForeignKey(
                        name: "FK_Pedido_Producto_Idproducto",
                        column: x => x.Idproducto,
                        principalTable: "Producto",
                        principalColumn: "Idproducto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedido_Proveedor_Idproveedor",
                        column: x => x.Idproveedor,
                        principalTable: "Proveedor",
                        principalColumn: "Idproveedor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalleVenta ",
                columns: table => new
                {
                    IddetalleVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idventa = table.Column<int>(type: "int", nullable: false),
                    Idproducto = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleVenta ", x => x.IddetalleVenta);
                    table.ForeignKey(
                        name: "FK_DetalleVenta _Producto_Idproducto",
                        column: x => x.Idproducto,
                        principalTable: "Producto",
                        principalColumn: "Idproducto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleVenta _Venta_Idventa",
                        column: x => x.Idventa,
                        principalTable: "Venta",
                        principalColumn: "Idventa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EstadoPedido",
                columns: table => new
                {
                    IdestadoPedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idpedido = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    calificiacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoPedido", x => x.IdestadoPedido);
                    table.ForeignKey(
                        name: "FK_EstadoPedido_Pedido_Idpedido",
                        column: x => x.Idpedido,
                        principalTable: "Pedido",
                        principalColumn: "Idpedido",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_Idusuario",
                table: "Asistencia",
                column: "Idusuario");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVenta _Idproducto",
                table: "DetalleVenta ",
                column: "Idproducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVenta _Idventa",
                table: "DetalleVenta ",
                column: "Idventa");

            migrationBuilder.CreateIndex(
                name: "IX_EstadoPedido_Idpedido",
                table: "EstadoPedido",
                column: "Idpedido");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_Idproducto",
                table: "Pedido",
                column: "Idproducto");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_Idproveedor",
                table: "Pedido",
                column: "Idproveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Idcategoria",
                table: "Producto",
                column: "Idcategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Idproveedor",
                table: "Producto",
                column: "Idproveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_Idcliente",
                table: "Venta",
                column: "Idcliente");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_Idusuario",
                table: "Venta",
                column: "Idusuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistencia");

            migrationBuilder.DropTable(
                name: "Configuracion ");

            migrationBuilder.DropTable(
                name: "DetalleVenta ");

            migrationBuilder.DropTable(
                name: "EstadoPedido");

            migrationBuilder.DropTable(
                name: "Negocio");

            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Proveedor");
        }
    }
}
