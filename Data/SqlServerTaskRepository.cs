using Microsoft.EntityFrameworkCore;

namespace Todo_List.Data;

public class SqlServerTaskRepository : ITaskRepository
{
    private readonly TaskManagerDbContext _context;

    public SqlServerTaskRepository (TaskManagerDbContext context)
    {
        _context = context;
    }

    public async Task AddTaskAsync (string title)
    {
        var task = new TaskItem
        {
            Title = title,
            IsCompleted = false
        };

        await _context.Tasks.AddAsync (task);
        await _context.SaveChangesAsync();
    }

    public async Task<List<TaskItem>> GetAllTasksAsync()
    {
        return await _context.Tasks
            .OrderBy (t => t.Id)
            .ToListAsync();
    }

    public async Task ToggleTaskStatusAsync (int id)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync (t => t.Id == id);

        if (task != null)
        {
            task.IsCompleted = !task.IsCompleted;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteTaskAsync (int id)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync (t => t.Id == id);

        if (task != null)
        {
            _context.Tasks.Remove (task);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateTaskTitleAsync (int id, string newTitle)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync (t => t.Id == id);

        if (task != null)
        {
            task.Title = newTitle;
            await _context.SaveChangesAsync();
        }
    }
}