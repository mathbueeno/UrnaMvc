namespace UrnaMvc.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string NomeVice { get; set; }
        public DateTime DataRegistro { get; set; }
        public int Legenda { get; set; }

        public virtual List<Voting> ListaVotos { get; set; }
    }
}
