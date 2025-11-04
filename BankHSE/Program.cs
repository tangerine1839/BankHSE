using BankHSE.Facades;
using BankHSE.Import;
using BankHSE.Observer;
using BankHSE.Strategy;
using Microsoft.Extensions.DependencyInjection;

namespace BankHSE;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        
        services
            .AddSingleton<BankFactory>()
            .AddSingleton<BankAccountFacade>()
            .AddSingleton<OperationFacade>()
            .AddSingleton<CategoryFacade>()
            .AddSingleton<CommandProcessor>();

        services
            .AddSingleton<IAnalyticsStrategy, BalanceDifferenceStrategy>()
            .AddSingleton<IAnalyticsStrategy, CategoryGroupingStrategy>();
        services.AddSingleton<AnalyticsFacade>();

        services.AddSingleton<CsvDataImporter>();
        services.AddSingleton<JsonDataImporter>();
        services.AddSingleton<YamlDataImporter>();

        services.AddSingleton<ImportFacade>();

        var serviceProvider = services.BuildServiceProvider();
        var operationFacade = serviceProvider.GetRequiredService<OperationFacade>();
        var accountFacade = serviceProvider.GetRequiredService<BankAccountFacade>();
        var balanceObserver = new BalanceUpdateObserver(accountFacade);
        operationFacade.SetBalanceObserver(balanceObserver);
        
        var commandProcessor = serviceProvider.GetRequiredService<CommandProcessor>();
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose action:");
            Console.WriteLine("1. Create account");
            Console.WriteLine("2. Delete account");
            Console.WriteLine("3. Show all accounts");
            Console.WriteLine("4. Create category");
            Console.WriteLine("5. Delete category");
            Console.WriteLine("6. Show all categories");
            Console.WriteLine("7. Create operation");
            Console.WriteLine("8. Show all operations");
            Console.WriteLine("9. Analytics");
            Console.WriteLine("10. Export data");
            Console.WriteLine("11. Import data");
            Console.WriteLine("0. Exit");
            Console.Write("Your choice: ");
            string choice = Console.ReadLine();
            
            if (choice == "0")
            {
                break;
            }

            Console.Clear();
            commandProcessor.Process(choice);

            Console.WriteLine("\nEnter any key to continue...");
            Console.ReadKey();
        }
    }
}