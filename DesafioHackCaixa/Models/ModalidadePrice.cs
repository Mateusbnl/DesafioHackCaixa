using Dapper.Contrib.Extensions;
using MinhaApi.Interfaces;

namespace MinhaApi.Models
{
    //Classe para o Objeto simulacaoPrice, que ira conter uma listagem de parcelas que foram calculadas utilizando o Price
    public class ModalidadePrice : IModalidade
    {
        public ModalidadePrice(decimal ValorEmprestimo, int PrazoEmprestimo, decimal taxa)
        {
            Tipo = "PRICE";
            prazoEmprestimo = PrazoEmprestimo;
            parcelas = calculaParcelas(ValorEmprestimo, PrazoEmprestimo, taxa);
            disposed = false;
        }

        public string Tipo { get; }
        public int prazoEmprestimo { get; }
        public List<Parcela> parcelas { get; set; }
        [Computed]
        private bool disposed { get; set; }

        public List<Parcela> calculaParcelas(decimal valor, int prazo, decimal taxa)
        {
            var parcelas = new List<Parcela>();
            //Valor fixo da Amortizacao da tabela PRICE
            var prestacao = (valor * taxa) / (decimal)(1 - Math.Pow(1 + ((double)taxa), -prazo));

            for (int i = 1; i <= prazo; i++)
            {
                var parcela = new Parcela();
                parcela.numero = i;
                parcela.valorAmortizacao = Math.Round(prestacao - (valor * taxa), 2);
                parcela.valorJuros = Math.Round(valor * taxa, 2);
                parcela.valorPrestacao = Math.Round(prestacao, 2);
                parcelas.Add(parcela);

                valor = valor - parcela.valorAmortizacao;
            }
            return parcelas;
        }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                foreach (var parcela in parcelas)
                {
                    parcela.Dispose();
                }
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            disposed = true;
        }
    }
}