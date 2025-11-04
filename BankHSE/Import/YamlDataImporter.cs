namespace BankHSE.Import;

using YamlDotNet.RepresentationModel;
using BankHSE.Models;


public class YamlDataImporter : DataImporter
{
    protected override void ParseData(string data)
    {
        var input = new StringReader(data);
        var yaml = new YamlStream();
        yaml.Load(input);

        var rootNode = (YamlMappingNode)yaml.Documents[0].RootNode;

        if (rootNode.Children.ContainsKey("BankAccounts"))
        {
            var accountsNode = (YamlSequenceNode)rootNode.Children["BankAccounts"];
            foreach (var node in accountsNode)
            {
                var accountNode = (YamlMappingNode)node;
                var id = int.Parse(accountNode.Children[new YamlScalarNode("Id")].ToString());  
                var name = accountNode.Children[new YamlScalarNode("Name")].ToString();
                var balance = decimal.Parse(accountNode.Children[new YamlScalarNode("Balance")].ToString());

                var account = new BankAccount(id, name, balance);  
                BankAccounts.Add(account);
            }
        }

        if (rootNode.Children.ContainsKey("Categories"))
        {
            var categoriesNode = (YamlSequenceNode)rootNode.Children["Categories"];
            foreach (var node in categoriesNode)
            {
                var categoryNode = (YamlMappingNode)node;
                var id = int.Parse(categoryNode.Children[new YamlScalarNode("Id")].ToString()); 
                var name = categoryNode.Children[new YamlScalarNode("Name")].ToString();
                var type = categoryNode.Children[new YamlScalarNode("Type")].ToString();

                var category = new Category(id, Enum.Parse<OperationType>(type), name) ;  
                Categories.Add(category);
            }
        }

        if (rootNode.Children.ContainsKey("Operations"))
        {
            var operationsNode = (YamlSequenceNode)rootNode.Children["Operations"];
            foreach (var node in operationsNode)
            {
                var operationNode = (YamlMappingNode)node;
                var id = int.Parse(operationNode.Children[new YamlScalarNode("Id")].ToString());  
                var type = Enum.Parse<OperationType>(operationNode.Children[new YamlScalarNode("Type")].ToString());
                var bankAccountId = int.Parse(operationNode.Children[new YamlScalarNode("AccountId")].ToString());
                var amount = decimal.Parse(operationNode.Children[new YamlScalarNode("Amount")].ToString());
                var date = DateTime.Parse(operationNode.Children[new YamlScalarNode("Date")].ToString());
                var categoryId = int.Parse(operationNode.Children[new YamlScalarNode("CategoryId")].ToString());
                var description = operationNode.Children[new YamlScalarNode("Description")]?.ToString();

                var operation = new Operation(id, amount, date, description, bankAccountId, categoryId, type) ;  
                Operations.Add(operation);
            }
        }
    }
}