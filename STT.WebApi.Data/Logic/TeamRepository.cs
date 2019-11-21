using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using STT.WebApi.Data.Interfaces;
using STT.WebApi.Data.Models;

namespace STT.WebApi.Data.Logic
{
    public class TeamRepository : IFootballRepository<Team>
    {
        private readonly FootballDBContext _dbcontext;

        public TeamRepository(FootballDBContext dBContext)
        {
            _dbcontext = dBContext;
        }

        public void Add(Team entity)
        {
             _dbcontext.AddAsync(entity);
            
        }

        public void Delete(Team entity)
        {
            _dbcontext.Remove(entity);
            
        }

        public void Edit(Team entity)
        {
            _dbcontext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
           
        }

        public Team GetById(int id)
        {
            return _dbcontext.Teams.Find(id);
        }

        public IEnumerable<Team> List()
        {
            return _dbcontext.Teams.AsEnumerable();
        }

        public IEnumerable<Team> List(Expression<Func<Team, bool>> predicate)
        {
            return _dbcontext.Teams.Where(predicate).AsEnumerable();
        }
    }
}
