namespace WebApi_Cinema.Models
{
    public class Sala
    {
        public int IdSala { get; set; }

        public int NumeroSala { get; set; }

        public string Descricao { get; set; }

        
        public ICollection<SalaFilme> SalaFilmes { get; set; } = new List<SalaFilme>();
    }
}
