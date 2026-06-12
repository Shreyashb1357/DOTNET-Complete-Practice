using System.Net.Sockets;

namespace Model;

public class Server(string remote)
{
    public async ValueTask<Iteminfo> FetchItemInfo(string name)
    {
        using var client = new TcpClient(remote, 4010);
        using var channel = client.GetStream();
        using var reader = new StreamReader(channel);
        using var writer = new StreamWriter(channel) { AutoFlush = true };
        // writer.WriteLine("Welcome to BS Server Clientside");
        await reader.ReadLineAsync();
        await writer.WriteLineAsync(name);
        string message = await reader.ReadLineAsync();
        if (message != null)
            return Iteminfo.Parse(message);
        return default;

    }
}