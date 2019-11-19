using System;
namespace STT.WebApi.APIClient.Models
{
    public class CompetitionListDTO
    { 
        public CompetitionList Competitions { get; set; }
        public Headers Headers { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
