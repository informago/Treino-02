using System.ComponentModel.DataAnnotations;

namespace Treino_REST_02.Models
{
    public class UF
    {
        [Required]
        public int Id { get; set; }
        [Required][MaxLength(2)]
        public required string Nome { get; set; }
        [Required][MaxLength(50)]
        public string? Capital { get; set; }
    }
}
