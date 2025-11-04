using BankHSE.Import;

namespace BankHSE.Facades;
using BankHSE.Models;


public class ImportFacade
{
    private Dictionary<string, DataImporter> _importers;
    private BankAccountFacade _bankAccountFacade;
    private OperationFacade _operationFacade;
    private CategoryFacade _categoryFacade;
    private BankFactory _bankFactory;

    public ImportFacade(
        BankAccountFacade bankAccountFacade,
        OperationFacade operationFacade,
        CategoryFacade categoryFacade,
        BankFactory bankFactory,
        CsvDataImporter csvImporter,
        JsonDataImporter jsonImporter,
        YamlDataImporter yamlImporter)
    {
        _bankAccountFacade = bankAccountFacade;
        _operationFacade = operationFacade;
        _categoryFacade = categoryFacade;
        _bankFactory = bankFactory;
        
        _importers = new Dictionary<string, DataImporter>(StringComparer.OrdinalIgnoreCase)
        {
            ["csv"] = csvImporter,
            ["json"] = jsonImporter,
            ["yaml"] = yamlImporter
        };
    }

    public void ImportData(string filePath, string format)
    {
        if (!_importers.TryGetValue(format, out var importer))
            throw new ArgumentException($"Unsupported format: {format}");

        importer.Import(filePath);
        
        var bankAccounts = importer.GetBankAccounts();
        var categories = importer.GetCategories();
        var operations = importer.GetOperations();
        
        RestoreImportedData(bankAccounts, categories, operations);
        
        Console.WriteLine($"Successfully imported: {bankAccounts.Count} accounts, {categories.Count} categories, {operations.Count} operations");
    }

    private void RestoreImportedData(List<BankAccount> bankAccounts, List<Category> categories, List<Operation> operations)
    {
        foreach (var category in categories)
        {
            var importedCategory = _bankFactory.CreateCategoryWithId(category.Id, category.Type, category.Name);
            _categoryFacade.AddCategory(importedCategory);
        }

        foreach (var account in bankAccounts)
        {
            var importedAccount = _bankFactory.CreateBankAccountWithId(account.Id, account.Name, account.Balance);
            _bankAccountFacade.AddAccount(importedAccount);
        }

        foreach (var operation in operations)
        {
            var importedOperation = _bankFactory.CreateOperationWithId(
                operation.Id, operation.Amount, operation.Date, operation.Description,
                operation.AccountId, operation.CategoryId, operation.Type);
            _operationFacade.AddOperation(importedOperation);
        }
    }
}