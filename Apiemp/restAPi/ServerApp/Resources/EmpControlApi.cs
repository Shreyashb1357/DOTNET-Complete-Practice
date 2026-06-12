using System.Security.Claims;
using Grpc.Core;
using Grpc.Core.Utils;
using Emp;
using Google.Protobuf.WellKnownTypes;

namespace ServerApp.Resources;

public class EmpControlApi
{
    // public static async Task<IResult> GetEmp(int EId , EmpManagerStub remote)
    // {
    //     var request = new EmpOne{EmpId = EId};
    //     try
    //     {
    //         var reply = await remote.GetOneEmpAsync(request);

    //         var employee = new EmployeeEntry
    //         {
    //             EmpId = reply.EmpId,
    //             EmpName = reply.EmpName,
    //             Age = reply.Age,
    //             Salary = reply.Salary
    //         };
    //         return Results.Ok(employee);
    //     }catch(RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
    //     {
    //         return Results.NotFound("Employee not found! ");
    //     }
    // }

   public static async Task<IResult> GetEmp(int EId, EmpManagerStub remote)
    {
        var request = new EmpOne { EmpId = EId };

        var reply = await remote.GetOneEmpAsync(request);
        if (reply.EmpId == 0)
            return Results.NotFound("Employee not found!");

        var employee = new EmployeeEntry
        {
            EmpId = reply.EmpId,
            EmpName = reply.EmpName,
            Age = reply.Age,
            Salary = reply.Salary
        };

        return Results.Ok(employee);
    }

    public static async Task<IResult> GetAllEmp(EmpManagerStub remote)
    {
        var request = new Empty{};
        try{
            var reply = remote.FetchEmployee(request);
            var resource = from message in await reply.ResponseStream.ToListAsync()
                select new EmployeeEntry
                {
                    EmpId = message.EmpId,
                    EmpName = message.EmpName,
                    Age = message.Age,
                    Salary = message.Salary
                };
            return Results.Ok(resource);
        }catch(RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            return Results.NotFound("Employees Not Found!");
        }
    }

    public static async Task<IResult> AddEmp(EmpDetail edt, EmpManagerStub remote)
    {
        try
        {
            var reply = await remote.AddEmployeeAsync(edt);
            var resource = new EmployeeEntry
            {
                EmpId = edt.EmpId,
                EmpName = edt.EmpName,
                Age = edt.Age,
                Salary = edt.Salary
            };
            return Results.Ok(resource);
        }
        catch(RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            return Results.NotFound("Sorry... Can't Add Employee!");
        }
    }
}