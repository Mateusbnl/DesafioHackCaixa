using System.ComponentModel.DataAnnotations;

namespace MinhaApi.Models
{
    public class Proposta : Base
    {
        public decimal valorDesejado { get; set; }
        public int Prazo { get; set; }
    }

}
