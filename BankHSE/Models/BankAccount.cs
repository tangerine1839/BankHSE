namespace BankHSE.Models;

public class BankAccount
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public decimal Balance { get; set; }

    public BankAccount(int id, string name, decimal balance)
    {
        Id = id;
        Name = name;
        Balance = balance;
    }
    
    public void UpdateBalance(decimal newBalance)
    {
        Balance = newBalance;
    }
    public BankAccount(){}
}