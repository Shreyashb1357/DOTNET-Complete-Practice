using System.Net.Sockets;

namespace Model;

public class Server(string remote)
{
    public async ValueTask<Iteminfo> FetchItemInfo(string name)
    {
        using var client = new HttpClient
        {
            BaseAddress = new Uri($"http://{remote}/shop/")
        };
        using var response = await client.GetAsync(name);
        if (response.IsSuccessStatusCode)
        {
            var message = await response.Content.ReadAsStringAsync();
            return Iteminfo.Parse(message);
        }
        return default;


    }
}