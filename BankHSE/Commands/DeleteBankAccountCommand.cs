using BankHSE.Facades;

namespace BankHSE.Commands;

public class DeleteBankAccountCommand : ICommand
{
    private BankAccountFacade _bankAccountFacade;

    public DeleteBankAccountCommand(BankAccountFacade bankAccountFacade)
    {
        _bankAccountFacade = bankAccountFacade;
    }

    public void Execute()
    {
        _bankAccountFacade.ShowAllBankAccounts();
        Console.WriteLine("Enter the id of account you want to delete:");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Id is not correct.");
            return;
        }
        _bankAccountFacade.DeleteBankAccount(id);
    }
}