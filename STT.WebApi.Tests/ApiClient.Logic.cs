using Moq;
using NUnit.Framework;
using STT.WebApi.APIClient.Interfaces;
using STT.WebApi.APIClient.Logic;
using STT.WebApi.APIClient.Models;

namespace STT.WebApi.Tests
{
    [TestFixture]
    class ApiClient
    {
        private IAPI_FootbalRepository API_Footbal;


        [SetUp]
        public void SetUp()
        {
            Mock<CompetitionListJSON> CompetitionListMock = new Mock<CompetitionListJSON>();
            Mock<TeamCompetitionsJSON> TeamCompetitionsMock = new Mock<TeamCompetitionsJSON>();
            Mock<GETTeamJSON> GetTeamMock = new Mock<GETTeamJSON>();
            MockHandler handler = new MockHandler(CompetitionListMock.Object, TeamCompetitionsMock.Object, GetTeamMock.Object);
            API_Footbal = new FootBallApiWebClient(new WebApiConfiguration { Token = "", URL = "test.com" }, handler);
        }

        [Test]
        public void Competition_Query()
        {
            var test = API_Footbal.CompetitionListDTO();
            Assert.IsTrue(test != null);
        }
        [Test]
        public void CompetitionTeams_Query()
        {
            var test = API_Footbal.TeamCompetitionsDTO(123);
            Assert.IsTrue(test != null);
        }
        [Test]
        public void Team_Query()
        {
            var test = API_Footbal.TeamDTO(123);
            Assert.IsTrue(test != null);
        }
    }
}
