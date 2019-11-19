using System;
using System.Collections.Generic;
using System.Text;

namespace STT.WebApi.APIClient.Models
{
    public class CompetitionList
    {
        public int count { get; set; }
        public List<Competition> competitions { get; set; }
    }
}
