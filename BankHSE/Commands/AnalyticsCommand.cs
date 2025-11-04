using BankHSE.Facades;

namespace BankHSE.Commands;

public class AnalyticsCommand : ICommand
{
    
    private AnalyticsFacade _analyticsFacade;
    
    public AnalyticsCommand(AnalyticsFacade analyticsFacade)
    {
        _analyticsFacade = analyticsFacade;
    }
    
    public void Execute()
    {
        _analyticsFacade.ShowAvailableAnalytics();
        Console.WriteLine("Enter analytics number:");
        if (int.TryParse(Console.ReadLine(), out int number))
        {
            _analyticsFacade.PerformAnalysis(number);
        }
        else
        {
            Console.WriteLine("Invalid number.");
        }
    }
}