
using Todo_List;

namespace Todo_List.Tests.Fakes;

public class FakeTaskRepository : ITaskRepository
{
    public List<TaskItem> Tasks { get; } = new();

    public Task AddTaskAsync(string title)
    {
        Tasks.Add(new TaskItem
        {
            Id = Tasks.Count + 1,
            Title = title
        });

        return Task.CompletedTask;
    }

    public Task<List<TaskItem>> GetAllTasksAsync()
    {
        return Task.FromResult(Tasks);
    }

    public Task ToggleTaskStatusAsync(int id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);

        if (task != null)
            task.IsCompleted = !task.IsCompleted;

        return Task.CompletedTask;
    }

    public Task DeleteTaskAsync(int id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);

        if (task != null)
            Tasks.Remove(task);

        return Task.CompletedTask;
    }

    public Task UpdateTaskTitleAsync(int id, string newTitle)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);

        if (task != null)
            task.Title = newTitle;

        return Task.CompletedTask;
    }
}
