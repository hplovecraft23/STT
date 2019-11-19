using Microsoft.EntityFrameworkCore;
using STT.WebApi.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Pomelo.EntityFrameworkCore.MySql;

namespace STT.WebApi.Data.Logic
{
    public static class DBContextOptionsBuilder
    { 
        public static DbContextOptions<FootballDBContext> GetOptions(string connectionstring)
        {
            var options = new DbContextOptionsBuilder<FootballDBContext>();
            options.UseMySql(connectionstring);
            return options.Options;
        }
    }
}
