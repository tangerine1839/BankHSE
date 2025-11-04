using BankHSE.Models;
using BankHSE.Observer;

namespace BankHSE.Facades;

public class OperationFacade
{
    private BankFactory _factory;
    private List<Operation> _operations;
    private IOperationObserver _balanceObserver;

    public OperationFacade(BankFactory factory)
    {
        _factory = factory;
        _operations = new List<Operation>();
    }

    public List<Operation> GetAllOperations()
    {
        return _operations;
    }
    
    public void SetBalanceObserver(IOperationObserver observer)
    {
        _balanceObserver = observer;
    }
    
    public void CreateOperation(decimal amount, DateTime date, string description, 
        int accountId, int categoryId, OperationType type)
    {
        try
        {
            Operation operation = _factory.CreateOperation(amount, date, description, accountId, categoryId, type);
            _operations.Add(operation);
            Console.WriteLine(
                $"Operation was created! Id: {operation.Id}, date: {operation.Date}, type: {operation.Type}," +
                $"amount: {operation.Amount}, account: {operation.AccountId}, category: {operation.CategoryId}");
            _balanceObserver?.OnOperationCreated(operation);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine(e.Message);
        }
    }
    public void AddOperation(Operation operation)
    {
        _operations.Add(operation);
    }
    
    public void ShowAllOperations()
    {
        Console.WriteLine("List of operations:");
        for (int i = 0; i < _operations.Count; ++i)
        {
            Operation operation = _operations[i];
            Console.WriteLine($"{i+ 1}) OperationId: {operation.Id}, date: {operation.Date}, type: {operation.Type}," +
                $"amount: {operation.Amount}, account: {operation.AccountId}, category: {operation.CategoryId}");
        }
        
    }
}