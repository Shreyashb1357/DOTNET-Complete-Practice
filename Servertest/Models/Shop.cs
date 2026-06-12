using System.Text.Json;

namespace Model;
public class Shop : IShop
{
    private IEnumerable<Iteminfo> items;

    public Shop()
    {
        items = Load("Abc.store");
    }
    
    public Iteminfo GetItemInformation(string Name)
    {
        return items.FirstOrDefault(i => i.Id == Name);
    }

    public static void Save(string document, Iteminfo[] entry)
    {
        using var output = new FileStream(document, FileMode.Create);
        JsonSerializer.Serialize(output, entry);
    }

    public static Iteminfo[] Load(string document)
    {
        using var input = new FileStream(document, FileMode.Open);
        return JsonSerializer.Deserialize<Iteminfo[]>(input);
    }
}