using System.ComponentModel.DataAnnotations;

namespace MinhaApi.ViewModels
{
    public class EditorCategoryViewModel
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Informe um valor maior que zero e que seja um numero valido")]
        public decimal valorDesejado { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um prazo maior que zero e que seja um numero valido")]
        public int prazo { get; set; }
    }
}