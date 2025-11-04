using BankHSE.Facades;
using BankHSE.Models;

namespace BankHSE.Commands;

public class CreateOperationCommand : ICommand
{
    private OperationFacade _operationFacade;
    private BankAccountFacade _bankAccountFacade;
    private CategoryFacade _categoryFacade;

    public CreateOperationCommand(OperationFacade operationFacade, BankAccountFacade bankAccountFacade, CategoryFacade categoryFacade)
    {
        _operationFacade = operationFacade;
        _bankAccountFacade = bankAccountFacade;
        _categoryFacade = categoryFacade;
    }

    public void Execute()
    {
        if (_bankAccountFacade.AccountsCount() == 0)
        {
            Console.WriteLine("You need to create at least one account before creating operations.");
            return;
        }

        if (_categoryFacade.CategoriesCount() == 0)
        {
            Console.WriteLine("You need to create at least one category before creating operations.");
            return;
        }
        
        _bankAccountFacade.ShowAllBankAccounts();
        
        Console.WriteLine("Enter id of account:");
        if (!int.TryParse(Console.ReadLine(), out var accountId))
        {
            Console.WriteLine("Wrong id.");
            return;
        }

        BankAccount account = _bankAccountFacade.GetById(accountId);
        if (account == null)
        {
            Console.WriteLine($"There is no account with id {accountId}.");
            return;
        }
        
        Console.WriteLine("Enter the operation amount:");
        if (!decimal.TryParse(Console.ReadLine(), out var amount))
        {
            Console.WriteLine("Wrong amount.");
            return;
        }

        Console.WriteLine("Enter description (optional):");
        string description = Console.ReadLine();

        _categoryFacade.ShowAllCategories();
        
        Console.WriteLine("Enter id of category:");
        if (!int.TryParse(Console.ReadLine(), out var categoryId))
        {
            Console.WriteLine("Wrong id.");
            return;
        }
        var category = _categoryFacade.GetById(categoryId);
        if (category == null)
        {
            Console.WriteLine($"There is no category with id {categoryId}.");
            return;
        }
        
        OperationType opType = category.Type;

        _operationFacade.CreateOperation(
            amount,
            DateTime.Now,
            description,
            accountId,
            categoryId,
            opType
        );
        
    }
}