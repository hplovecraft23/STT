using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using STT.WebApi.Data.Logic;
using STT.WebApi.Data.Models;
using Moq;

namespace STT.WebApi.Tests
{
    [TestFixture]
    public class Tests
    {
        private FootballDBContext dBContext;
        private FootballUOW FootballUOW;
        private Mock<Player> PlayerM;
        private Mock<Competition> CompetitionM;
        private Mock<Team> TeamM;
        private Mock<TeamPlayers> TeamPlayersM;
        private Mock<Competition_Teams> Competition_TeamsM;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FootballDBContext>()
                .UseInMemoryDatabase("SST")
                .Options;
            dBContext = new FootballDBContext(options);
            FootballUOW = new FootballUOW(dBContext);
            PlayerM = new Mock<Player>();
            CompetitionM = new Mock<Competition>();
            TeamM = new Mock<Team>();
            TeamPlayersM = new Mock<TeamPlayers>();
            Competition_TeamsM = new Mock<Competition_Teams>();
        }
        [Test]
        public void Creation_And_Get_Of_New_Player()
        {
            FootballUOW.Players.Add(PlayerM.Object);
            Player py = FootballUOW.Players.GetById(PlayerM.Object.id);
            Assert.IsTrue(py == PlayerM.Object);
        }
        [Test]
        public void Modification_Of_Player()
        {
            int id = PlayerM.Object.id;
            PlayerM = new Mock<Player>();
            PlayerM.Object.id = id;
            FootballUOW.Players.Edit(PlayerM.Object);
            Player py = FootballUOW.Players.GetById(PlayerM.Object.id);
            Assert.IsTrue(py == PlayerM.Object);
        }
        [Test]
        public void Creation_And_Get_Of_New_Competition()
        {
            FootballUOW.Competitions.Add(CompetitionM.Object);
            Competition ct = FootballUOW.Competitions.GetById(CompetitionM.Object.id);
            Assert.IsTrue(ct == CompetitionM.Object);
        }
        [Test]
        public void Modification_Of_Competition()
        {
            int id = CompetitionM.Object.id;
            CompetitionM = new Mock<Competition>();
            CompetitionM.Object.id = id;
            FootballUOW.Competitions.Edit(CompetitionM.Object);
            Competition ct = FootballUOW.Competitions.GetById(CompetitionM.Object.id);
            Assert.IsTrue(ct == CompetitionM.Object);
        }
        [Test]
        public void Creation_And_Get_Of_New_Team()
        {
            FootballUOW.Teams.Add(TeamM.Object);
            Team tm = FootballUOW.Teams.GetById(TeamM.Object.id);
            Assert.IsTrue(tm == TeamM.Object);
        }
        [Test]
        public void Modification_Of_Team()
        {
            int id = TeamM.Object.id;
            TeamM = new Mock<Team>();
            TeamM.Object.id = id;
            FootballUOW.Teams.Edit(TeamM.Object);
            Team tm = FootballUOW.Teams.GetById(TeamM.Object.id);
            Assert.IsTrue(tm == TeamM.Object);
        }
        [Test]
        public void Creation_And_Get_Of_New_TeamPlayer()
        {
            FootballUOW.TeamPlayers.Add(TeamPlayersM.Object);
            TeamPlayers tmp = FootballUOW.TeamPlayers.GetById(TeamPlayersM.Object.idTeamPlayers);
            Assert.IsTrue(tmp == TeamPlayersM.Object);
        }
        [Test]
        public void Modification_Of_TeamPlayer()
        {
            int id = TeamPlayersM.Object.idTeamPlayers;
            TeamPlayersM = new Mock<TeamPlayers>();
            TeamPlayersM.Object.idTeamPlayers = id;
            FootballUOW.TeamPlayers.Edit(TeamPlayersM.Object);
            TeamPlayers tmp = FootballUOW.TeamPlayers.GetById(TeamPlayersM.Object.idTeamPlayers);
            Assert.IsTrue(tmp == TeamPlayersM.Object);
        }
        [Test]
        public void Creation_And_Get_Of_CompetitionTeam()
        {
            FootballUOW.Competition_Teams.Add(Competition_TeamsM.Object);
            Competition_Teams ct = FootballUOW.Competition_Teams.GetById(Competition_TeamsM.Object.idCompetition_Teams);
            Assert.IsTrue(ct == Competition_TeamsM.Object);
        }
        [Test]
        public void Modification_Of_CompetitionTeam()
        {
            int id = Competition_TeamsM.Object.idCompetition_Teams;
            Competition_TeamsM = new Mock<Competition_Teams>();
            Competition_TeamsM.Object.idCompetition_Teams = id;
            FootballUOW.Competition_Teams.Edit(Competition_TeamsM.Object);
            Competition_Teams ct = FootballUOW.Competition_Teams.GetById(Competition_TeamsM.Object.idCompetition_Teams);
            Assert.IsTrue(ct == Competition_TeamsM.Object);
        }
    }
}