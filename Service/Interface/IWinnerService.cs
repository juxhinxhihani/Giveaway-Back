using WinnerGenerator_Backend.DTO;
using WinnerGenerator_Backend.Models;
using WinnerGenerator_Backend.Models.Request;
using WinnerGenerator_Backend.Response;

namespace WinnerGenerator_Backend.Service.Interface;

public interface IWinnerService
{
    Task<SubmitResponse> SubmitData(SubmitDataDTO requestDTO);
    Task<List<Winner>> GetAllGiveaways();
    Task<Winner> GetActiveGW();
    Task<bool> UpdateActiveField(int id);
    Task<bool> AddWinner(WinnerRequest winnerReq);
}