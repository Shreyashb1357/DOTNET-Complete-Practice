using Grpc.Core;
using Grpc.Core.Utils;
using Emp;
using ServerApp.Data.Company;
using Microsoft.EntityFrameworkCore;
using Google.Protobuf.WellKnownTypes;


namespace ServerApp.Services;

public class EmpManagerService(ShopDbContext shop) : empManager.empManagerBase
{
    //rpc AddEmployee(EmpDetail) returns(EmpStatus);
    public override async Task<EmpStatus> AddEmployee(EmpDetail edt , ServerCallContext context)
    {
        var em = new Employee
        {
          EmpId = edt.EmpId,
          EmpName = edt.EmpName,
          Age = edt.Age,
          Salary = edt.Salary  
        };

        shop.Employees.Add(em);
        
        await shop.SaveChangesAsync();
        return new EmpStatus { 
            ConfirmationMsg = "Employee Added!" 
        };
    }

    //rpc FetchEmployee(google.protobuf.Empty) returns(stream AllEmployee);
    public override async Task FetchEmployee(Empty request , IServerStreamWriter<AllEmployee> responseStream, ServerCallContext context)
    {
       var ems = await shop.Employees.ToListAsync();
       foreach(var entry in ems)
        {
            await responseStream.WriteAsync(new AllEmployee
            {
                EmpId = entry.EmpId,
                EmpName = entry.EmpName,
                Age = entry.Age,
                Salary = entry.Salary
            });
        }
    } 

    //rpc GetOneEmp(EmpOne) returns(SingleEmployee);
    public override async Task<SingleEmployee> GetOneEmp(EmpOne request , ServerCallContext context)
    {
        var entry = await shop.Employees.FindAsync(request.EmpId);
        if(entry == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Employee not found"));
        }
        return new SingleEmployee
        {
            EmpId = entry.EmpId,
            EmpName = entry.EmpName,
            Age = entry.Age,
            Salary = entry.Salary
        };
    }

}