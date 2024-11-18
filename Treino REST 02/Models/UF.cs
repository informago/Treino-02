using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Treino_REST_02.Models
{
    [Table("Capitais")]
    public class UF
    {

        [Required] [Column("Id",TypeName = "int")]
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] [Column("UF", TypeName = "char")]
        [MaxLength(2)]
        public required string Nome { get; set; }

        [Required] [Column("Capital",TypeName = "varchar")]
        [MaxLength(50)] 
        public string? Capital { get; set; }

    }

}
