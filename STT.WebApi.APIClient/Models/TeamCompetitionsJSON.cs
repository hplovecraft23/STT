using System;
using System.Collections.Generic;
using System.Text;

namespace STT.WebApi.APIClient.Models
{
    public class Season
    {
        public int id { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int currentMatchday { get; set; }
        public object winner { get; set; }
    }
    public class TeamJSON
    {
        public int id { get; set; }
        public Area area { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string tla { get; set; }
        public string crestUrl { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public string email { get; set; }
        public int founded { get; set; }
        public string clubColors { get; set; }
        public string venue { get; set; }
        public DateTime lastUpdated { get; set; }
    }

    public class TeamCompetitionsJSON
    {
        public int count { get; set; }
        public Filters filters { get; set; }
        public CompetitionJSON competition { get; set; }
        public Season season { get; set; }
        public List<TeamJSON> teams { get; set; }
    }
}
