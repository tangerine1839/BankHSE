using BankHSE.Facades;
using BankHSE.Models;

namespace BankHSE.Observer;

public class BalanceUpdateObserver : IOperationObserver
{
    private BankAccountFacade _bankAccountFacade;

    public BalanceUpdateObserver(BankAccountFacade bankAccountFacade)
    {
        _bankAccountFacade = bankAccountFacade;
    }
    
    public void OnOperationCreated(Operation operation)
    {
        var account = _bankAccountFacade.GetById(operation.AccountId);
        if (account == null)
        {
            return;
        }

        if (operation.Type == OperationType.Income)
        {
            account.UpdateBalance(account.Balance + operation.Amount);
        }
        else
        {
            account.UpdateBalance(account.Balance - operation.Amount);
        }
        
        Console.WriteLine($"Balance of account with id {account.Id} was updated. Current balance: {account.Balance}");
    }
}