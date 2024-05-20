using FourSix.Controllers.Gateways.DataAccess;
using FourSix.Controllers.Gateways.Repositories;
using FourSix.Domain.Entities.ClienteAggregate;
using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.UseCases.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.ContextTest;

namespace UnitTests.Repostiories
{
    public class ClienteRepositoryTest
    {
        private Context _dbContext;
        private ClienteRepository _clienteRepository;

        public ClienteRepositoryTest()
        {
            _dbContext = TestDatabaseInMemory.GetDatabase();

            // Criar instância do PedidoRepository
            _clienteRepository = new ClienteRepository(_dbContext);
        }

        [Fact]
        public void Listar_QuandoSemQuery_RetornarTodosClientes()
        {
            // Arrange
            var clientes = GerarClientes().AsQueryable();
            var count = clientes.Count();

            var mockSet = new Mock<DbSet<Cliente>>();
            mockSet.As<IQueryable<Cliente>>().Setup(m => m.Provider).Returns(clientes.Provider);
            mockSet.As<IQueryable<Cliente>>().Setup(m => m.Expression).Returns(clientes.Expression);
            mockSet.As<IQueryable<Cliente>>().Setup(m => m.ElementType).Returns(clientes.ElementType);
            mockSet.As<IQueryable<Cliente>>().Setup(m => m.GetEnumerator()).Returns(clientes.GetEnumerator());

            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.Set<Cliente>()).Returns(mockSet.Object);

            _clienteRepository = new ClienteRepository(mockContext.Object);

            // Act
            var result = _clienteRepository.Listar();

            // Assert
            Assert.True(count >= 4);
            Assert.Equal(count, result.Count());
        }

        [Fact]
        public async Task Deve_Incluir_Novo_Cliente()
        {
            // Arrange
            var clientes = GerarClientes().AsQueryable();
            var clienteIncluir = clientes.FirstOrDefault();

            var mockSet = new Mock<DbSet<Cliente>>();

            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.Set<Cliente>()).Returns(mockSet.Object);

            _clienteRepository = new ClienteRepository(mockContext.Object);

            // Act
            await _clienteRepository.Incluir(clienteIncluir);

            // Assert
            mockSet.Verify(m => m.AddAsync(It.IsAny<Cliente>(), CancellationToken.None), Times.Once);
        }

        private List<Cliente> GerarClientes()
        {
            var clientes = new List<Cliente>
            {
                new Cliente(Guid.NewGuid(), "55423576016", "João da Silva", "joa@gmail.com"),
                new Cliente(Guid.NewGuid(), "98752548007", "Maria Gomes Ferreira", "maria@gmail.com"),
                new Cliente(Guid.NewGuid(), "48177607022", "Antonio da Silva", "a.silva@gmail.com"),
                new Cliente(Guid.NewGuid(), "23726129057", "Ana Paula Almeida", "a.p.alm@gmail.com")
            };

            return clientes;
        }
    }
}
