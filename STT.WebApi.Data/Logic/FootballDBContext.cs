using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using STT.WebApi.Data.Models;

namespace STT.WebApi.Data.Logic
{
    public class FootballDBContext : DbContext
    {
        public FootballDBContext() { }
        public FootballDBContext(DbContextOptions<FootballDBContext> options) : base(options)
        { }
        public DbSet<Competition> Competitions { get ; set ; }
        public DbSet<Competition_Teams> Competition_Teams { get ; set ; }
        public DbSet<Player> Players { get ; set ; }
        public DbSet<Team> Teams { get ; set ; }
        public DbSet<TeamPlayers> TeamPlayers { get ; set ; }
    }
}
