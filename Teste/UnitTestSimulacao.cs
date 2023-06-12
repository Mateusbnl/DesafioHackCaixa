using Microsoft.Extensions.Options;
using MinhaApi.Models;

namespace DesafioHackCaixa_Teste
{
    [TestClass]
    public class SimulacaoTest
    {
        [TestMethod]
        public void TestaPrestacaoSAC()
        {
            //Arrange
            decimal valor = 900.00M;
            int prazo = 5;
            decimal tx_juros = 0.0179M;
            decimal valorPrestacaoEsperada = 196.11M;
            ModalidadeSAC modalidadeSAC = new ModalidadeSAC(valor, prazo, tx_juros);

            //Act
            modalidadeSAC.calculaParcelas(valor, prazo, tx_juros);

            //Assert
            decimal actualPrestacao = modalidadeSAC.parcelas.First<Parcela>().valorPrestacao;
            Assert.AreEqual(valorPrestacaoEsperada, actualPrestacao);
        }
        [TestMethod]
        public void TestaAmortizacaoSAC()
        {
            //Arrange
            decimal valor = 900.00M;
            int prazo = 5;
            decimal tx_juros = 0.0179M;
            decimal valorAmortizacaoEsperada = 180.00M;
            ModalidadeSAC modalidadeSAC = new ModalidadeSAC(valor, prazo, tx_juros);

            //Act
            modalidadeSAC.calculaParcelas(valor, prazo, tx_juros);

            //Assert
            decimal actualAmortizacao = modalidadeSAC.parcelas.First<Parcela>().valorAmortizacao;
            Assert.AreEqual(valorAmortizacaoEsperada, actualAmortizacao);
        }
        [TestMethod]
        public void TestaJurosSAC()
        {
            //Arrange
            decimal valor = 900.00M;
            int prazo = 5;
            decimal tx_juros = 0.0179M;
            decimal valorJurosEsperado = 16.11M;
            ModalidadeSAC modalidadeSAC = new ModalidadeSAC(valor, prazo, tx_juros);

            //Act
            modalidadeSAC.calculaParcelas(valor, prazo, tx_juros);

            //Assert
            decimal actualJuros = modalidadeSAC.parcelas.First<Parcela>().valorJuros;
            Assert.AreEqual(valorJurosEsperado, actualJuros);
        }

        [TestMethod]
        public void TestaPrestacaoPRICE()
        {
            //Arrange
            decimal valor = 900.00M;
            int prazo = 5;
            decimal tx_juros = 0.0179M;
            decimal valorPrestacaoEsperada = 189.78M;
            ModalidadePrice modalidadePRICE = new ModalidadePrice(valor, prazo, tx_juros);

            //Act
            modalidadePRICE.calculaParcelas(valor, prazo, tx_juros);

            //Assert
            decimal actualPrestacao = modalidadePRICE.parcelas.First<Parcela>().valorPrestacao;
            Assert.AreEqual(valorPrestacaoEsperada, actualPrestacao);
        }

        [TestMethod]
        public void TestaJurosPRICE()
        {
            //Arrange
            decimal valor = 900.00M;
            int prazo = 5;
            decimal tx_juros = 0.0179M;
            decimal valorJurosEsperado = 16.11M;
            ModalidadePrice modalidadePRICE = new ModalidadePrice(valor, prazo, tx_juros);

            //Act
            modalidadePRICE.calculaParcelas(valor, prazo, tx_juros);

            //Assert
            decimal actualPrestacao = modalidadePRICE.parcelas.First<Parcela>().valorJuros;
            Assert.AreEqual(valorJurosEsperado, actualPrestacao);
        }

        [TestMethod]
        public void TestaAmortizacaoPRICE()
        {
            //Arrange
            decimal valor = 900.00M;
            int prazo = 5;
            decimal tx_juros = 0.0179M;
            decimal valorAmortizacaoEsperada = 173.67M;
            ModalidadePrice modalidadePRICE = new ModalidadePrice(valor, prazo, tx_juros);

            //Act
            modalidadePRICE.calculaParcelas(valor, prazo, tx_juros);

            //Assert
            decimal actualPrestacao = modalidadePRICE.parcelas.First<Parcela>().valorAmortizacao;
            Assert.AreEqual(valorAmortizacaoEsperada, actualPrestacao);
        }
    }
}