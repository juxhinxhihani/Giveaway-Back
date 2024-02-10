namespace WinnerGenerator_Backend.DTO;

public class SubmitDataDTO
{
    public string giveawayName { get; set; } 

    public string base64Excel { get; set; } 
    
    public int totalWinners { get; set; }

    public string images { get; set; } 
}