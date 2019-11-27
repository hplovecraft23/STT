using System;
using System.Collections.Generic;

namespace STT.WebApi.APIClient.Models
{
    public class Filters
    {
    }

    public class Area
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Winner
    {
        public int id { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string tla { get; set; }
        public string crestUrl { get; set; }
    }

    public class CurrentSeason
    {
        public int id { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int? currentMatchday { get; set; }
        public Winner winner { get; set; }
    }

    public class CompetitionJSON
    {
        public int id { get; set; }
        public Area area { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string emblemUrl { get; set; }
        public string plan { get; set; }
        public CurrentSeason currentSeason { get; set; }
        public int numberOfAvailableSeasons { get; set; }
        public DateTime lastUpdated { get; set; }
    }

    public class CompetitionListJSON
    {
        public int count { get; set; }
        public Filters filters { get; set; }
        public List<CompetitionJSON> competitions { get; set; }
    }
}
