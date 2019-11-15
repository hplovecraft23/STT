using System.ComponentModel.DataAnnotations;

namespace STT.WebApi.Data.Models
{
    public class Team
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string tla { get; set; }
        public string shortName { get; set; }
        public string areaName { get; set; }
        public string email { get; set; }
    }
}
