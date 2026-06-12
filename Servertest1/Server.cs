using System.Net.Sockets;
using Model;
namespace demo
{
    public class Server(IConfiguration config, ILogger<Server> logger, IShop acc) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoptoken)
        {


        }
    
        private async void CommunicateWith(TcpClient connect)
        {
            try
            {
                int channel = connect.GetStream();
                using var reader = new StreamReader(connect);
                using var writer = new StreamReader(connect);
                writer.WriteLine($"The server start for port : {port}");
                string item = await reader.ReadLineAsync();
                Iteminfo info = acc.GetItemInformation(item);
                if (info != null)
                {
                    await writer.WriteLineAsync($"The Cost : {info.Cost} & Stock : {info.Stock}");
                }

            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Invalid Argument : {0}", ex.Message);
            }
            connect.Close();
        }
    }
}