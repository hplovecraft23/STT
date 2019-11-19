using System;
using System.Collections.Generic;
using System.Text;

namespace STT.WebApi.Contract.Models
{
    public class ImportLeagueResponse
    {
        public Import_LeagueResults Status { get; set; }
        public string Message { get; set; }
        public string LeagueName { get; set; }
    }
}
