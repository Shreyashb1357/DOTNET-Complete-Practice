using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Linq;
using Model;
namespace DemoApp;

class Server(IShop model)
{
    public void Run()
    {
        var listener = new TcpListener(IPAddress.Any, 4010);
        listener.Start();
        while (true)
        {
            var client = listener.AcceptTcpClient();
            //CommunicateWith(client);
            new Thread (() => CommunicateWith(client)).Start();
        }
    }
    
    private void CommunicateWith(TcpClient connect)
    {
        try
        {
            var channel = connect.GetStream();
            using var reader = new StreamReader(channel);
            using var writer = new StreamWriter(channel) {AutoFlush = true};
            writer.WriteLine("Welcome to BS Group of Business");
            string item = reader.ReadLine();
            Iteminfo info = model.GetItemInformation(item);
            if (info != null)
            {
                writer.WriteLine($"cost={info.Cost}&stock={info.Stock}");
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("Invalid argument passed : {0}", ex.Message);
        }

        connect.Close();
    }
}