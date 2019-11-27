using System.Collections.Generic;

namespace STT.WebApi.APIClient.Models
{
    public class CompetitionList
    {
        public int count { get; set; }
        public List<Competition> competitions { get; set; }
    }
}
