class Program
{
    public static void Main(string[] args) {
        string filepath = "/home/cdac/dotnet/Practice/demo.txt";
        string testdata = File.ReadAllText(filepath);
        Console.WriteLine(testdata);
        // string data = "Hello world";
        // File.WriteAllText(filepath , data);
        // testdata = testdata = File.ReadAllText(filepath);
        // Console.WriteLine(testdata);
        // string[] testDataLineByLine = File.ReadAllLines(filepath);
        // foreach (var entry in testDataLineByLine)
        // {
        //     Console.WriteLine(entry);
        //     Console.WriteLine("=========================================");
        // }
        // Console.WriteLine("-----------------------END---------------------------");

        // byte[] testDataRawBytes = File.ReadAllBytes(filepath);
        // foreach (var entry in testDataRawBytes)
        // {
        //     Console.WriteLine(entry);
        //     Console.WriteLine("=========================================");
        // }

        string data = "Also Certified from IIT Kharagpur";
        File.AppendAllText(filepath, data);
        testdata = File.ReadAllText(filepath);
        Console.WriteLine(testdata);
    }
}