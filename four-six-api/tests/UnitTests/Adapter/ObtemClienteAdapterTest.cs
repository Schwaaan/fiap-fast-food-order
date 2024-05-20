using FourSix.Controllers.Adapters.Clientes.ObtemCliente;
using FourSix.Controllers.ViewModels;
using FourSix.Domain.Entities.ClienteAggregate;
using FourSix.UseCases.UseCases.Clientes.ObtemCliente;
using Moq;

namespace UnitTests.Adapter
{
    public class ObtemClienteAdapterTest
    {
        [Fact]
        public async Task Listar_Deve_Obter_ClientePorCpf()
        {
            // Arrange
            var mockUseCase = new Mock<IObtemClienteUseCase>();

            var clientes = GerarClientes();
            var cliente = clientes.Where(q => q.Cpf == "55423576016").First();

            mockUseCase
                 .Setup(x => x.Execute(cliente.Cpf))
                 .ReturnsAsync(cliente);

            var adapter = new ObtemClienteAdapter(mockUseCase.Object);
            var clienteModel = clientes.Select(s => new ClienteModel(s)).ToList();

            // Act
            var response = await adapter.Obter(cliente.Cpf);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<ObtemClienteResponse>(response);
            mockUseCase.Verify(x => x.Execute(cliente.Cpf), Times.Once);
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
