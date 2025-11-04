using BankHSE.Models;

namespace BankHSE.Strategy;

public class BalanceDifferenceStrategy : IAnalyticsStrategy
{
    public void Analyze(List<Operation> operations)
    {
        decimal totalIncome = operations.Where(o => o.Type == OperationType.Income)
            .Sum(o => o.Amount);
        decimal totalExpense = operations.Where(o => o.Type == OperationType.Expense)
            .Sum(o => o.Amount);
        decimal difference = totalIncome - totalExpense;
        
        Console.WriteLine("INCOME VS EXPENSE DIFFERENCE");
        Console.WriteLine($"Total Income: {totalIncome}");
        Console.WriteLine($"Total Expense: {totalExpense}");
        Console.WriteLine($"Difference: {difference}");
    }
}