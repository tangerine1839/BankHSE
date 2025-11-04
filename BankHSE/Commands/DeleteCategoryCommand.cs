using BankHSE.Facades;

namespace BankHSE.Commands;

public class DeleteCategoryCommand : ICommand
{
    private CategoryFacade _categoryFacade;

    public DeleteCategoryCommand(CategoryFacade categoryFacade)
    {
        _categoryFacade = categoryFacade;
    }

    public void Execute()
    {
        _categoryFacade.ShowAllCategories();
        Console.WriteLine("Enter the id of category you want to delete:");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Id is not correct.");
            return;
        }
        _categoryFacade.DeleteCategory(id);
    }
}