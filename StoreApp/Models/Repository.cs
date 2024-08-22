namespace StoreApp.Models
{
    public static class Repository
    {
        private static List<Candidate> _candidates = new();
        public static IEnumerable<Candidate> Candidates => _candidates;

        public static void Add(Candidate candidate)
        {
            _candidates.Add(candidate);
        }
    }
}
