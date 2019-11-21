using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using STT.WebApi.Data.Interfaces;
using STT.WebApi.Data.Models;

namespace STT.WebApi.Data.Logic
{
    public class PlayerRepository : IFootballRepository<Player>
    {
        private readonly FootballDBContext _dbcontext;

        public PlayerRepository(FootballDBContext dBContext)
        {
            _dbcontext = dBContext;
        }

        public void Add(Player entity)
        {
             _dbcontext.AddAsync(entity);
             
        }

        public void Delete(Player entity)
        {
            _dbcontext.Remove(entity);
           
        }

        public void Edit(Player entity)
        {
            _dbcontext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
           
        }

        public Player GetById(int id)
        {
            return _dbcontext.Players.Find(id);
        }

        public IEnumerable<Player> List()
        {
            return _dbcontext.Players.AsEnumerable();
        }

        public IEnumerable<Player> List(Expression<Func<Player, bool>> predicate)
        {
            return _dbcontext.Players.Where(predicate).AsEnumerable();
        }
    }
}
