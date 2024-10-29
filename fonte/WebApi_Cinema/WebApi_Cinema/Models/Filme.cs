using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_Cinema.Models
{
    [Table("Filme")]
    public class Filme
    {
        [Key]
        public int IdFilme { get; set; }

        public string Nome { get; set; }

        public string? Diretor { get; set; }

        public int Duracao { get; set; }  // minutos

        
      //  public ICollection<SalaFilme> SalaFilmes { get; set; } = new List<SalaFilme>();
    }
}
