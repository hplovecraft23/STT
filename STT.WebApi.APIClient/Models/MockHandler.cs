using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace STT.WebApi.APIClient.Models
{
    public class MockHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            
        }
    }
}
