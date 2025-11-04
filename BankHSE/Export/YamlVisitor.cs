using BankHSE.Models;
using YamlDotNet.Serialization;

namespace BankHSE.Export;

public class YamlVisitor : Visitor
{
    private readonly ExportData _data = new ExportData();

    public YamlVisitor(string filePath) : base(filePath) { }

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
        var serializer = new SerializerBuilder().Build();
        var yaml = serializer.Serialize(_data);
        File.WriteAllText(_filePath, yaml);
    }
}