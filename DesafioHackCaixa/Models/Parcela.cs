namespace MinhaApi.Models
{
    //Objeto Parcela, que pode ser reutilizada para reuso em novas modalidades, como SACRE, etc
    public class Parcela : Base
    {
        public int numero { get; set; }
        public decimal valorAmortizacao { get; set; }
        public decimal valorJuros { get; set; }
        public decimal valorPrestacao { get; set; }
    }
}