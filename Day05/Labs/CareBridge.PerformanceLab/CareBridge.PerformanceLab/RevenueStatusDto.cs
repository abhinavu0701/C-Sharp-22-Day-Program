namespace CareBridge.PerformanceLab;

public class RevenueStatusDto
{
    public string Status { get; set; } = string.Empty;

    public int ClaimCount { get; set; }

    public decimal TotalBilled { get; set; }

    public decimal TotalReimbursed { get; set; }

    public decimal Gap { get; set; }
}