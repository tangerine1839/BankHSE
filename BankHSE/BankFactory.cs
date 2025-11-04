using BankHSE.Models;

namespace BankHSE;

public class BankFactory
{
    private int _bankAccountCounter = 1;
    private int _categoryCounter = 1;
    private int _operationCounter = 1;
    
    public BankAccount CreateBankAccount(string name, decimal balance)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name of the account can not be empty.");
        }

        if (balance < 0)
        {
            throw new ArgumentOutOfRangeException("Balance can not be less than zero.");
        }

        return new BankAccount(_bankAccountCounter++, name, balance);
    }

    public Category CreateCategory(OperationType type, string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name of category can not be empty.");
        }

        return new Category(_categoryCounter++, type, name);
    }

    public Operation CreateOperation(decimal amount, DateTime date, string description, 
        int accountId, int categoryId, OperationType type)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException("Amount can not be less than zero.");
        }

        return new Operation(_operationCounter++, amount, date, description, accountId, categoryId, type);
    }
    public BankAccount CreateBankAccountWithId(int id, string name, decimal balance)
    {
        _bankAccountCounter = Math.Max(_bankAccountCounter, id + 1);
        return new BankAccount(id, name, balance);
    }

    public Category CreateCategoryWithId(int id, OperationType type, string name)
    {
        _categoryCounter = Math.Max(_categoryCounter, id + 1);
        return new Category(id, type, name);
    }

    public Operation CreateOperationWithId(int id, decimal amount, DateTime date, string description, 
        int accountId, int categoryId, OperationType type)
    {
        _operationCounter = Math.Max(_operationCounter, id + 1);
        return new Operation(id, amount, date, description, accountId, categoryId, type);
    }
}