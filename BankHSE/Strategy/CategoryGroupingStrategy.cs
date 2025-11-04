using BankHSE.Models;

namespace BankHSE.Strategy;

public class CategoryGroupingStrategy : IAnalyticsStrategy
{
    public void Analyze(List<Operation> operations)
    {
        var grouped = operations.GroupBy(o => o.CategoryId)
            .Select(g => new {
                CategoryId = g.Key,
                TotalIncome = g.Where(o => o.Type == OperationType.Income).Sum(o => o.Amount),
                TotalExpense = g.Where(o => o.Type == OperationType.Expense).Sum(o => o.Amount),
                OperationsCount = g.Count()
            });
        
        Console.WriteLine("GROUPING BY CATEGORIES");
        foreach (var group in grouped)
        {
            Console.WriteLine($"CategoryId: {group.CategoryId}");
            Console.WriteLine($"Income: {group.TotalIncome}");
            Console.WriteLine($"Expense: {group.TotalExpense}");
            Console.WriteLine($"Operations: {group.OperationsCount}");
            Console.WriteLine();
        }
    }
}