using BankHSE.Facades;
using BankHSE.Models;

namespace BankHSE.Commands;

public class CreateCategoryCommand : ICommand
{
    private CategoryFacade _categoryFacade;

    public CreateCategoryCommand(CategoryFacade categoryFacade)
    {
        _categoryFacade = categoryFacade;
    }

    public void Execute()
    {
        Console.WriteLine("Enter the name of category:");
        string name = Console.ReadLine();

        Console.WriteLine("Press 1 if it is income, press 2 if it is expense");
        string choice = Console.ReadLine();

        OperationType type = OperationType.Income;
        if (choice == "2")
            type = OperationType.Expense;

        _categoryFacade.CreateCategory(name, type);
    }
}