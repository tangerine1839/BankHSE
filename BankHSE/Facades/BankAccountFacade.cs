using BankHSE.Models;

namespace BankHSE.Facades;

public class BankAccountFacade
{
    private List<BankAccount> _accounts;
    private BankFactory _factory;

    public BankAccountFacade(BankFactory factory)
    {
        _factory = factory;
        _accounts = new List<BankAccount>();
    }
    
    public void CreateBankAccount(string name, decimal balance)
    {
        try
        {
            BankAccount account = _factory.CreateBankAccount(name, balance);
            _accounts.Add(account);
            Console.WriteLine($"Account was created! Id: {account.Id}, name: {name}, balance: {balance}");
        }
        catch (Exception e) when (e is ArgumentException || e is ArgumentOutOfRangeException)
        {
            Console.WriteLine(e.Message);
        }
        
    }
    
    public void DeleteBankAccount(int id)
    {
        BankAccount? account = _accounts.FirstOrDefault(a => a.Id == id);
        if (account != null)
        {
            _accounts.Remove(account);
            Console.WriteLine($"Account with id {id} was deleted.");
        }
        else
        {
            Console.WriteLine($"There is no account with id {id}.");
        }
    }

    public int AccountsCount()
    {
        return _accounts.Count;
    }
    public void ShowAllBankAccounts()
    {
        int index = 1;
        Console.WriteLine("List of accounts:");
        foreach (BankAccount account in _accounts)
        {
            Console.WriteLine($"{index}. Id: {account.Id}, name: {account.Name}, balance: {account.Balance}");
            index++;
        }
        if (_accounts.Count == 0)
        {
            Console.WriteLine("There are no accounts.");
        }
    }

    public BankAccount GetById(int id)
    {
        BankAccount? account = _accounts.FirstOrDefault(a => a.Id == id);
        return account;
    }

    public List<BankAccount>GetAllAccounts()
    {
        return _accounts;
    }
    public void AddAccount(BankAccount account)
    {
        _accounts.Add(account);
    }
}