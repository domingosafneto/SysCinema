namespace WebApi_Cinema.Models
{
    public class Filme
    {
        public int IdFilme { get; set; }

        public string Nome { get; set; }

        public string? Diretor { get; set; }

        public int Duracao { get; set; }

        
        public ICollection<SalaFilme> SalaFilmes { get; set; }
    }
}
