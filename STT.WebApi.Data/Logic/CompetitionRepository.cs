using STT.WebApi.Data.Interfaces;
using STT.WebApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace STT.WebApi.Data.Logic
{
    public class CompetitionRepository : IFootballRepository<Competition>
    {
        private readonly FootballDBContext _dbcontext;

        public CompetitionRepository(FootballDBContext dBContext)
        {
            _dbcontext = dBContext;
        }

        public void Add(Competition entity)
        {
            _dbcontext.AddAsync(entity);

        }

        public void Delete(Competition entity)
        {
            _dbcontext.Remove(entity);

        }

        public void Edit(Competition entity)
        {
            _dbcontext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        }

        public Competition GetById(int id)
        {
            return _dbcontext.Competitions.Find(id);
        }

        public IEnumerable<Competition> List()
        {
            return _dbcontext.Competitions.AsEnumerable();
        }

        public IEnumerable<Competition> List(Expression<Func<Competition, bool>> predicate)
        {
            return _dbcontext.Competitions.Where(predicate).AsEnumerable();
        }
    }
}
