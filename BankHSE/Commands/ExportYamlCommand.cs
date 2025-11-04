using BankHSE.Export;
using BankHSE.Facades;

namespace BankHSE.Commands;

public class ExportYamlCommand : ExportCommandBase
{
    public override string Format => "YAML";
    public override string FileExtension => "yaml";

    public ExportYamlCommand(
        BankAccountFacade bankAccountFacade,
        OperationFacade operationFacade,
        CategoryFacade categoryFacade) 
        : base(bankAccountFacade, operationFacade, categoryFacade) { }

    protected override Visitor CreateVisitor(string filePath)
    {
        return new YamlVisitor(filePath);
    }
}