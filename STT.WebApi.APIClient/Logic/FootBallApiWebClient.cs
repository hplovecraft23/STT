using AutoMapper;
using Newtonsoft.Json;
using STT.WebApi.APIClient.Interfaces;
using STT.WebApi.APIClient.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace STT.WebApi.APIClient.Logic
{
    public class FootBallApiWebClient : IAPI_FootbalRepository
    {
        private HttpMessageHandler _handler;
        private HttpClient _httpClient;
        private string ApiToken;
        private string ApiUrl;
        private IMapper Mapper;
        private AutomapperConfig AutomapperConfig;
        private int remainingCalls = 1;

        public FootBallApiWebClient(IWebConfiguration configuration, HttpMessageHandler handler = null)
        {
            if (handler != null)
            {
                _handler = handler;
            }
            ApiToken = configuration.Token;
            ApiUrl = configuration.URL;
            AutomapperConfig = new AutomapperConfig();
        }

        public string GetCurrentURL()
        {
            if (!string.IsNullOrEmpty(ApiUrl))
            {
                return ApiUrl;
            }
            else return string.Empty;
        }

        public async Task<CompetitionListDTO> CompetitionListDTO()
        {
            using (_httpClient = GetHttpClient())
            {
                if (remainingCalls < 1)
                {
                    Thread.Sleep(60000);
                }
                HttpResponseMessage result = await _httpClient.GetAsync("/v2/competitions");
                if (result.IsSuccessStatusCode)
                {
                    Mapper = AutomapperConfig.CompetitionListMapConfig.CreateMapper();
                    string content = await result.Content.ReadAsStringAsync();
                    CompetitionListJSON list = JsonConvert.DeserializeObject<CompetitionListJSON>(content);
                    CompetitionList mappedList = Mapper.Map<CompetitionListJSON, CompetitionList>(list);
                    var headers = result.Headers;
                    headers.TryGetValues("X-API-Version", out var apiversion);
                    headers.TryGetValues("X-Requests-Available-Minute", out var requestsav);
                    _ = int.TryParse(requestsav.First().ToString(), out remainingCalls);
                    headers.TryGetValues("X-Authenticated-Client", out var username);
                    CompetitionListDTO competition = new CompetitionListDTO
                    {
                        Competitions = mappedList,
                        Headers = new Headers
                        {
                            ApiVersion = apiversion.FirstOrDefault().ToString(),
                            RequestsAvailable = requestsav.FirstOrDefault().ToString(),
                            UserName = username.FirstOrDefault().ToString()
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
                if (remainingCalls < 1)
                {
                    Thread.Sleep(60000);
                }
                HttpResponseMessage result = await _httpClient.GetAsync($"/v2/competitions/{CompetitionID}/teams");
                if (result.IsSuccessStatusCode)
                {
                    Mapper = AutomapperConfig.TeamsCompetitionListMapConfig.CreateMapper();
                    string content = await result.Content.ReadAsStringAsync();
                    TeamCompetitionsJSON list = JsonConvert.DeserializeObject<TeamCompetitionsJSON>(content);
                    CompetitionTeamList mappedlist = Mapper.Map<TeamCompetitionsJSON, CompetitionTeamList>(list);
                    var headers = result.Headers;
                    headers.TryGetValues("X-API-Version", out var apiversion);
                    headers.TryGetValues("X-Requests-Available-Minute", out var requestsav);
                    _ = int.TryParse(requestsav.First().ToString(), out remainingCalls);
                    headers.TryGetValues("X-Authenticated-Client", out var username);
                    TeamCompetitionsDTO teamCompetitions = new TeamCompetitionsDTO
                    {
                        CompetitionTeamList = mappedlist,
                        Headers = new Headers
                        {
                            ApiVersion = apiversion.FirstOrDefault().ToString(),
                            RequestsAvailable = requestsav.FirstOrDefault().ToString(),
                            UserName = username.FirstOrDefault().ToString()
                        },
                        Success = true
                    };
                    return teamCompetitions;
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return new TeamCompetitionsDTO() { Message = result.Content.ToString(), Success = false, Forbidden = true };
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
                if (remainingCalls < 1)
                {
                    Thread.Sleep(45000);
                }
                HttpResponseMessage result = await _httpClient.GetAsync($"/v2/teams/{TeamID}");
                if (result.IsSuccessStatusCode)
                {
                    Mapper = AutomapperConfig.GetTeamConfig.CreateMapper();
                    string content = await result.Content.ReadAsStringAsync();
                    GETTeamJSON list = JsonConvert.DeserializeObject<GETTeamJSON>(content);
                    Team convertedTeam = Mapper.Map<Team>(list);
                    var headers = result.Headers;
                    headers.TryGetValues("X-API-Version", out var apiversion);
                    headers.TryGetValues("X-Requests-Available-Minute", out var requestsav);
                    _ = int.TryParse(requestsav.First().ToString(), out remainingCalls);
                    headers.TryGetValues("X-Authenticated-Client", out var username);
                    TeamDTO team = new TeamDTO
                    {
                        Team = convertedTeam,
                        Headers = new Headers
                        {
                            ApiVersion = apiversion.FirstOrDefault().ToString(),
                            RequestsAvailable = requestsav.FirstOrDefault().ToString(),
                            UserName = username.FirstOrDefault().ToString()
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
