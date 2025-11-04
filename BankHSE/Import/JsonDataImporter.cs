namespace BankHSE.Import;

using System.Text.Json;
using BankHSE.Models;


public class JsonDataImporter : DataImporter
{
    protected override void ParseData(string data)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        var jsonData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(data);
        if (jsonData == null)
            throw new InvalidDataException("Invalid JSON format");

        if (jsonData.ContainsKey("BankAccounts"))
        {
            var accountsJson = jsonData["BankAccounts"].GetRawText();
            var accounts = JsonSerializer.Deserialize<List<BankAccount>>(accountsJson, options);
            if (accounts != null)
                BankAccounts.AddRange(accounts);
        }

        if (jsonData.ContainsKey("Categories"))
        {
            var categoriesJson = jsonData["Categories"].GetRawText();
            var categories = JsonSerializer.Deserialize<List<Category>>(categoriesJson, options);
            if (categories != null)
                Categories.AddRange(categories);
        }

        if (jsonData.ContainsKey("Operations"))
        {
            var operationsJson = jsonData["Operations"].GetRawText();
            var operations = JsonSerializer.Deserialize<List<Operation>>(operationsJson, options);
            if (operations != null)
                Operations.AddRange(operations);
        }
    }
}