using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using CareBridge.PerformanceLab;
using CareBridge.PerformanceLab.Models;

while (true)
{
    Console.Clear();

    Console.WriteLine("=================================================");
    Console.WriteLine(" CAREBRIDGE PERFORMANCE LAB");
    Console.WriteLine(" ASSIGNMENT 1 - REVENUE AT RISK DASHBOARD");
    Console.WriteLine("=================================================");
    Console.WriteLine();

    Console.WriteLine("1. Revenue At Risk Dashboard");
    Console.WriteLine("2. Exit");

    Console.WriteLine();
    Console.Write("Choose Option: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            RevenueAtRiskDashboard();
            break;

        case "2":
            return;

        default:
            Console.WriteLine("Invalid Option");
            break;
    }

    Console.WriteLine();
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}

static void RevenueAtRiskDashboard()
{
    using var db = new CareBridgeContext();

    Console.WriteLine();
    Console.WriteLine("REVENUE-AT-RISK DASHBOARD");
    Console.WriteLine("----------------------------------------------------------------------------");

    Stopwatch stopwatch = Stopwatch.StartNew();

    var summary =
        db.Claims
          .AsNoTracking()
          .GroupBy(c => c.Status)
          .Select(g => new RevenueStatusDto
          {
              Status = g.Key,

              ClaimCount = g.Count(),

              TotalBilled =
                  g.Sum(x => x.BilledAmount),

              TotalReimbursed =
                  g.Sum(x => x.ReimbursedAmt) ?? 0m,

              Gap =
                  g.Sum(x => x.BilledAmount)
                  - (g.Sum(x => x.ReimbursedAmt) ?? 0m)
          })
          .OrderByDescending(x => x.TotalBilled)
          .ToList();

    decimal revenueAtRisk =
        summary
            .Where(x => x.Status != "Paid")
            .Sum(x => x.TotalBilled);

    stopwatch.Stop();

    Console.WriteLine();

    Console.WriteLine(
        $"{"Status",-12} {"Claims",-10} {"Billed",-18} {"Reimbursed",-18} {"Gap",-18}");

    Console.WriteLine(
        "----------------------------------------------------------------------------");

    foreach (var row in summary)
    {
        Console.WriteLine(
            $"{row.Status,-12}" +
            $"{row.ClaimCount,-10}" +
            $"{row.TotalBilled,-18:N2}" +
            $"{row.TotalReimbursed,-18:N2}" +
            $"{row.Gap,-18:N2}");
    }

    Console.WriteLine(
        "----------------------------------------------------------------------------");

    Console.WriteLine();

    Console.WriteLine(
        $"REVENUE AT RISK (not Paid) : {revenueAtRisk:N2}");

    Console.WriteLine(
        $"Tracked Entities           : {db.ChangeTracker.Entries().Count()}");

    Console.WriteLine(
        $"Elapsed Time               : {stopwatch.ElapsedMilliseconds} ms");
}