using System;
using System.Threading.Tasks;
using STT.WebApi.APIClient.Interfaces;
using STT.WebApi.Data.Interfaces;

namespace STT.WebApi.Contract.Interfaces
{
    public interface IContractUOW
    {
        //Suggested constructor
        //public IContractUOW(IWebConfiguration webConfiguration, IAPI_FootbalRepository API_FootbalRepository, IFootballUOW footballUOW);

        public async Task<> ImportLeague();
        public async Task<> TotalPlayesOnLeague();
    }
}
