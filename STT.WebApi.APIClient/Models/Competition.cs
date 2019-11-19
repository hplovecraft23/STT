using System.ComponentModel.DataAnnotations;

namespace STT.WebApi.APIClient.Models
{
    public class Competition
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string areaName { get; set; }
    }
}
