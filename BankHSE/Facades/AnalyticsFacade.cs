using BankHSE.Strategy;

namespace BankHSE.Facades;

public class AnalyticsFacade
{
    private OperationFacade _operationFacade;
    private List<IAnalyticsStrategy> _strategies;
    
    public AnalyticsFacade(OperationFacade operationFacade, IEnumerable<IAnalyticsStrategy> strategies)
    {
        _operationFacade = operationFacade;
        _strategies = strategies.ToList();
    }
    
    public void ShowAvailableAnalytics()
    {
        Console.WriteLine("AVAILABLE ANALYTICS");
        Console.WriteLine("1. Get balance difference between income and expenses.");
        Console.WriteLine("2. Get statistics by categories.");
    }
    
    public void PerformAnalysis(int strategyNumber)
    {
        if (strategyNumber >= 1 && strategyNumber <= _strategies.Count)
        {
            var operations = _operationFacade.GetAllOperations();
            if (!operations.Any())
            {
                Console.WriteLine("No operations for analysis");
                return;
            }
            
            _strategies[strategyNumber - 1].Analyze(operations);
        }
        else
        {
            Console.WriteLine("Invalid report number");
        }
    }
}