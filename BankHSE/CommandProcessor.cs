
using BankHSE.Commands;
using BankHSE.Facades;

namespace BankHSE;

public class CommandProcessor
{
    private BankAccountFacade _accountFacade;
    private OperationFacade _operationFacade;
    private CategoryFacade _categoryFacade;
    private AnalyticsFacade _analyticsFacade;
    private ImportFacade _importFacade;

    public CommandProcessor(
        BankAccountFacade accountFacade, 
        OperationFacade operationFacade, 
        CategoryFacade categoryFacade,
        AnalyticsFacade analyticsFacade,
        ImportFacade importFacade)
    {
        _accountFacade = accountFacade;
        _operationFacade = operationFacade;
        _categoryFacade = categoryFacade;
        _analyticsFacade = analyticsFacade;
        _importFacade = importFacade;
    }

    public void Process(string commandNumber)
    {
        ICommand? command = null;
        
        switch (commandNumber)
        {
            case "1": 
                command = new TimingDecorator(new CreateBankAccountCommand(_accountFacade)); 
                break;
            case "2": 
                command = new TimingDecorator(new DeleteBankAccountCommand(_accountFacade)); 
                break;
            case "3": 
                _accountFacade.ShowAllBankAccounts(); 
                break;
            case "4": 
                command = new TimingDecorator(new CreateCategoryCommand(_categoryFacade)); 
                break;
            case "5": 
                command = new TimingDecorator(new DeleteCategoryCommand(_categoryFacade)); 
                break;
            case "6": 
                _categoryFacade.ShowAllCategories(); 
                break;
            case "7": 
                command = new TimingDecorator(new CreateOperationCommand(_operationFacade, _accountFacade, _categoryFacade)); 
                break;
            case "8": 
                _operationFacade.ShowAllOperations(); 
                break;
            case "9": 
                command = new TimingDecorator(new AnalyticsCommand(_analyticsFacade)); 
                break;
            case "10": 
                command = new TimingDecorator(new ExportCommand(_accountFacade, _operationFacade, _categoryFacade)); 
                break;
            case "11": 
                command = new TimingDecorator(new ImportCommand(_importFacade)); 
                break;
            default: 
                Console.WriteLine("Wrong command. Try again."); 
                break;
        }

        command?.Execute();
    }
}