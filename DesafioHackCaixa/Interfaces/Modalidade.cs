using MinhaApi.Models;

namespace MinhaApi.Interfaces
{
    public interface IModalidade : IDisposable
    {
        public string Tipo { get; }
        public int prazoEmprestimo { get; }
        public List<Parcela> parcelas { get; set; }

        public List<Parcela> calculaParcelas(decimal ValorEmprestimo, int PrazoEmprestimo, decimal taxa);
    }
}