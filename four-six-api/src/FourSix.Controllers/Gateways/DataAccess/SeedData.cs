using FourSix.Domain.Entities.PedidoAggregate;
using Microsoft.EntityFrameworkCore;

namespace FourSix.Controllers.Gateways.DataAccess
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            #region POPULA StatusPedido

            builder.Entity<StatusPedido>()
                .HasData(
                new
                {
                    Id = EnumStatusPedido.Criado,
                    Descricao = "Criado"
                },
                new
                {
                    Id = EnumStatusPedido.AguardandoPagamento,
                    Descricao = "Aguardando Pagamento"
                },
                new
                {
                    Id = EnumStatusPedido.EmPreparacao,
                    Descricao = "Em Preparação"
                },
                new
                {
                    Id = EnumStatusPedido.Pronto,
                    Descricao = "Pronto"
                },
                new
                {
                    Id = EnumStatusPedido.Finalizado,
                    Descricao = "Finalizado"
                },
                new
                {
                    Id = EnumStatusPedido.Cancelado,
                    Descricao = "Cancelado"
                });

            #endregion

            #region POPULA Pedido
            var dataPedido = DateTime.Now.AddHours(-5);
            builder.Entity<Pedido>()
              .HasData(
               new
               {
                   Id = new Guid("78E3B8D0-BE9A-4407-9304-C61788797808"),
                   NumeroPedido = 1,
                   ClienteId = new Guid("717B2FB9-4BBE-4A8C-8574-7808CD652E0B"),
                   DataPedido = dataPedido,
                   StatusId = EnumStatusPedido.Criado
               });

            #endregion

            #region POPULA PedidoItem

            builder.Entity<PedidoItem>()
              .HasData(
               new
               {
                   PedidoId = new Guid("78E3B8D0-BE9A-4407-9304-C61788797808"),
                   ProdutoId = new Guid("63c776f5-4539-478e-a17a-54d3a1c2d3ee"),
                   ValorUnitario = 5.5m,
                   Quantidade = 2,
                   Observacao = "Sem tomate"
               },
               new
               {
                   PedidoId = new Guid("78E3B8D0-BE9A-4407-9304-C61788797808"),
                   ProdutoId = new Guid("9482fcf0-e9e4-4bdc-869f-ad7d1d15016c"),
                   ValorUnitario = 8.25m,
                   Quantidade = 1
               });

            #endregion

            #region POPULA ProdutoCheckout

            builder.Entity<PedidoCheckout>()
              .HasData(
               new
               {
                   PedidoId = new Guid("78E3B8D0-BE9A-4407-9304-C61788797808"),
                   StatusId = EnumStatusPedido.Criado,
                   DataStatus = dataPedido,
                   Sequencia = 0
               });

            #endregion
        }
    }
}
