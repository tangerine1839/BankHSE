using BankHSE.Models;

namespace BankHSE.Facades;

public class CategoryFacade
{
    private List<Category> _categories;
    private BankFactory _factory;

    public CategoryFacade(BankFactory factory)
    {
        _factory = factory;
        _categories= new List<Category>();
    }
    
    public void CreateCategory(string name, OperationType type)
    {
        try
        {
            Category category = _factory.CreateCategory(type, name);
            _categories.Add(category);
            Console.WriteLine($"Category was created! Id: {category.Id}, name: {name}, type: {type}");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
        
    }
    
    
    public void DeleteCategory(int id)
    {
        var category = _categories.FirstOrDefault(c => c.Id == id);
        if (category != null)
        {
            _categories.Remove(category);
            Console.WriteLine($"Category with id {id} was deleted.");
        }
        else
        {
            Console.WriteLine($"There is no category with id {id}.");

        }
    }

    public int CategoriesCount()
    {
        return _categories.Count;
    }
    public void ShowAllCategories()
    {
        int index = 1;
        Console.WriteLine("List of categories:");
        foreach (Category category in _categories)
        {
            Console.WriteLine($"{index}. CategoryId: {category.Id}, name: {category.Name}, type: {category.Type}");
            index++;
        }
        if (_categories.Count == 0)
        {
            Console.WriteLine("There are no categories.");
        }
    }
    
    public Category GetById(int id)
    {
        Category? account = _categories.FirstOrDefault(a => a.Id == id);
        return account;
    }

    public List<Category> GetAllCategories()
    {
        return _categories;
    }
    public void AddCategory(Category category)
    {
        _categories.Add(category);
    }
    
}