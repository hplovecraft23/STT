using System;
namespace STT.WebApi.APIClient.Models
{
    public class TeamDTO
    {
        public Headers Headers { get; set; }
        public Team Team { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
