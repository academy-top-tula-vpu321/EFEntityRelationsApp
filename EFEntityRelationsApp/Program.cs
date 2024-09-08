using EFEntityRelationsApp;
using Microsoft.EntityFrameworkCore;

using (CompaniesContext context = new())
{
    //DataInit(context);

    //var projects = context.Projects
    //                        .Include(p => p.Employees)
    //                        .ToList();
    //foreach(var project in projects)
    //{
    //    Console.WriteLine($"Project: {project.Title}");
    //    foreach(var e in project.Employees)
    //        Console.WriteLine($"\t{e.Name}");
    //}
    //Console.WriteLine();

    //var employees = context.Employees
    //                        .Include(e => e.Projects)
    //                        .ToList();
    //foreach (var e in employees)
    //{
    //    Console.WriteLine($"Employee: {e.Name}");
    //    foreach (var p in e.Projects)
    //        Console.WriteLine($"\t{p.Title}");
    //    Console.WriteLine();
    //}


    //var projects = context.Projects
    //                        .Include(p => p.Employees)
    //                        .ToList();

    //foreach(var p in projects)
    //{
    //    Console.WriteLine($"Project: {p.Title}");
    //    foreach(var ep in p.EmployeeProjects)
    //        Console.WriteLine($"\t{ep.Employee.Name} - {ep.StartDate.ToShortDateString()}");
    //}

    //var products = context.Products;
    //foreach(var p in products)
    //    Console.WriteLine($"{p.Id} - {p.Title} - {p?.Parent?.Id}");

    var products = context.Products
                            .Include(p => p.ChildsProducts)
                            .ToList();

    foreach (var topLevelProduct in products)
        if(topLevelProduct.Parent is null)
            ProductPrint(topLevelProduct, 0);

}


void DataInit(CompaniesContext context)
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    City russiaCapital = new() { Title = "Moscow" };

    Country russia = new() { Title = "Russia", Capital = russiaCapital };
    context.Countries.Add(russia);

    Position developer = new() { Title = "Developer" };
    Position tester = new() { Title = "Tester" };

    Language cpp = new() { Title = "C++" };
    Language cs = new() { Title = "C#" };

    Company yandex = new() { Title = "Yandex", Country = russia, Language = cpp };
    Company ozon = new() { Title = "Ozon", Country = russia, Language = cs };

    context.Companies.AddRange(yandex, ozon);

    Project mobileApp = new() { Title = "Mobile App project", Language = cpp };
    Project site = new() { Title = "Site for bank", Language = cs };
    context.Projects.AddRange(mobileApp, site);


    Employee tom = new()
    {
        Name = "Tom",
        BirthDate = new(2002, 9, 4),
        Company = yandex,
        Position = developer,
        Passport = new() { Series = "1234", Number = "123456" },
    };

    Employee bob = new()
    {
        Name = "Bob",
        BirthDate = new(1990, 8, 11),
        Company = ozon,
        Position = tester,
        Passport = new() { Series = "2222", Number = "202020" },
    };

    Employee leo = new()
    {
        Name = "Leo",
        BirthDate = new(2001, 4, 12),
        Company = ozon,
        Position = developer,
        Passport = new() { Series = "5432", Number = "987654" },
    };

    Employee jim = new()
    {
        Name = "Jim",
        BirthDate = new(1998, 11, 25),
        Company = yandex,
        Position = tester,
        Passport = new() { Series = "5555", Number = "555555" },
    };

    context.Employees.AddRange(tom, bob, leo, jim);

    //bob.Projects.Add(mobileApp);
    //tom.Projects.Add(mobileApp);
    //tom.Projects.Add(site);
    //leo.Projects.Add(site);

    bob.EmployeeProjects.Add(new() { Project = mobileApp, StartDate = new(2024, 6, 10) });
    tom.EmployeeProjects.Add(new() { Project = mobileApp });
    tom.EmployeeProjects.Add(new() { Project = site, StartDate = new(2024, 7, 23) });
    leo.EmployeeProjects.Add(new() { Project = site, StartDate = DateTime.Now.AddMonths(-3) });


    Product textEditors = new() { Title = "Text Editors" };
    Product graphEditors = new() { Title = "Graph Editors" };
    Product os = new() { Title = "Operation system" };

    Product word = new() { Title = "Word", Parent = textEditors };
    Product lexicon = new() { Title = "Lexicon", Parent = textEditors };

    Product photoshop = new() { Title = "Photoshop", Parent = graphEditors };
    Product illustrator = new() { Title = "Illustrator", Parent = graphEditors };
    Product corelDraw = new() { Title = "Corel Draw", Parent = graphEditors };

    Product corelDrawTech = new() { Title = "Corel Draw Technical", Parent = corelDraw };
    Product corelDraw3D = new() { Title = "Corel Draw 3D", Parent = corelDraw };

    Product windows = new() { Title = "Windows", Parent = os };

    context.Products.AddRange(textEditors, graphEditors, os, word, lexicon, photoshop, illustrator, corelDraw, corelDrawTech, corelDraw3D, windows);

    context.SaveChanges();
}


void ProductPrint(Product product, int level)
{
    string title = new string('\t', level * 2) + product.Title;
    Console.WriteLine(title);

    if (product.ChildsProducts.Count > 0)
        foreach (var childProduct in product.ChildsProducts)
            ProductPrint(childProduct, level + 1);
}