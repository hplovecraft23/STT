using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace STT.WebApi.APIClient.Models
{
    public class MockHandler : HttpMessageHandler
    {
        private Uri LeaguesUri = new Uri("");
        private Uri TeamsUri = new Uri("");
        private Uri PlayersUri = new Uri("");
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri == LeaguesUri )
            {

            }
            return null;
        }
    }
}
