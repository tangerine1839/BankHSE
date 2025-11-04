using BankHSE.Export;
using BankHSE.Facades;

namespace BankHSE.Commands;

public class ExportJsonCommand : ExportCommandBase
{
    public override string Format => "JSON";
    public override string FileExtension => "json";

    public ExportJsonCommand(
        BankAccountFacade bankAccountFacade,
        OperationFacade operationFacade,
        CategoryFacade categoryFacade) 
        : base(bankAccountFacade, operationFacade, categoryFacade) { }

    protected override Visitor CreateVisitor(string filePath)
    {
        return new JsonVisitor(filePath);
    }
}