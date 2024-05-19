using FourSix.Controllers.Gateways.DataAccess;
using FourSix.Controllers.Gateways.Repositories;
using FourSix.Domain.Entities.PedidoAggregate;
using Microsoft.EntityFrameworkCore;
using Moq;
using UnitTests.ContextTest;

namespace UnitTests.Repostiories
{
    public class PedidoRepositoryTest
    {
        private Context _dbContext;
        private PedidoRepository _pedidoRepository;

        public PedidoRepositoryTest()
        {
            _dbContext = TestDatabaseInMemory.GetDatabase();

            // Criar instância do PedidoRepository
            _pedidoRepository = new PedidoRepository(_dbContext);
        }

        [Fact]
        public void Listar_QuandoSemQuery_RetornarTodosPedidos()
        {
            // Arrange
            var pedidos = GerarPedidos(5).AsQueryable();

            var mockSet = new Mock<DbSet<Pedido>>();
            mockSet.As<IQueryable<Pedido>>().Setup(m => m.Provider).Returns(pedidos.Provider);
            mockSet.As<IQueryable<Pedido>>().Setup(m => m.Expression).Returns(pedidos.Expression);
            mockSet.As<IQueryable<Pedido>>().Setup(m => m.ElementType).Returns(pedidos.ElementType);
            mockSet.As<IQueryable<Pedido>>().Setup(m => m.GetEnumerator()).Returns(pedidos.GetEnumerator());

            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.Set<Pedido>()).Returns(mockSet.Object);

            _pedidoRepository = new PedidoRepository(mockContext.Object);

            // Act
            var result = _pedidoRepository.Listar();

            // Assert
            Assert.Equal(5, result.Count());
        }

        [Fact]
        public void Listar_ComQuery_Retornar_Por_Id()
        {
            // Arrange
            var pedidos = GerarPedidos(5).AsQueryable();
            var pedidoBusca = pedidos.FirstOrDefault();

            var mockSet = new Mock<DbSet<Pedido>>();
            mockSet.As<IQueryable<Pedido>>().Setup(m => m.Provider).Returns(pedidos.Provider);
            mockSet.As<IQueryable<Pedido>>().Setup(m => m.Expression).Returns(pedidos.Expression);
            mockSet.As<IQueryable<Pedido>>().Setup(m => m.ElementType).Returns(pedidos.ElementType);
            mockSet.As<IQueryable<Pedido>>().Setup(m => m.GetEnumerator()).Returns(pedidos.GetEnumerator());

            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.Set<Pedido>()).Returns(mockSet.Object);

            _pedidoRepository = new PedidoRepository(mockContext.Object);

            // Act
            var result = _pedidoRepository.Listar(q=>q.Id==pedidoBusca.Id);

            // Assert
            Assert.Single(result);
            Assert.Equal(result.First().Id, pedidoBusca.Id);
            Assert.Equal(result.First().DataPedido, pedidoBusca.DataPedido);
            Assert.Equal(result.First().ValorTotal, pedidoBusca.ValorTotal);
            Assert.Equal(result.First().NumeroPedido, pedidoBusca.NumeroPedido);
            Assert.Equal(result.First().Status, pedidoBusca.Status);
        }

        [Fact]
        public async Task Deve_Incluir_Novo_Pedido()
        {
            // Arrange
            var pedidos = GerarPedidos(1).AsQueryable();
            var pedidoIncluir= pedidos.FirstOrDefault();

            var mockSet = new Mock<DbSet<Pedido>>();

            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.Set<Pedido>()).Returns(mockSet.Object);

            _pedidoRepository = new PedidoRepository(mockContext.Object);

            // Act
            await _pedidoRepository.Incluir(pedidoIncluir);

            // Assert
            mockSet.Verify(m => m.AddAsync(It.IsAny<Pedido>(), CancellationToken.None), Times.Once);
        }
        

        [Fact]
        public async Task Deve_Obter_Pedido_Por_Id()
        {
            // Arrange
            var pedidos = GerarPedidos(1).AsQueryable();
            var pedidoIncluir = pedidos.FirstOrDefault();

            var mockSet = new Mock<DbSet<Pedido>>();
            mockSet.Setup(m=>m.Find(pedidoIncluir.Id)).Returns(pedidoIncluir);

            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.Set<Pedido>()).Returns(mockSet.Object);

            _pedidoRepository = new PedidoRepository(mockContext.Object);

            // Act
            var result = _pedidoRepository.Obter(pedidoIncluir.Id);

            // Assert
            Assert.Equal(pedidoIncluir.Id, result.Id);
        }

        [Fact]
        public async Task Deve_Alterar_Estado_Para_Modified()
        {
            // Arrange
            var pedidos = GerarPedidos(1).AsQueryable();
            Pedido pedidoIncluir = pedidos.First();

            _dbContext = TestDatabaseInMemory.GetDatabase();
            _pedidoRepository = new PedidoRepository(_dbContext);

            // Act
            await _pedidoRepository.Alterar(pedidoIncluir);

            // Assert
            Assert.Equal(EntityState.Modified, _dbContext.Entry(pedidoIncluir).State);
        }

        

        [Fact]
        public async Task Deve_Excluir_Pedido()
        {
            // Arrange
            var pedidos = GerarPedidos(1).AsQueryable();
            Pedido pedidoIncluir = pedidos.First();

            _dbContext = TestDatabaseInMemory.GetDatabase();
            _pedidoRepository = new PedidoRepository(_dbContext);

            _pedidoRepository.Incluir(pedidoIncluir);
            var _unitOfWork = new UnitOfWork(_dbContext);
            await _unitOfWork.Save();

            // Act
            await _pedidoRepository.Excluir(pedidoIncluir.Id);

            // Assert
            Assert.Equal(EntityState.Deleted, _dbContext.Entry(pedidoIncluir).State);
        }

        private List<Pedido> GerarPedidos(int quantidade)
        {
            var pedidos = new List<Pedido>();
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
                var checkouts = new List<PedidoCheckout>();
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

            return pedidos;
        }
    }
}
