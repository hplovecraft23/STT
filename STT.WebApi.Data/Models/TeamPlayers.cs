
using System.ComponentModel.DataAnnotations;

namespace STT.WebApi.Data.Models
{
    public class TeamPlayers
    {
        [Key]
        public int idTeamPlayers { get; set; }
        public int Team_id { get; set; }
        public int Player_id { get; set; }
    }
}
