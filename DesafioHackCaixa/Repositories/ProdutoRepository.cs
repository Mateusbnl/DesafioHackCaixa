using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using MinhaApi.Interfaces;
using MinhaApi.Models;


namespace MinhaApi.Repositories
{
    //Utilizei o Pattern Repository para realizar a consulta ao Banco de Dados, basicamente eh composta por uma task para retornar um produto conforme os parametros especificados pelo cliente
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly SqlConnection _connection;
        private readonly IMemoryCache _memoryCache;

        internal bool _disposed;

        public ProdutoRepository([FromServices] IMemoryCache cache)
        {
            _connection = new SqlConnection(Configuration.sqlConnection);
            _memoryCache = cache;
            _disposed = false;
        }
        public async Task<Produto> Get(Proposta proposta)
        {
            var produtos = _memoryCache.GetOrCreate("ProdutosCache", entry => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                return GetProdutos(_connection);
            });


            //Caso seja sem limite o valor ou prazo, seta para os valores maximos suportados pelo C#
            foreach (var produto1 in produtos) {
                if (produto1.VR_MAXIMO == 0) {
                    produto1.VR_MAXIMO = decimal.MaxValue;
                }

                if (produto1.NU_MAXIMO_MESES == 0)
                {
                    produto1.NU_MAXIMO_MESES = int.MaxValue;
                }

            }

            var produto = produtos.SingleOrDefault<Produto>(produto => 
            proposta.Prazo >= produto.NU_MINIMO_MESES && proposta.Prazo <= produto.NU_MAXIMO_MESES
            && proposta.valorDesejado >= produto.VR_MINIMO && proposta.valorDesejado <= produto.VR_MAXIMO
            );

            return produto;
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
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposed = true;
        }

        public SqlConnection Get_connection()
        {
            return _connection;
        }

        public IEnumerable<Produto> GetProdutos(SqlConnection _connection)
        {
            var produtos = _connection.GetAll<Produto>();
            return produtos;
        }
    }
}
