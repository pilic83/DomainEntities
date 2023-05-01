using DomainEntities.Example.AggregateRoots;
using DomainEntities.Example.Entities;
using DomainEntities.Example.StronglyTypedIDs;
using System.Text.Json;

var menuItem = new List<MenuItem>() { 
    new MenuItem("Item1", "Description1"),
    new MenuItem("Item2", "Description2"),
    new MenuItem("Item3", "Description3"),
    new MenuItem("Item4", "Description4")};
var menuSection = new List<MenuSection>(){
    new MenuSection(menuItem, "Section1", "Section description 1"),
    new MenuSection(menuItem, "Section2", "Section description 2"),
    new MenuSection(menuItem, "Section3", "Section description 3")};
var menu = new Menu(menuSection, "Main MENU", "Main MENU description", new HostId());
Console.WriteLine(JsonSerializer.Serialize<Menu>(menu));
Console.ReadLine();