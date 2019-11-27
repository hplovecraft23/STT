using STT.WebApi.APIClient.Models;
using STT.WebApi.Contract.Models;
using System.Threading.Tasks;

namespace STT.WebApi.Contract.Interfaces
{
    public interface IContractUOW
    {
        //Suggested constructor
        //public IContractUOW(IWebConfiguration webConfiguration, IAPI_FootbalRepository API_FootbalRepository, IFootballUOW footballUOW);

        public Task<ImportLeagueResponse> ImportLeague(string code);
        public Task<TotalPlayesOnLeagueResponse> TotalPlayesOnLeague(string code);
        public Task<CompetitionListDTO> GetLeagues();

        public CompetitionListDTO CompetitionListCache { get; set; }
        public void ChangeAPIKey(string newkey);
        public void CallRoolback();
    }
}
