﻿using STT.WebApi.APIClient.Interfaces;

namespace STT.WebApi.APIClient.Models
{
    public class WebApiConfiguration : IWebConfiguration
    {
        public string URL { get; set; }
        public string Token { get; set; }
    }
}
