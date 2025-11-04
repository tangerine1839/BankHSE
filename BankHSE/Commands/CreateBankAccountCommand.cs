using BankHSE.Facades;

namespace BankHSE.Commands;

public class CreateBankAccountCommand : ICommand
{
    private BankAccountFacade _bankAccountFacade;

    public CreateBankAccountCommand(BankAccountFacade bankAccountFacade)
    {
        _bankAccountFacade = bankAccountFacade;
    }

    public void Execute()
    {
        Console.WriteLine("Enter the name of account:");
        string name = Console.ReadLine();

        Console.WriteLine("Enter the balance of account:");
        if (!decimal.TryParse(Console.ReadLine(), out var initialBalance))
        {
            Console.WriteLine("Balance is not correct.");
            return;
        }
        _bankAccountFacade.CreateBankAccount(name, initialBalance);

    }
}