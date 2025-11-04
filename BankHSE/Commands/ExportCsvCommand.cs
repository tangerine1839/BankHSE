using BankHSE.Export;
using BankHSE.Facades;

namespace BankHSE.Commands;

public class ExportCsvCommand : ExportCommandBase
{
    public override string Format => "CSV";
    public override string FileExtension => "csv";

    public ExportCsvCommand(
        BankAccountFacade bankAccountFacade,
        OperationFacade operationFacade,
        CategoryFacade categoryFacade) 
        : base(bankAccountFacade, operationFacade, categoryFacade) { }

    protected override Visitor CreateVisitor(string filePath)
    {
        return new CsvVisitor(filePath);
    }
}