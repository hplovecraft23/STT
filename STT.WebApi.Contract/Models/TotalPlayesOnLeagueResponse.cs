namespace STT.WebApi.Contract.Models
{
    public class TotalPlayesOnLeagueResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string LeagueName { get; set; }
        public int Players { get; set; }
    }
}
