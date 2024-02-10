using System;
using System.Collections.Generic;

namespace WinnerGenerator_Backend.Models;

public partial class Winner
{
    public int Id { get; set; }

    public string GiveawayName { get; set; } = null!;

    public int? TotalWinners { get; set; }

    public int TotalCandidates { get; set; }

    public bool IsActive { get; set; }

    public DateTime? InsertDateTime { get; set; }

    public string? WinnersName { get; set; }

    public string Images { get; set; } = null!;

    public string ExcelFile { get; set; } = null!;
}
