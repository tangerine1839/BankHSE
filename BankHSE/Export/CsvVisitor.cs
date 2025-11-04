using BankHSE.Models;

namespace BankHSE.Export;

public class CsvVisitor : Visitor
{
    public CsvVisitor(string filePath) : base(filePath) { }

    public override void Visit(BankAccount bankAccount)
    {
        if (_lines.Count == 0)
        {
            _lines.Add("BankAccounts");
            _lines.Add("Id,Name,Balance");
        }
        _lines.Add($"{bankAccount.Id},{EscapeCsv(bankAccount.Name)},{bankAccount.Balance}");
    }
    
    public override void Visit(Category category)
    {
        if (!_lines.Any(line => line.StartsWith("Categories")))
        {
            _lines.Add("");
            _lines.Add("Categories");
            _lines.Add("Id,Name,Type");
        }
        _lines.Add($"{category.Id},{EscapeCsv(category.Name)},{category.Type}");
    }
    
    public override void Visit(Operation operation)
    {
        if (!_lines.Any(line => line.StartsWith("Operations")))
        {
            _lines.Add("");
            _lines.Add("Operations");
            _lines.Add("Id,Type,BankAccountId,Amount,Date,CategoryId,Description");
        }
        _lines.Add($"{operation.Id},{operation.Type},{operation.AccountId},{operation.Amount},{operation.Date:dd.MM.yyyy HH:mm:ss},{operation.CategoryId},{EscapeCsv(operation.Description ?? "")}");
    }
    
    public override void Save()
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        File.WriteAllLines(_filePath, _lines);
    }

    private string EscapeCsv(string field)
    {
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            return $"\"{field.Replace("\"", "\"\"")}\"";
        }
        return field;
    }
}