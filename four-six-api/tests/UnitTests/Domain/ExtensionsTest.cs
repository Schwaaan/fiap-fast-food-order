using FourSix.Domain.Entities.PedidoAggregate;
using FourSix.Domain.ExtensionsMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Domain
{
    public class ExtensionsTest
    {
        [Fact]
        public void Deve_RemoverAcentos()
        {
            var teste = "Amanhã será sábado e é dia de caçar na casa do vovô";

            var acentoRemovido = teste.RemoverAcentos();

            Assert.Equal("Amanha sera sabado e e dia de cacar na casa do vovo", acentoRemovido);
            
        }
    }
}
