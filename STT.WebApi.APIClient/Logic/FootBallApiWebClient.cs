﻿using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
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
        private IMapper Mapper;
        private AutomapperConfig AutomapperConfig;

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
            using(_httpClient = GetHttpClient())
            {
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
                HttpResponseMessage result = await _httpClient.GetAsync($"/v2/teams/{TeamID}");
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