namespace BankHSE.Models;

public class Operation
{
    public int Id { get; set; }
    public OperationType Type { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    
    public Operation(int id, decimal amount, DateTime date, string description, 
        int accountId, int categoryId, OperationType type)
    {
        Id = id;
        Amount = amount;
        Date = date;
        Description = description;
        AccountId = accountId;
        CategoryId = categoryId;
        Type = type;
    }
    public Operation(){}
}