using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WinnerGenerator_Backend.DTO;
using WinnerGenerator_Backend.Models;
using WinnerGenerator_Backend.Response;
using WinnerGenerator_Backend.Service.Helper;
using WinnerGenerator_Backend.Service.Interface;
using WinnerGenerator_Backend.DataContext;
using WinnerGenerator_Backend.Models.Request;

namespace WinnerGenerator_Backend.Service;

public class WinnerService : IWinnerService
{
    private readonly IMapper _mapper;
    private readonly DataContext.DataContext _dbContext;

    public WinnerService(IMapper mapper, DataContext.DataContext dataContext)
    {
        _mapper = mapper;
        _dbContext = dataContext;
    }

    public async Task<SubmitResponse> SubmitData(SubmitDataDTO requestDTO)
    {
        SubmitResponse response = new SubmitResponse();
        if (requestDTO == null || requestDTO.base64Excel == null || requestDTO.giveawayName == null)
        {
            response.isActive = false;
            response.errorMessage = "Request body is empty!";
            response.hasError = true;
            return response;
        }

        var updateActiveGW = await HelperService.CheckForActiveGiveaway(_dbContext);
        if (!updateActiveGW)
        {
            response.isActive = false;
            response.errorMessage = "You have one giveaway active! Please deactive it, if you want to add another giveaway!";
            response.hasError = true;
            return response;
        }

        var mappedData = _mapper.Map<Winner>(requestDTO);
        _dbContext.Winners.Add(mappedData);
        var affectedRow = await _dbContext.SaveChangesAsync();
        if (affectedRow > 0)
        {
            response.isActive = true;
            response.hasError = false;
        }
        return response;
    }

    public async Task<List<Winner>> GetAllGiveaways()
    {
        var winnersFromDb = _dbContext.Winners.OrderByDescending(order => order.InsertDateTime).ToList();
        if(winnersFromDb.Count() == 0)
        {
            return null;
        }
        return winnersFromDb;
    }

    public async Task<Winner> GetActiveGW()
    {
        var winnerFromDb =  _dbContext.Winners.Where(x => x.IsActive == true);
        if (winnerFromDb.Count() > 1 )
        {
            return null;
        }
        return (await winnerFromDb.FirstOrDefaultAsync());
    }

    public async Task<bool> UpdateActiveField(int id)
    {
        try
        {
            var winnerFromDb = await _dbContext.Winners.FirstOrDefaultAsync(x => x.Id == id);
            if (winnerFromDb == null)
                return false;

            if (winnerFromDb.IsActive == false)
            {
                var updateActiveGW = await HelperService.CheckForActiveGiveaway(_dbContext);
                if (updateActiveGW)
                    winnerFromDb.IsActive = true;
            }
            else
                winnerFromDb.IsActive = false;

            var affectedRows = await _dbContext.SaveChangesAsync();
            if (affectedRows > 0)
                return true;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
        
        return false;
    }

    public async Task<bool> AddWinner(WinnerRequest winnerReq)
    {
        if (winnerReq == null || winnerReq.id == null || winnerReq.fullName == null || string.IsNullOrWhiteSpace(winnerReq.fullName))
        {
            return false;
        }

        var dataFromDb = await _dbContext.Winners.FirstOrDefaultAsync(x => x.Id == winnerReq.id);
        if (dataFromDb.WinnersName == null)
        {
            dataFromDb.WinnersName = winnerReq.fullName;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        else if (dataFromDb.WinnersName != null && (await HelperService.CheckNumberOfWinners(dataFromDb.WinnersName)) <
                 dataFromDb.TotalWinners)
        {
            dataFromDb.WinnersName += ", " + winnerReq.fullName;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
        
    }
}