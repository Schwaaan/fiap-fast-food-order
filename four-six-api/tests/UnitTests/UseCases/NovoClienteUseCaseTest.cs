using FourSix.Domain.Entities.ClienteAggregate;
using FourSix.UseCases.Interfaces;
using FourSix.UseCases.UseCases.Clientes.NovoCliente;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.UseCases
{
    public class NovoClienteUseCaseTest
    {
        [Fact]
        public async Task Deve_Inserir_NovoCliente()
        {
            // Arrange
            var mockRepository = new Mock<IClienteRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockRepository.Setup(repo => repo.Incluir(It.IsAny<Cliente>())).Returns(Task.CompletedTask);
            NovoClienteUseCase useCase = new NovoClienteUseCase(mockRepository.Object, mockUnitOfWork.Object);

            var cliente = GerarCliente();


            // Act
            var clienteIncluido = await useCase.Execute(cliente.Cpf, cliente.Nome, cliente.Email);

            // Assert
            Assert.IsType<Cliente>(clienteIncluido);
            mockRepository.Verify(repo => repo.Incluir(It.IsAny<Cliente>()), Times.Once);
            mockUnitOfWork.Verify(unit => unit.Save(), Times.Once);
        }

        [Fact]
        public async void Deve_RetornarErro_Inserir_ClienteExistente()
        {
            //Arrange
            var mockRepository = new Mock<IClienteRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var cliente = GerarCliente();

            mockRepository.Setup(repo => repo.Incluir(It.IsAny<Cliente>())).Returns(Task.CompletedTask);
            mockRepository.Setup(repo => repo.Listar(It.IsAny<Expression<Func<Cliente, bool>>>())).Returns((Expression<Func<Cliente, bool>> predicate) => new List<Cliente> { cliente }.AsQueryable().Where(predicate).ToList());
            NovoClienteUseCase useCase = new NovoClienteUseCase(mockRepository.Object, mockUnitOfWork.Object);


            //Act
            Func<Task> act = () => useCase.Execute(cliente.Cpf, cliente.Nome, cliente.Email);

            //& Assert
            await Assert.ThrowsAsync<Exception>(act);
            mockRepository.Verify(repo => repo.Incluir(It.IsAny<Cliente>()), Times.Never);
            mockUnitOfWork.Verify(unit => unit.Save(), Times.Never);
        }

        private Cliente GerarCliente()
        {
            return new Cliente(Guid.NewGuid(), "55423576016", "João da Silva", "joa@gmail.com");
        }
    }
}