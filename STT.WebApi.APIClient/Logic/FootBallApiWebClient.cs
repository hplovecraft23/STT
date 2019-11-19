using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using STT.WebApi.APIClient.Interfaces;
using STT.WebApi.APIClient.Models;

namespace STT.WebApi.APIClient.Logic
{
    public class FootBallApiWebClient : IAPI_FootbalRepository
    {
        private HttpMessageHandler _handler;
        private HttpClient _httpClient;
        private string ApiToken;
        private string ApiUrl;

        public FootBallApiWebClient(IWebConfiguration configuration, HttpMessageHandler handler = null)
        {
            if (handler != null)
            {
                _handler = handler;
            }
            ApiToken = configuration.Token;
            ApiUrl = configuration.URL;
        }

        public async Task<CompetitionListDTO> CompetitionListDTO()
        {
            using(_httpClient = GetHttpClient())
            {
                HttpResponseMessage result = await _httpClient.GetAsync("/competitions");
                if (result.IsSuccessStatusCode)
                {
                    CompetitionList list = JsonConvert.DeserializeObject<CompetitionList>(result.Content.ToString());
                    CompetitionListDTO competition = new CompetitionListDTO
                    {
                        Competitions = list,
                        Headers = new Headers
                        {
                            ApiVersion = result.Headers.GetValues("X-API-Version").ToString(),
                            RequestsAvailable = result.Headers.GetValues("X-Requests-Available-Minute").ToString(),
                            UserName = result.Headers.GetValues("X-Authenticated-Client").ToString()
                        },
                        Success = true
                    };
                    return competition;
                }
                else
                {
                    return new CompetitionListDTO() { Message = result.Content.ToString(), Success = false };
                }

            }
        }

        public async Task<TeamCompetitionsDTO> TeamCompetitionsDTO(int CompetitionID)
        {
            using (_httpClient = GetHttpClient())
            {
                HttpResponseMessage result = await _httpClient.GetAsync($"/competitions/{CompetitionID}/teams");
                if (result.IsSuccessStatusCode)
                {
                    CompetitionTeamList list = JsonConvert.DeserializeObject<CompetitionTeamList>(result.Content.ToString());
                    TeamCompetitionsDTO teamCompetitions = new TeamCompetitionsDTO
                    {
                        CompetitionTeamList = list,
                        Headers = new Headers
                        {
                            ApiVersion = result.Headers.GetValues("X-API-Version").ToString(),
                            RequestsAvailable = result.Headers.GetValues("X-Requests-Available-Minute").ToString(),
                            UserName = result.Headers.GetValues("X-Authenticated-Client").ToString()
                        },
                        Success = true
                    };
                    return teamCompetitions;
                }
                else
                {
                    return new TeamCompetitionsDTO() { Message = result.Content.ToString(), Success = false };
                }
            }
        }
        
        public async Task<TeamDTO> TeamDTO(int TeamID)
        {
            using (_httpClient = GetHttpClient())
            {
                HttpResponseMessage result = await _httpClient.GetAsync($"/teams/{TeamID}");
                if (result.IsSuccessStatusCode)
                {
                    Team list = JsonConvert.DeserializeObject<Team>(result.Content.ToString());
                    TeamDTO team = new TeamDTO
                    {
                        Team = list,
                        Headers = new Headers
                        {
                            ApiVersion = result.Headers.GetValues("X-API-Version").ToString(),
                            RequestsAvailable = result.Headers.GetValues("X-Requests-Available-Minute").ToString(),
                            UserName = result.Headers.GetValues("X-Authenticated-Client").ToString()
                        },
                        Success = true
                    };
                    return team;
                }
                else
                {
                    return new TeamDTO() { Message = result.Content.ToString(), Success = false };
                }
            }
        }

        private HttpClient GetHttpClient()
        {
            HttpClient http;
            if (_handler != null)
            {
                http = new HttpClient(_handler);
            }
            else
            {
                http = new HttpClient();
            }
            http.BaseAddress = new Uri(ApiUrl);
            http.DefaultRequestHeaders.Add("X-Auth-Token", ApiToken);
            return http;
        }
    }
}
