using System.Collections.Generic;

namespace STT.WebApi.APIClient.Models
{
    public class CompetitionTeamList
    {
        public int count { get; set; }
        public Competition competition { get; set; }
        public List<TeamsCompetitionTeamDTO> teams { get; set; }
    }
}
