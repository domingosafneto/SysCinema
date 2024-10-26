namespace WebApi_Cinema.Models
{
    public class SalaFilme
    {
        public int IdSala { get; set; }

        public Sala Sala { get; set; }


        public int IdFilme { get; set; }

        public Filme Filme { get; set; }
    }
}
