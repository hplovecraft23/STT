using System.Threading.Tasks;
using STT.WebApi.APIClient.Models;

namespace STT.WebApi.APIClient.Interfaces
{
    public interface IAPI_FootbalRepository
    {
        Task<CompetitionListDTO> CompetitionListDTO();
        Task<TeamCompetitionsDTO> TeamCompetitionsDTO(int CompetitionID);
        Task<TeamDTO> TeamDTO(int TeamID);
        string GetCurrentURL();
    }
}
