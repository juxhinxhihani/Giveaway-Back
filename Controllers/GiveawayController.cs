using Microsoft.AspNetCore.Mvc;
using WinnerGenerator_Backend.DTO;
using WinnerGenerator_Backend.Models.Request;
using WinnerGenerator_Backend.Service.Interface;

namespace WinnerGenerator_Backend.Controllers;

[ApiController]
[Route("[controller]/")]
public class GiveawayController : ControllerBase
{
    private readonly IWinnerService _winnerService;
    public GiveawayController(IWinnerService winnerService)
    {
        _winnerService = winnerService;
    }

    /// <summary>
    /// Submit another giveaway
    /// </summary>
    /// <returns></returns>
    [HttpPost("Submit")]
    public async Task<IActionResult> SubmitData([FromBody] SubmitDataDTO request)
    {
        var result = await _winnerService.SubmitData(request);
        if(result.isActive && !result.hasError)
            return Ok(result);
        else if (result == null)
            return NotFound();
        else
            return BadRequest(result);
    }
    /// <summary>
    /// Get all giveaways
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _winnerService.GetAllGiveaways();
        if(result != null & result.Any())
            return Ok(result);
        else if (result == null)
            return BadRequest("List is empty!");
        else
            return NotFound();
    }
    /// <summary>
    /// Get giveaway by ID
    /// </summary>
    /// <returns></returns>
    [HttpGet("Get/Active")]
    public async Task<IActionResult> GetActive()
    {
        var result = await _winnerService.GetActiveGW();
        if(result != null)
            return Ok(result);
        else if (result == null)
            return BadRequest("No giveaway found!");
        else
            return NotFound();
    }
    
    /// <summary>
    /// Set active/nonActive giveaways
    /// </summary>
    /// <returns></returns>
    [HttpPut("Active/{id}")]
    public async Task<IActionResult> UpdateActive(int id)
    {
        var result = await _winnerService.UpdateActiveField(id);
        if(result)
            return Ok(result);
        else if (!result)
            return BadRequest("List is empty!");
        else
            return NotFound();
    }
    
    /// <summary>
    /// Add winner on active giveaway
    /// </summary>
    /// <returns></returns>
    [HttpPost("Winner")]
    public async Task<IActionResult> AddWinner(WinnerRequest request)
    {
        var result = await _winnerService.AddWinner(request);
        if(result)
            return Ok(result);
        else if (!result)
            return BadRequest("Can not add more winners!");
        else
            return NotFound();
    }
}