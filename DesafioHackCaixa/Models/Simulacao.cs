using MinhaApi.Interfaces;

namespace MinhaApi.Models
{
    public class Simulacao : Base
    {

        //Objeto de Simulacao, que ira conter dois tipos de modalidade SAC ou Price, caso seja necessaria adicionar novas modalidades, basta adicionar na lista de IModalidade
        public Simulacao()
        {

            resultadoSimulacao = new List<IModalidade>();

        }
        public int codigoProduto { get; set; }
        public string descricaoProduto { get; set; }
        public decimal taxaJuros { get; set; }
        public List<IModalidade> resultadoSimulacao { get; set; }

        public void GeraSimulacao(Proposta proposta, Produto produto) {
            codigoProduto = produto.CO_PRODUTO;
            descricaoProduto = produto.NO_PRODUTO;
            taxaJuros = Math.Round(produto.PC_TAXA_JUROS, 4);
            resultadoSimulacao.Add(new ModalidadeSAC(proposta.valorDesejado, proposta.Prazo, taxaJuros));
            resultadoSimulacao.Add(new ModalidadePrice(proposta.valorDesejado, proposta.Prazo, taxaJuros));
        }
    }
}
