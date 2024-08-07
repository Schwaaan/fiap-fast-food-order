﻿using FourSix.Controllers.Gateways.Configurations;
using FourSix.Domain.Entities.ClienteAggregate;
using FourSix.Domain.Entities.PedidoAggregate;
using Microsoft.EntityFrameworkCore;

namespace FourSix.Controllers.Gateways.DataAccess
{
    public class Context : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<SolicitacaoLgpd> SolicitacoesLgpd { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<StatusPedido> StatusPedidos { get; set; }
        public DbSet<PedidoCheckout> PedidosCheckouts { get; set; }
        public DbSet<PedidoItem> PedidosItens { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoItemConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoCheckoutConfiguration());
            modelBuilder.ApplyConfiguration(new StatusPedidoConfiguration());
            modelBuilder.ApplyConfiguration(new SolicitacaoLgpdConfiguration());
            SeedData.Seed(modelBuilder);
        }
    }
}
