using BankHSE.Facades;

namespace BankHSE.Commands;

public class ExportCommand : ICommand
{
    private BankAccountFacade _bankAccountFacade;
    private OperationFacade _operationFacade;
    private CategoryFacade _categoryFacade;

    public ExportCommand(
        BankAccountFacade bankAccountFacade,
        OperationFacade operationFacade,
        CategoryFacade categoryFacade)
    {
        _bankAccountFacade = bankAccountFacade;
        _operationFacade = operationFacade;
        _categoryFacade = categoryFacade;
    }
    public void Execute()
    {
        Console.WriteLine("SELECT EXPORT FORMAT:");
        Console.WriteLine("1. CSV");
        Console.WriteLine("2. JSON");
        Console.WriteLine("3. YAML");
        Console.WriteLine("4. Cancel");

        Console.Write("Enter your choice (1-4) (file will be saved in the project root directory): ");
        string choice = Console.ReadLine();

        ICommand exportCommand = choice switch
        {
            "1" => new ExportCsvCommand(_bankAccountFacade, _operationFacade, _categoryFacade),
            "2" => new ExportJsonCommand(_bankAccountFacade, _operationFacade, _categoryFacade),
            "3" => new ExportYamlCommand(_bankAccountFacade, _operationFacade, _categoryFacade),
            "4" => null,
            _ => null
        };
        
        if (exportCommand == null)
        {
            Console.WriteLine("Invalid choice or cancelled.");
            return;
        }

        exportCommand.Execute();
    }
}