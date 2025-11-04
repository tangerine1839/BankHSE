using BankHSE.Export;
using BankHSE.Facades;

namespace BankHSE.Commands;

public abstract class ExportCommandBase : ICommand
{
    private BankAccountFacade _bankAccountFacade;
    private OperationFacade _operationFacade;
    private CategoryFacade _categoryFacade;
    public abstract string Format { get; }
    public abstract string FileExtension { get; }

    public ExportCommandBase(
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
        Console.Write($"Enter file name for {Format} export (without extension): ");
        string fileName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(fileName))
        {
            Console.WriteLine("File name cannot be empty");
            return;
        }
        string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string filePath = Path.Combine(projectDirectory, $"{fileName}.{FileExtension}");
        

        try
        {
            var exportData = new ExportData
            {
                BankAccounts = _bankAccountFacade.GetAllAccounts(),
                Operations = _operationFacade.GetAllOperations(),
                Categories = _categoryFacade.GetAllCategories()
            };

            Visitor visitor = CreateVisitor(filePath);
            foreach (var account in exportData.BankAccounts)
                visitor.Visit(account);

            foreach (var category in exportData.Categories)
                visitor.Visit(category);

            foreach (var operation in exportData.Operations)
                visitor.Visit(operation);

            visitor.Save();

            Console.WriteLine($"Data successfully exported to {Format}: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{Format} export failed: {ex.Message}");
        }
    }

    protected abstract Visitor CreateVisitor(string filePath);

   
}