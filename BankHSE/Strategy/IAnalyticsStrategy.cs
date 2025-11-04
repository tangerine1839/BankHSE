using BankHSE.Models;

namespace BankHSE.Strategy;

public interface IAnalyticsStrategy
{
    void Analyze(List<Operation> operations);

}