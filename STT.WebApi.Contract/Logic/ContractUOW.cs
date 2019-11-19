using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using STT.WebApi.APIClient.Interfaces;
using STT.WebApi.APIClient.Models;
using STT.WebApi.Contract.Interfaces;
using STT.WebApi.Contract.Models;
using STT.WebApi.Data.Interfaces;
using STT.WebApi.APIClient.Logic;
using System.Linq;
using AutoMapper;

namespace STT.WebApi.Contract.Logic
{
    public class ContractUOW : IContractUOW
    {
        private IAPI_FootbalRepository _API_FootbalRepository;
        private IFootballUOW _FootballUOW;
        private IMapper Mapper;
        private AutomapperConfigurations AutomapperConfigurations;

        public ContractUOW(IAPI_FootbalRepository API_FootbalRepository, IFootballUOW footballUOW)
        {
            _API_FootbalRepository = API_FootbalRepository;
            _FootballUOW = footballUOW;
            AutomapperConfigurations = new AutomapperConfigurations();
        }

        private CompetitionListDTO competitioncache;

        public CompetitionListDTO CompetitionListCache
        {
            get { return competitioncache; }
            set { competitioncache = value; }
        }


        public async Task<ImportLeagueResponse> ImportLeague(string code)
        {
            var result = new ImportLeagueResponse();
            try
            {
                if (CompetitionListCache == null)
                {
                    CompetitionListCache = await _API_FootbalRepository.CompetitionListDTO();
                }
                if (CompetitionListCache.Competitions.competitions.Where(x => x.code == code).Any())
                {
                    var competition = CompetitionListCache.Competitions.competitions.Where(x => x.code == code).FirstOrDefault();
                    if (_FootballUOW.Competitions.GetById(competition.id) != null)
                    {
                        result.Message = "League already imported";
                        result.Status = Import_LeagueResults.AlreadyImported;
                    }
                    else
                    {
                        Mapper = AutomapperConfigurations.CompetitionAPItoLocal.CreateMapper();
                        Data.Models.Competition Comp = Mapper.Map<Data.Models.Competition>(competition);
                        _FootballUOW.Competitions.Add(Comp);
                        var teams = await _API_FootbalRepository.TeamCompetitionsDTO(Comp.id);
                        foreach (TeamsCompetitionTeamDTO item in teams.CompetitionTeamList.teams)
                        {
                            int id = item.id;
                            if (_FootballUOW.Teams.GetById(id) != null)
                            {
                                if (_FootballUOW.Competition_Teams.List().Where(x => x.Competition_id == Comp.id && x.Team_id == id).Any())
                                {
                                    continue;
                                }
                                else
                                {
                                    _FootballUOW.Competition_Teams.Add(new Data.Models.Competition_Teams { Competition_id = competition.id, Team_id = item.id });
                                    continue;
                                }
                            }
                            else
                            {
                                Mapper = AutomapperConfigurations.TeamAPItoLocal.CreateMapper();
                                Data.Models.Team Team = Mapper.Map<Data.Models.Team>(item);
                                _FootballUOW.Teams.Add(Team);
                                _FootballUOW.Competition_Teams.Add(new Data.Models.Competition_Teams { Competition_id = competition.id, Team_id = Team.id });
                                continue;
                            }
                        }
                    }
                }
                else
                {
                    result.Message = "Not found";
                    result.Status = Import_LeagueResults.NotFound;
                }
            }
            catch (Exception)
            {
                result.Message = "Server Error";
                result.Status = Import_LeagueResults.ServerError;
            }  
        }

        public async Task<TotalPlayesOnLeagueResponse> TotalPlayesOnLeague(string code)
        {
            if (CompetitionListCache == null)
            {
                CompetitionListCache = await _API_FootbalRepository.CompetitionListDTO();
            }
        }
        public Task<CompetitionListDTO> GetLeagues()
        {
            return _API_FootbalRepository.CompetitionListDTO();
        }

        public void ChangeAPIKey(string newkey)
        {
            string url = _API_FootbalRepository.GetCurrentURL();
            _API_FootbalRepository = new FootBallApiWebClient(configuration: new WebApiConfiguration
            {
                Token = newkey,
                URL = url
            });
        }
    }
}
