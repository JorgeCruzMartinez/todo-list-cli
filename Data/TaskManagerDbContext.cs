using Microsoft.EntityFrameworkCore;

namespace Todo_List.Data;

public class TaskManagerDbContext : DbContext
{
    public DbSet<TaskItem> Tasks { get; set; }

    public TaskManagerDbContext (DbContextOptions<TaskManagerDbContext> options) : base(options)
    {
    }
}