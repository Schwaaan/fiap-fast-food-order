using FourSix.Controllers.Adapters.Clientes.NovoCliente;
using FourSix.Controllers.ViewModels;
using FourSix.Domain.Entities.ClienteAggregate;
using FourSix.UseCases.UseCases.Clientes.NovoCliente;
using Moq;

namespace UnitTests.Adapter
{
    public class NovoClienteAdapterTest
    {
        [Fact]
        public async Task Inserir_Deve_RetornarNovoClienteResponse()
        {
            // Arrange
            var mockUseCase = new Mock<INovoClienteUseCase>();

            var cliente = GerarClientes().First();
            var clienteRequest = new NovoClienteRequest
            {
                Cpf=cliente.Cpf,
                Email=cliente.Email,
                NomeCompleto=cliente.Nome
            };

            mockUseCase
                .Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(cliente);

            var adapter = new NovoClienteAdapter(mockUseCase.Object);
            var clienteModel = new ClienteModel(cliente);

            // Act
            var response = await adapter.Inserir(clienteRequest);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<NovoClienteResponse>(response);
            Assert.Equal(clienteModel.Id, response.Cliente.Id);
            Assert.Equal(clienteModel.Cpf, response.Cliente.Cpf);
            Assert.Equal(clienteModel.Nome, response.Cliente.Nome);
            Assert.Equal(clienteModel.Id, response.Cliente.Id);

            mockUseCase.Verify(x => x.Execute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
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
