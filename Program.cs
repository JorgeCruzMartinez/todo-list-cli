using Todo_List;
using Todo_List.Data;
using Microsoft.EntityFrameworkCore;


var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
                      .UseSqlServer (@"Server = (localdb)\MSSQLLocalDB;Database=TodoListDb;Trusted_Connection=True;
                                                   TrustServerCertificate=True;")
                      .Options;

using var context = new TaskManagerDbContext (options);

// Instanciar SqlServerTaskRepository (NO JsonTaskRepository)
ITaskRepository repository = new SqlServerTaskRepository (context);
ConsoleUserInterface ui = new (repository);

// Ejecución asíncrona del menú principal
await ui.RunAsync();
