namespace STT.WebApi.APIClient.Models
{
    public class TeamCompetitionsDTO
    {
        public Headers Headers { get; set; }
        public CompetitionTeamList CompetitionTeamList { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool Forbidden { get; set; }
    }
}
