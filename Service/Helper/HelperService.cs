using Microsoft.EntityFrameworkCore;

namespace WinnerGenerator_Backend.Service.Helper;

public static class HelperService
{
    
    public static async Task<bool> CheckForActiveGiveaway(DataContext.DataContext _dbContext)
    {
        var hasActiveGiveaways =  _dbContext.Winners.Where(x => x.IsActive == true).ToList();
        if (hasActiveGiveaways.Count() == 0)
            return true;
        else
        {
            foreach (var activeGiveaway in hasActiveGiveaways)
            {
                activeGiveaway.IsActive = false;
            }
            var affectedRows = await _dbContext.SaveChangesAsync();
            if (affectedRows == hasActiveGiveaways.Count())
                return true;
            else
                return false;
        }
    }

    public static async Task<int> CheckNumberOfWinners(string winners)
    {
        string[] namePairs = winners.Split(',');
        return namePairs.Length;
    }
}