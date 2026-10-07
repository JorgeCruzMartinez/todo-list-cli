using Todo_List;
using Todo_List.Data;
using Microsoft.EntityFrameworkCore;

class Program
{
    static async Task Main(string[] args)
    {
        var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
            .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=TodoListDb;Trusted_Connection=True;TrustServerCertificate=True;")
            .Options;

        using var context = new TaskManagerDbContext(options);

        ITaskRepository repository = new SqlServerTaskRepository(context);

        ConsoleUserInterface ui = new(repository);

        await ui.RunAsync();
    }
}