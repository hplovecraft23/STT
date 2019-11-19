using System;
using System.Collections.Generic;
using System.Text;
using STT.WebApi.Data.Logic;

namespace STT.WebApi.Data.Interfaces
{
    public interface IFootballUOW
    {
        public CompetitionRepository Competitions { get; }
        public TeamRepository Teams { get; }
        public PlayerRepository Players { get; }
        public Competition_TeamsRepository Competition_Teams { get;}
        public TeamPlayersRepository TeamPlayers { get; }

        void Dispose();
        public void SaveChanges();
    }
}
