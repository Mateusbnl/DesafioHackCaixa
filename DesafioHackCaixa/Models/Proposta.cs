using System.ComponentModel.DataAnnotations;

namespace MinhaApi.Models
{
    //A proposta de simulacao enviada pelo cliente, caso valida, eh convertida em um objeto proposta. Caso seja adicionado novos campos, basta adicionar aqui
    public class Proposta : Base
    {
        public decimal valorDesejado { get; set; }
        public int Prazo { get; set; }
    }

}
