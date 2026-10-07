using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Todo_List.Data;

public class TaskManagerDbContextFactory : IDesignTimeDbContextFactory<TaskManagerDbContext>
{
    public TaskManagerDbContext CreateDbContext (string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TaskManagerDbContext>();

        optionsBuilder.UseSqlServer (@"Server=(localdb)\MSSQLLocalDB;Database=TodoListDb;Trusted_Connection=True;TrustServerCertificate=True;");

        return new TaskManagerDbContext(optionsBuilder.Options);
    }
}