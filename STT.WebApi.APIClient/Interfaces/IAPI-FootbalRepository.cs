using STT.WebApi.APIClient.Models;
using System.Threading.Tasks;

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
