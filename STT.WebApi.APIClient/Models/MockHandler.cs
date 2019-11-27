using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace STT.WebApi.APIClient.Models
{
    public class MockHandler : HttpMessageHandler
    {
        private CompetitionListJSON competitionListJSONMock;
        private TeamCompetitionsJSON TeamCompetitionsJSONMock;
        private GETTeamJSON GETTeamJSON;
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.ToString() == "test.com/v2/competitions")
            {
                return Task.FromResult(new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(competitionListJSONMock)) });
            }
            else if (request.RequestUri.ToString() == "test.com/v2/competitions/123/teams")
            {
                return Task.FromResult(new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(TeamCompetitionsJSONMock)) });
            }
            else if (request.RequestUri.ToString() == "test.com/v2/teams/123")
            {
                return Task.FromResult(new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(GETTeamJSON)) });
            }
            return null;
        }

        public MockHandler(CompetitionListJSON competitionlist, TeamCompetitionsJSON teamCompetitions, GETTeamJSON teamJSON)
        {
            competitionListJSONMock = competitionlist;
            TeamCompetitionsJSONMock = teamCompetitions;
            GETTeamJSON = teamJSON;
        }
    }
}
