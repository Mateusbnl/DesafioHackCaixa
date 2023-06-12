using MinhaApi.Models;

namespace MinhaApi.Interfaces
{
    public interface IProdutoRepository : IDisposable
    {
        public Task<Produto> Get(Proposta proposta);
    }
}
