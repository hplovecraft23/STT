using Microsoft.EntityFrameworkCore;
using STT.WebApi.Data.Interfaces;
using STT.WebApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace STT.WebApi.Data.Logic
{
    public class Competition_TeamsRepository : IFootballRepository<Competition_Teams>
    {
        private readonly FootballDBContext _dbcontext;

        public Competition_TeamsRepository(FootballDBContext dBContext)
        {
            _dbcontext = dBContext;
        }

        public void Add(Competition_Teams entity)
        {
            _dbcontext.AddAsync(entity);

        }

        public void Delete(Competition_Teams entity)
        {
            _dbcontext.Remove(entity);

        }

        public void Edit(Competition_Teams entity)
        {
            _dbcontext.Entry(entity).State = EntityState.Modified;

        }

        public Competition_Teams GetById(int id)
        {
            return _dbcontext.Competition_Teams.Find(id);
        }

        public IEnumerable<Competition_Teams> List()
        {
            return _dbcontext.Competition_Teams.AsEnumerable();
        }

        public IEnumerable<Competition_Teams> List(Expression<Func<Competition_Teams, bool>> predicate)
        {
            return _dbcontext.Competition_Teams.Where(predicate).AsEnumerable();
        }
    }
}
