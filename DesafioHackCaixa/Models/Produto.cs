using Dapper.Contrib.Extensions;

namespace MinhaApi.Models
{
    //Utilizamos o Dapper para mapear os produtos contidos no Banco de Dados, caso seja adicionado um novo campo que seja necess[ario, basta adicionar uma nova propriedade correspondente nessa classe
    [Table("[PRODUTO]")]
    public class Produto : Base
    {
        [ExplicitKey]
        public int CO_PRODUTO { get; set; }
        public string NO_PRODUTO { get; set; }
        public decimal PC_TAXA_JUROS { get; set; }
        public int NU_MINIMO_MESES { get; set; }
        public int NU_MAXIMO_MESES { get; set; }
        public decimal VR_MINIMO { get; set; }
        public decimal VR_MAXIMO { get; set; }

    }
}