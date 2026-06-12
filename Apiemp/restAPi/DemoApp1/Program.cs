using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Google.Protobuf.WellKnownTypes;
using Emp;

class Program
{
    static async Task Main(string[] args)
    {
        AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

        using var channel = GrpcChannel.ForAddress("http://localhost:5090");
        var client = new empManager.empManagerClient(channel);

        while (true)
        {
            Console.WriteLine("\n===== EMPLOYEE MANAGEMENT =====");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. View All Employees");
            Console.WriteLine("3. View Specific Employees");
            Console.WriteLine("4. Exit");
            Console.Write("Enter choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await AddEmployee(client);
                    break;

                case "2":
                    await ViewAllEmployees(client);
                    break;

                case "3":
                    await GetOneEmp(client);
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    // -------- Add Employee --------
    static async Task AddEmployee(empManager.empManagerClient client)
    {
        Console.Write("Enter ID: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Enter Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Age: ");
        int age = int.Parse(Console.ReadLine());

        Console.Write("Enter Salary: ");
        double salary = double.Parse(Console.ReadLine());

        var emp = new EmpDetail
        {
            EmpId = id,
            EmpName = name,
            Age = age,
            Salary = salary
        };

        var response = await client.AddEmployeeAsync(emp);
        Console.WriteLine("Server: " + response.ConfirmationMsg);
    }

    // -------- View All Employees --------
    static async Task ViewAllEmployees(empManager.empManagerClient client)
    {
        Console.WriteLine("\n----- All Employees -----");

        var stream = client.FetchEmployee(new Empty());
        Console.WriteLine("=================================");
        while (await stream.ResponseStream.MoveNext(CancellationToken.None))
        {
            var e = stream.ResponseStream.Current;
            Console.WriteLine($"{e.EmpId} {e.EmpName}");
        }
        Console.WriteLine("=================================");

    }

     // -------- Add one Employee --------
    // static async Task GetOneEmp(empManager.empManagerClient client)
    // {
    //     Console.Write("Enter ID: ");
    //     int id = int.Parse(Console.ReadLine());

    //     var response = await client.GetOneEmpAsync(new EmpOne
    //     {
    //         EmpId = id
    //     });

    //     if (response.EmpId == null)
    //     {
    //         Console.WriteLine("Employee not found.");
    //     }

    //     Console.WriteLine("");
    //     Console.WriteLine("=================================");
    //     Console.WriteLine("Employee Details:");
    //     Console.WriteLine($"ID: {response.EmpId}");
    //     Console.WriteLine($"Name: {response.EmpName}");
    //     Console.WriteLine($"Age: {response.Age}");
    //     Console.WriteLine($"Salary: {response.Salary}");
    //     Console.WriteLine("=================================");
    //     Console.WriteLine("");

    // }

    static async Task GetOneEmp(empManager.empManagerClient client)
    {
        Console.Write("Enter ID: ");
        int id = int.Parse(Console.ReadLine());

        try
        {
            var response = await client.GetOneEmpAsync(new EmpOne { EmpId = id });

            // If server returns an empty employee object
            if (response.EmpId == 0)
            {
                Console.WriteLine("===========================");
                Console.WriteLine("");
                Console.WriteLine("Employee not found.");
                Console.WriteLine("");
                Console.WriteLine("===========================");
                return;
            }

            Console.WriteLine("");
            Console.WriteLine("=================================");
            Console.WriteLine("Employee Details:");
            Console.WriteLine($"ID: {response.EmpId}");
            Console.WriteLine($"Name: {response.EmpName}");
            Console.WriteLine($"Age: {response.Age}");
            Console.WriteLine($"Salary: {response.Salary}");
            Console.WriteLine("=================================");
            Console.WriteLine("");
        }
        catch (Grpc.Core.RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
        {
            Console.WriteLine("");
            Console.WriteLine("===========================");
            Console.WriteLine("");
            Console.WriteLine("Employee not found.");
            Console.WriteLine("");
            Console.WriteLine("===========================");
            Console.WriteLine("");
        }
    }


}
