namespace Handler;
public class Greeting
{
    public async Task Ticks(HttpResponse response)
    {
        await response.WriteAsync(Environment.TickCount.ToString());
    }

    
    public static async Task Welcome(HttpResponse response)
    {
        await response.WriteAsync($"""
        <html>
            <head>
                <title>DemoApp</title>
            </head>
            <body>
                <h1>Welcome Visitor</h1>
                <p>
                    <b>Current Time: </b>{DateTime.Now}
                </p>
                <form action="Login" method="POST">
                    <b>Name: </b>
                    <input type="text" name="person"/>
                    <input type="submit" value="Greet"/>
                </form>
            </body>
        </html>
        """);
    }
    public static async Task Hello(HttpRequest request, HttpResponse response)
    {
        await response.WriteAsync("""
            <html>
                <head
                    <title>DemoApp</title>
                </head>
                <body>
                    <h1>Hello {name}</h1>
                <p>
                    <b>Your OTP: </b>{Random.Shared.Next(100000, 1000000)}
                </p>
                </body>
            </html>
        """);
    }
}