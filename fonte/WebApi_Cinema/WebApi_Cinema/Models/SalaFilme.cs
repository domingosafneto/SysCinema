using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_Cinema.Models
{
    [Table("Sala_Filme")]
    public class SalaFilme
    {
        public int IdSala { get; set; }

        public Sala Sala { get; set; }


        public int IdFilme { get; set; }

        public Filme Filme { get; set; }
    }
}
