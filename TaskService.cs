namespace Todo_List;

public class TaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task AddTaskAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(
                "El título no puede estar vacío.",
                nameof(title));

        await _repository.AddTaskAsync(title.Trim());
    }

    public async Task<List<TaskItem>> GetAllTasksAsync()
    {
        return await _repository.GetAllTasksAsync();
    }

    public async Task ToggleTaskStatusAsync(int id)
    {
        await _repository.ToggleTaskStatusAsync(id);
    }

    public async Task DeleteTaskAsync(int id)
    {
        await _repository.DeleteTaskAsync(id);
    }

    public async Task UpdateTaskTitleAsync(int id, string newTitle)
    {
        if (string.IsNullOrWhiteSpace(newTitle))
            throw new ArgumentException(
                "El título no puede estar vacío.",
                nameof(newTitle));

        await _repository.UpdateTaskTitleAsync(id, newTitle.Trim());
    }
}
