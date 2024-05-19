using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FourSix.Controllers.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class CreateMicroservice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusPedido",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusPedido", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroPedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedido_StatusPedido_StatusId",
                        column: x => x.StatusId,
                        principalTable: "StatusPedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoCheckout",
                columns: table => new
                {
                    PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sequencia = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<short>(type: "smallint", nullable: false),
                    DataStatus = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoCheckout", x => new { x.PedidoId, x.Sequencia });
                    table.ForeignKey(
                        name: "FK_PedidoCheckout_Pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoCheckout_StatusPedido_StatusId",
                        column: x => x.StatusId,
                        principalTable: "StatusPedido",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PedidoItem",
                columns: table => new
                {
                    PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoItem", x => new { x.PedidoId, x.ProdutoId });
                    table.ForeignKey(
                        name: "FK_PedidoItem_Pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StatusPedido",
                columns: new[] { "Id", "Descricao" },
                values: new object[,]
                {
                    { (short)1, "Criado" },
                    { (short)2, "Aguardando Pagamento" },
                    { (short)3, "Em Preparação" },
                    { (short)4, "Pronto" },
                    { (short)5, "Finalizado" },
                    { (short)6, "Cancelado" }
                });

            migrationBuilder.InsertData(
                table: "Pedido",
                columns: new[] { "Id", "ClienteId", "DataPedido", "NumeroPedido", "StatusId" },
                values: new object[] { new Guid("78e3b8d0-be9a-4407-9304-c61788797808"), new Guid("717b2fb9-4bbe-4a8c-8574-7808cd652e0b"), new DateTime(2024, 5, 13, 17, 9, 20, 269, DateTimeKind.Local).AddTicks(5522), 1, (short)1 });

            migrationBuilder.InsertData(
                table: "PedidoCheckout",
                columns: new[] { "PedidoId", "Sequencia", "DataStatus", "StatusId" },
                values: new object[] { new Guid("78e3b8d0-be9a-4407-9304-c61788797808"), 0, new DateTime(2024, 5, 13, 17, 9, 20, 269, DateTimeKind.Local).AddTicks(5522), (short)1 });

            migrationBuilder.InsertData(
                table: "PedidoItem",
                columns: new[] { "PedidoId", "ProdutoId", "Observacao", "Quantidade", "ValorUnitario" },
                values: new object[,]
                {
                    { new Guid("78e3b8d0-be9a-4407-9304-c61788797808"), new Guid("63c776f5-4539-478e-a17a-54d3a1c2d3ee"), "Sem tomate", 2, 5.5m },
                    { new Guid("78e3b8d0-be9a-4407-9304-c61788797808"), new Guid("9482fcf0-e9e4-4bdc-869f-ad7d1d15016c"), null, 1, 8.25m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_StatusId",
                table: "Pedido",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoCheckout_StatusId",
                table: "PedidoCheckout",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoCheckout");

            migrationBuilder.DropTable(
                name: "PedidoItem");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "StatusPedido");
        }
    }
}
