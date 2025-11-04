using BankHSE.Models;

namespace BankHSE.Import;

public abstract class DataImporter
{
    public List<BankAccount> BankAccounts = new();
    public List<Category> Categories = new();
    public List<Operation> Operations = new();
    
    public void Import(string filePath)
    {
        ValidateFile(filePath);
        string data = ReadFile(filePath);
        ParseData(data);
    }
    
    protected virtual void ValidateFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("File not found", filePath);
    }

    protected virtual string ReadFile(string filePath)
    {
        return File.ReadAllText(filePath);
    }
    protected abstract void ParseData(string data);
    public List<BankAccount> GetBankAccounts() => BankAccounts;
    public List<Category> GetCategories() => Categories;
    public List<Operation> GetOperations() => Operations;
    
    public int BankAccountsCount => BankAccounts.Count;
    public int CategoriesCount => Categories.Count;
    public int OperationsCount => Operations.Count;
}