using System;
using System.ComponentModel.DataAnnotations;

namespace STT.WebApi.APIClient.Models
{
    public class Player
    {
        [Key]
        public int id { get; set; }
        public string  position { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string countryOfBirth { get; set; }
        public string nationality { get; set; }
    }
}
