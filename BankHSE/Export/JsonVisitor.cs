using System.Xml;
using BankHSE.Models;
using Newtonsoft.Json;

namespace BankHSE.Export;

public class JsonVisitor : Visitor
{
    private ExportData _data = new ExportData();

    public JsonVisitor(string filePath) : base(filePath) { }

    public override void Visit(BankAccount bankAccount)
    {
        _data.BankAccounts.Add(bankAccount);
    }

    public override void Visit(Category category)
    {
        _data.Categories.Add(category);
    }
    
    public override void Visit(Operation operation)
    {
        _data.Operations.Add(operation);
    }

    public override void Save()
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(_data, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(_filePath, json);    }
}