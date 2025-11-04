using BankHSE.Facades;

namespace BankHSE.Commands;

public class ImportCommand : ICommand
{
    private readonly ImportFacade _importFacade;

    public ImportCommand(ImportFacade importFacade)
    {
        _importFacade = importFacade;
    }

    public void Execute()
    {
        Console.WriteLine("SELECT IMPORT FORMAT:");
        Console.WriteLine("1. CSV");
        Console.WriteLine("2. JSON");
        Console.WriteLine("3. YAML");
        Console.WriteLine("4. Cancel");

        Console.Write("Enter your choice (1-4): ");
        string choice = Console.ReadLine();

        string format = choice switch
        {
            "1" => "csv",
            "2" => "json",
            "3" => "yaml",
            "4" => null,
            _ => null
        };

        if (format == null)
        {
            Console.WriteLine("Invalid choice or cancelled.");
            return;
        }

        Console.Write("Enter file name (relative to project root folder): ");
        string fileName = Console.ReadLine();
        string baseDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        
        string filePath = Path.Combine(baseDirectory, fileName);
        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("File path can not be empty");
            return;
        }

        try
        {
            _importFacade.ImportData(filePath, format);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Import failed: {ex.Message}");
        }
    }
}