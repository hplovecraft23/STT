using System.ComponentModel.DataAnnotations;

namespace STT.WebApi.Data.Models
{
    public class Competition_Teams
    {
        [Key]
        public int idCompetition_Teams { get; set; }
        public int Competition_id { get; set; }
        public int Team_id { get; set; }
    }
}
