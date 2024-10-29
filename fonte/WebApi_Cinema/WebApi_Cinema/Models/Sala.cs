using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_Cinema.Models
{
    [Table("Sala")]
    public class Sala
    {
        [Key]
        public int IdSala { get; set; }

        public int NumeroSala { get; set; }

        public string Descricao { get; set; }

        
       // public ICollection<SalaFilme> SalaFilmes { get; set; } = new List<SalaFilme>();
    }
}
