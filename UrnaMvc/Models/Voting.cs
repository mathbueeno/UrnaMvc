namespace UrnaMvc.Models
{
    public class Voting
    {
        public int Id { get; set; }
        public int? CandidateId { get; set; }
        public DateTime DataVoto { get; set; }

        public virtual Candidate Candidate { get; set; }
    }
}
