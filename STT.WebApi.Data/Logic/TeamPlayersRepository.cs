using STT.WebApi.Data.Interfaces;
using STT.WebApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace STT.WebApi.Data.Logic
{
    public class TeamPlayersRepository : IFootballRepository<TeamPlayers>
    {
        private readonly FootballDBContext _dbcontext;

        public TeamPlayersRepository(FootballDBContext dBContext)
        {
            _dbcontext = dBContext;
        }

        public void Add(TeamPlayers entity)
        {
            _dbcontext.AddAsync(entity);

        }

        public void Delete(TeamPlayers entity)
        {
            _dbcontext.Remove(entity);

        }

        public void Edit(TeamPlayers entity)
        {
            _dbcontext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        }

        public TeamPlayers GetById(int id)
        {
            return _dbcontext.TeamPlayers.Find(id);
        }

        public IEnumerable<TeamPlayers> List()
        {
            return _dbcontext.TeamPlayers.AsEnumerable();
        }

        public IEnumerable<TeamPlayers> List(Expression<Func<TeamPlayers, bool>> predicate)
        {
            return _dbcontext.TeamPlayers.Where(predicate).AsEnumerable();
        }
    }
}
