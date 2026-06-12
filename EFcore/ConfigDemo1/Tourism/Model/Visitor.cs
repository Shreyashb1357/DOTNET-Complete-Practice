namespace DemoApp.Model;

using Microsoft.EntityFrameworkCore;

public readonly record struct Visitor(string Name, string Stars, int Visits, DateTime Recent);