using Faker;
using Spectre.Console;

if(!AnsiConsole.Confirm("Do you want to run this test about Spectre.Console?"))
{
    AnsiConsole.MarkupLine("Ok... :(");
    return;
}

AnsiConsole.MarkupLine("[bold green]Hello[/] [italic blue]World[/]!");

AnsiConsole.MarkupLine("This is [#ff0000]red[/], this is [#00ff00]green[/], this is [#0000ff]blue[/].");

AnsiConsole.MarkupLine("[bold red on yellow blink underline]Warning![/] This is very [italic green on black strikethrough]important[/].");

Console.WriteLine();

var table = new Table();
table.AddColumn("Name");
table.AddColumn("Age");
table.AddColumn("Occupation");

table.AddRow("Alice", "23", "Software Engineer");
table.AddRow("Bob", "32", "Accountant");
table.AddRow("Charlie", "28", "Teacher");

table.Title = new TableTitle("[underline yellow]People[/]");
table.Caption = new TableTitle("[grey]Some random people[/]");

AnsiConsole.Write(table);

Console.WriteLine();

var tree = new Tree("[yellow]Root[/]");

var child1 = tree.AddNode(new Markup("[green]Child 1[/]"));
var child2 = tree.AddNode(new Markup("[green]Child 2[/]"));
var child3 = tree.AddNode(new Markup("[green]Child 3[/]"));

child1.AddNode("[blue]Grandchild 1-1[/]");
child1.AddNode("[blue]Grandchild 1-2[/]");

child2.AddNode("[green]Grandchild 2-1[/]");

var grandchild3 = child3.AddNode("[green]Grandchild 3-1[/]");
child3.AddNode("[green]Grandchild 3-2[/]");
grandchild3.AddNode("[yellow]Great Grandchild 3-1-1[/]");
grandchild3.AddNode("[yellow]Great Grandchild 3-1-2[/]");

AnsiConsole.Write(tree);

Console.WriteLine();

// Synchronous
AnsiConsole.Status()
    .Spinner(Spinner.Known.Dots8)
    .SpinnerStyle(Style.Parse("green bold"))
    .Start("Thinking...", ctx =>
    {
        // Simulate some work
        AnsiConsole.MarkupLine("Doing some work...");
        Thread.Sleep(1000);

        // Update the status and spinner
        //ctx.Status("Thinking some more");
        //ctx.SpinnerStyle(Style.Parse("green"));

        // Simulate some work
        AnsiConsole.MarkupLine("Doing some more work...");
        Thread.Sleep(2000);
    });

Console.WriteLine();

await AnsiConsole.Progress()
    .StartAsync(async ctx =>
    {
        // Define tasks
        var task1 = ctx.AddTask("[green]Chrome RAM usage[/]");
        var task2 = ctx.AddTask("[yellow]VS Code RAM usage[/]");

        while (!ctx.IsFinished)
        {
            // Simulate some work
            await Task.Delay(100);

            // Increment
            task1.Increment(4.5);
            task2.Increment(2);
        }
    });

Console.WriteLine();

AnsiConsole.Write(new BarChart() // Create a bar chart
    .Width(60)
    .Label("[green bold underline]Global Smartphone Shipments Market Share (%)[/]") // Set the label of the chart
    .CenterLabel() //And center it
    .AddItem("Apple", 23, Color.Yellow) // Add the items with lables, values, and colors
    .AddItem("Samsung", 19, Color.Green)
    .AddItem("Xiaomi", 11, Color.Red)
    .AddItem("OPPO", 10, Color.Blue)
    .AddItem("Vivo", 8, Color.DarkMagenta)
    .AddItem("Others", 29, Color.Orange1));

Console.WriteLine();

// Using Live Display
// Create a table
var table2 = new Table()
    .Border(TableBorder.Rounded)
    .AddColumn("Id")
    .AddColumn("Name")
    .AddColumn("Age");

// Add some initial rows
table2.AddRow(Identification.SocialSecurityNumber(), Faker.Name.First(),
    Faker.RandomNumber.Next(18, 99).ToString());
table2.AddRow(Identification.SocialSecurityNumber(), Faker.Name.First(),
    Faker.RandomNumber.Next(18, 99).ToString());

// Use LiveDisplay to update the table
await AnsiConsole.Live(table2)
    .StartAsync(async ctx =>
    {
        // Loop until we are done
        for (int i = 0; i < 5; i++)
        {
            var id = Faker.Identification.SocialSecurityNumber();
            var name = Faker.Name.First();
            var age = Faker.RandomNumber.Next(18, 99);
            table2.AddRow(id, name, age.ToString());

            ctx.Refresh();

            // Simulate doing the work
            await Task.Delay(1000);
        }
    });