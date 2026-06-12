using DemoApp.Employee.Model;
namespace DemoApp.Controller;
using Microsoft.AspNetCore.Mvc;


public class HomeController(SiteModel model) : Controller
{
    public IActionResult Index()
    {
        var em = model.GetEmp();
        return View(em); 
    }

    public IActionResult Fill()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Fill(decimal empNo, string empName , DateOnly hireDate, decimal empSal, decimal empComm, string actionType)
    {
        if (actionType == "Add")
        {
            model.AddEmp(empNo, empName, hireDate, empSal, empComm);
        }
        else if (actionType == "Remove")
        {
            model.RemoveEmp(empNo);
        }
        return RedirectToAction("Index");
    }

    // [HttpPost]
    // public IActionResult Fill(decimal empNo)
    // {
    //     model.RemoveEmp(empNo);
    //     return RedirectToAction("Index");
    // }
}
