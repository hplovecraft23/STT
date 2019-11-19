using System.Collections.Generic;

namespace STT.WebApi.APIClient.Models
{
    public class CompetitionTeamList
    {
        public int count { get; set; }
        public string[] filters { get; set; }
        public Competition competition { get; set; }
        public List<Team> teams { get; set; }
    }
}
