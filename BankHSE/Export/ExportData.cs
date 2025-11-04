using BankHSE.Models;

namespace BankHSE.Export;

public class ExportData
{
    public List<BankAccount> BankAccounts { get; set; } = new();
    public List<Operation> Operations { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
}