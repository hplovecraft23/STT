using STT.WebApi.Data.Interfaces;
using System;

namespace STT.WebApi.Data.Logic
{
    public class FootballUOW : IFootballUOW, IDisposable
    {
        private FootballDBContext _dbcontext;
        public FootballUOW(FootballDBContext dBContext)
        {
            _dbcontext = dBContext;
        }
        private CompetitionRepository _Competitions { get; set; }
        public CompetitionRepository Competitions
        {
            get
            {
                if (_Competitions == null)
                {
                    _Competitions = new CompetitionRepository(_dbcontext);
                }
                return _Competitions;
            }
        }
        private TeamRepository _teams { get; set; }
        public TeamRepository Teams
        {
            get
            {
                if (_teams == null)
                {
                    _teams = new TeamRepository(_dbcontext);
                }
                return _teams;
            }
        }
        private PlayerRepository _players { get; set; }
        public PlayerRepository Players
        {
            get
            {
                if (_players == null)
                {
                    _players = new PlayerRepository(_dbcontext);
                }
                return _players;
            }
        }
        private Competition_TeamsRepository _competition_Teams;
        public Competition_TeamsRepository Competition_Teams
        {
            get
            {
                if (_competition_Teams == null)
                {
                    _competition_Teams = new Competition_TeamsRepository(_dbcontext);
                }
                return _competition_Teams;
            }
        }
        private TeamPlayersRepository _teamPlayers { get; set; }
        public TeamPlayersRepository TeamPlayers
        {
            get
            {
                if (_teamPlayers == null)
                {
                    _teamPlayers = new TeamPlayersRepository(_dbcontext);
                }
                return _teamPlayers;
            }
        }

        public FootballDBContext dBContext
        {
            get
            {
                return _dbcontext;
            }
        }

        public void SaveChanges()
        {
            _dbcontext.SaveChanges();
        }

        public void Dispose()
        {
            ((IDisposable)_dbcontext).Dispose();
        }
    }
}
