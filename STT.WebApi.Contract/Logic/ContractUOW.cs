using AutoMapper;
using STT.WebApi.APIClient.Interfaces;
using STT.WebApi.APIClient.Logic;
using STT.WebApi.APIClient.Models;
using STT.WebApi.Contract.Interfaces;
using STT.WebApi.Contract.Models;
using STT.WebApi.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                        return result;
                    }
                    else
                    {
                        await _FootballUOW.dBContext.Database.BeginTransactionAsync();
                        Mapper = AutomapperConfigurations.CompetitionAPItoLocal.CreateMapper();
                        Data.Models.Competition Comp = Mapper.Map<Data.Models.Competition>(competition);
                        _FootballUOW.Competitions.Add(Comp);
                        var teams = await _API_FootbalRepository.TeamCompetitionsDTO(Comp.id);
                        if (teams.Forbidden)
                        {
                            if (_FootballUOW.dBContext.Database.CurrentTransaction != null)
                            {
                                _FootballUOW.dBContext.Database.RollbackTransaction();
                            }

                            result.Message = "This leage is out of your tier.";
                            result.Status = Import_LeagueResults.ServerError;
                            return result;
                        }
                        foreach (TeamsCompetitionTeamDTO item in teams.CompetitionTeamList.teams)
                        {
                            int id = item.id;
                            if (_FootballUOW.Teams.GetById(id) != null)
                            {
                                if (!_FootballUOW.Competition_Teams.List().Where(x => x.Competition_id == Comp.id && x.Team_id == id).Any())
                                {
                                    _FootballUOW.Competition_Teams.Add(new Data.Models.Competition_Teams { Competition_id = competition.id, Team_id = item.id });
                                }
                            }
                            else
                            {
                                Mapper = AutomapperConfigurations.TeamAPItoLocal.CreateMapper();
                                Data.Models.Team Team = Mapper.Map<TeamsCompetitionTeamDTO, Data.Models.Team>(item);
                                _FootballUOW.Teams.Add(Team);
                                _FootballUOW.Competition_Teams.Add(new Data.Models.Competition_Teams { Competition_id = competition.id, Team_id = Team.id });

                            }
                            TeamDTO teamDTO = await _API_FootbalRepository.TeamDTO(item.id);
                            if (!(teamDTO.Team == null || teamDTO.Team.squad == null))
                            {
                                foreach (Player player in teamDTO.Team.squad)
                                {
                                    if (player.position == null)
                                    {
                                        player.position = "Coach";
                                    }
                                    if (player.dateOfBirth == null)
                                    {
                                        player.dateOfBirth = new DateTime(1999, 01, 01);
                                    }
                                    if (_FootballUOW.Players.GetById(player.id) != null)
                                    {
                                        if (!_FootballUOW.TeamPlayers.List().Where(x => x.Player_id == player.id && x.Team_id == item.id).Any())
                                        {
                                            _FootballUOW.TeamPlayers.Add(new Data.Models.TeamPlayers { Player_id = player.id, Team_id = item.id });
                                        }
                                    }
                                    else
                                    {
                                        Mapper = AutomapperConfigurations.PlayerAPIToLocal.CreateMapper();
                                        Data.Models.Player DBPlayer = Mapper.Map<Player, Data.Models.Player>(player);
                                        _FootballUOW.Players.Add(DBPlayer);
                                        _FootballUOW.TeamPlayers.Add(new Data.Models.TeamPlayers() { Player_id = DBPlayer.id, Team_id = item.id });
                                    }
                                }
                            }

                        }

                        await _FootballUOW.dBContext.SaveChangesAsync();
                        await _FootballUOW.dBContext.Database.CurrentTransaction.CommitAsync();
                        result.Message = "Successfully imported";
                        result.Status = Import_LeagueResults.SuccessfullyImported;
                        return result;
                    }
                }

                else
                {
                    if (_FootballUOW.dBContext.Database.CurrentTransaction != null)
                    {
                        await _FootballUOW.dBContext.Database.CurrentTransaction.RollbackAsync();
                    }
                    result.Message = "Not found";
                    result.Status = Import_LeagueResults.NotFound;
                    return result;
                }
            }
            catch (Exception)
            {
                if (_FootballUOW.dBContext.Database.CurrentTransaction != null)
                {
                    _FootballUOW.dBContext.Database.RollbackTransaction();
                }

                result.Message = "Server Error";
                result.Status = Import_LeagueResults.ServerError;
                return result;
            }
        }

        public async Task<TotalPlayesOnLeagueResponse> TotalPlayesOnLeague(string code)
        {
            if (CompetitionListCache == null)
            {
                CompetitionListCache = await _API_FootbalRepository.CompetitionListDTO();
            }
            if (CompetitionListCache.Competitions.competitions.Where(x => x.code == code) != null)
            {
                Competition competition = CompetitionListCache.Competitions.competitions.Where(x => x.code == code).FirstOrDefault();
                var localcomp = _FootballUOW.Competitions.GetById(competition.id);
                if (localcomp != null)
                {
                    int count = 0;
                    List<Data.Models.Competition_Teams> teams = _FootballUOW.Competition_Teams.List().ToList();
                    List<Data.Models.TeamPlayers> players = _FootballUOW.TeamPlayers.List().ToList();
                    foreach (Data.Models.Competition_Teams item in teams.Where(x => x.Competition_id == competition.id))
                    {

                        int teamcount = players.Where(x => x.Team_id == item.Team_id).Count();
                        count += teamcount;
                    }
                    return new TotalPlayesOnLeagueResponse()
                    {
                        LeagueName = competition.name,
                        Players = count,
                        Success = true
                    };
                }
                return new TotalPlayesOnLeagueResponse()
                {
                    Success = false,
                    Message = "League not imported"
                };
            }
            else
            {
                return new TotalPlayesOnLeagueResponse()
                {
                    Success = false,
                    Message = "League not found"
                };
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

        public void CallRoolback()
        {
            if (_FootballUOW.dBContext.Database.CurrentTransaction != null)
            {
                _FootballUOW.dBContext.Database.RollbackTransaction();
            }
        }
    }
}
