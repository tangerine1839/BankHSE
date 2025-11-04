using BankHSE.Models;

namespace BankHSE.Import;

using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

public class CsvDataImporter : DataImporter
{
    protected override void ParseData(string data)
    {
        var sections = data.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var section in sections)
        {
            var lines = section.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length < 2) continue;
            
            string sectionName = lines[0].Trim();
            string header = lines[1].Trim();

            var sectionData = string.Join("\n", lines.Skip(1));

            using var reader = new StringReader(sectionData);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
                HasHeaderRecord = true,
                TrimOptions = TrimOptions.Trim


            });

            if (sectionName == "BankAccounts")
            {
                csv.Context.RegisterClassMap<BankAccountMap>();
                BankAccounts.AddRange(csv.GetRecords<BankAccount>().ToList());
            }
            else if (sectionName == "Categories")
            {
                csv.Context.RegisterClassMap<CategoryMap>();
                Categories.AddRange(csv.GetRecords<Category>().ToList());
            }
            else if (sectionName == "Operations")
            {
                csv.Context.RegisterClassMap<OperationMap>();
                Operations.AddRange(csv.GetRecords<Operation>().ToList());
            }
        }
    }
}

public class BankAccountMap : ClassMap<BankAccount>
{
    public BankAccountMap()
    {
        Map(m => m.Id).Name("Id");
        Map(m => m.Name).Name("Name");
        Map(m => m.Balance).Name("Balance");
    }
}

public class CategoryMap : ClassMap<Category>
{
    public CategoryMap()
    {
        Map(m => m.Id).Name("Id");
        Map(m => m.Name).Name("Name");
        Map(m => m.Type).Name("Type");
    }
}

public class OperationMap : ClassMap<Operation>
{
    public OperationMap()
    {
        Map(m => m.Id).Name("Id");
        Map(m => m.Type).Name("Type");
        Map(m => m.AccountId).Name("AccountId");
        Map(m => m.Amount).Name("Amount");
        Map(m => m.Date).Name("Date");
        Map(m => m.CategoryId).Name("CategoryId");
        Map(m => m.Description).Name("Description").Optional();
    }
}