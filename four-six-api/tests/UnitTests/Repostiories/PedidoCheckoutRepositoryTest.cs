using FourSix.Controllers.Gateways.DataAccess;
using FourSix.Controllers.Gateways.Repositories;
using FourSix.Domain.Entities.PedidoAggregate;
using Microsoft.EntityFrameworkCore;
using Moq;
using UnitTests.ContextTest;

namespace UnitTests.Repostiories
{
    public class PedidoCheckoutRepositoryTest
    {
        private Context _dbContext;
        private PedidoCheckoutRepository _pedidoCheckoutRepository;

        public PedidoCheckoutRepositoryTest()
        {
            _dbContext = TestDatabaseInMemory.GetDatabase();

            // Criar instância do PedidoRepository
            _pedidoCheckoutRepository = new PedidoCheckoutRepository(_dbContext);
        }

        [Fact]
        public void Listar_QuandoSemQuery_RetornarTodosPedidosCheckout()
        {
            // Arrange
            var pedidosCheckout = GerarPedidosCheckout(5).AsQueryable();
            var count = pedidosCheckout.Count();

            var mockSet = new Mock<DbSet<PedidoCheckout>>();
            mockSet.As<IQueryable<PedidoCheckout>>().Setup(m => m.Provider).Returns(pedidosCheckout.Provider);
            mockSet.As<IQueryable<PedidoCheckout>>().Setup(m => m.Expression).Returns(pedidosCheckout.Expression);
            mockSet.As<IQueryable<PedidoCheckout>>().Setup(m => m.ElementType).Returns(pedidosCheckout.ElementType);
            mockSet.As<IQueryable<PedidoCheckout>>().Setup(m => m.GetEnumerator()).Returns(pedidosCheckout.GetEnumerator());

            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.Set<PedidoCheckout>()).Returns(mockSet.Object);

            _pedidoCheckoutRepository = new PedidoCheckoutRepository(mockContext.Object);

            // Act
            var result = _pedidoCheckoutRepository.Listar();

            // Assert
            Assert.True(count >= 5);
            Assert.Equal(count, result.Count());
        }

        [Fact]
        public void Listar_ComQuery_Retornar_Por_Status()
        {
            // Arrange
            var pedidos = GerarPedidosCheckout(5).AsQueryable();
            var pedidoBusca = pedidos.FirstOrDefault();

            var mockSet = new Mock<DbSet<PedidoCheckout>>();
            mockSet.As<IQueryable<PedidoCheckout>>().Setup(m => m.Provider).Returns(pedidos.Provider);
            mockSet.As<IQueryable<PedidoCheckout>>().Setup(m => m.Expression).Returns(pedidos.Expression);
            mockSet.As<IQueryable<PedidoCheckout>>().Setup(m => m.ElementType).Returns(pedidos.ElementType);
            mockSet.As<IQueryable<PedidoCheckout>>().Setup(m => m.GetEnumerator()).Returns(pedidos.GetEnumerator());

            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.Set<PedidoCheckout>()).Returns(mockSet.Object);

            _pedidoCheckoutRepository = new PedidoCheckoutRepository(mockContext.Object);

            // Act
            var result = _pedidoCheckoutRepository.Listar(q => q.StatusId == pedidoBusca.StatusId && q.PedidoId==pedidoBusca.PedidoId);

            // Assert
            Assert.Single(result);
            Assert.Equal(result.First().PedidoId, pedidoBusca.PedidoId);
            Assert.Equal(result.First().Sequencia, pedidoBusca.Sequencia);
            Assert.Equal(result.First().StatusId, pedidoBusca.StatusId);
        }

        [Fact]
        public async Task Deve_Obter_Pedido_Por_Status()
        {
            // Arrange
            var pedidos = GerarPedidosCheckout(1).AsQueryable();
            var pedidoIncluir = pedidos.FirstOrDefault();

            var mockSet = new Mock<DbSet<PedidoCheckout>>();
            mockSet.Setup(m => m.Find(pedidoIncluir.StatusId)).Returns(pedidoIncluir);

            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.Set<PedidoCheckout>()).Returns(mockSet.Object);

            _pedidoCheckoutRepository = new PedidoCheckoutRepository(mockContext.Object);

            // Act
            var result = _pedidoCheckoutRepository.Obter(pedidoIncluir.StatusId);

            // Assert
            Assert.Equal(pedidoIncluir.PedidoId, result.PedidoId);
            Assert.Equal(pedidoIncluir.StatusId, result.StatusId);
        }

        private List<PedidoCheckout> GerarPedidosCheckout(int quantidade)
        {
            var pedidos = new List<Pedido>();
            var checkouts = new List<PedidoCheckout>();
            Random random = new Random();
            var quantidadeMinutos = random.Next(100, 200);
            var dataPedido = DateTime.Now.AddDays(-quantidadeMinutos);
            if (quantidade <= 0) quantidade = 1;

            for (int i = 0; i < quantidade; i++)
            {
                var guidPedido = Guid.NewGuid();
                var itens = new List<PedidoItem>();
                var quantidadeItens = random.Next(1, 4);
                for (int j = 0; j < quantidadeItens; j++)
                {
                    var guidProduto = Guid.NewGuid();
                    var valorItem = (decimal)(random.NextDouble() * (25.0 - 5.0) + 5.0);
                    itens.Add(new PedidoItem(guidPedido, guidProduto, valorItem, 1, null));
                }

                var dataStatus = dataPedido;
                
                var quantidadeCheckout = random.Next(1, 6);
                for (int k = 0; k < quantidadeCheckout; k++)
                {
                    if (k > 0)
                    {
                        var minutosStatus = random.Next(5, 10);
                        dataStatus = dataStatus.AddMinutes(minutosStatus);
                    }
                    checkouts.Add(new PedidoCheckout(guidPedido, k, (EnumStatusPedido)k + 1, dataStatus));
                }

                pedidos.Add(new Pedido(guidPedido, dataPedido, Guid.NewGuid(), itens, checkouts));
            }

            return checkouts;
        }
    }
}
