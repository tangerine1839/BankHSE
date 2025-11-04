using BankHSE.Models;

namespace BankHSE.Export;

public abstract class Visitor
{
    protected string _filePath;
    protected List<string> _lines = new List<string>();

    public Visitor(string filePath)
    {
        _filePath = filePath;
    }
    public abstract void Visit(BankAccount bankAccount);
    public abstract void Visit(Category category);
    public abstract void Visit(Operation operation);
    public abstract void Save();
}