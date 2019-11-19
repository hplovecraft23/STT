using System;
namespace STT.WebApi.APIClient.Interfaces
{
    public interface IWebConfiguration
    {
        public string URL { get; set; }
        public string Token { get; set; }
    }
}
