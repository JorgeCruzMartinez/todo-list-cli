namespace Todo_List;

public interface ITaskRepository
{
    Task AddTaskAsync (string title);
    Task<List<TaskItem>> GetAllTasksAsync();
    Task ToggleTaskStatusAsync (int id);
    Task DeleteTaskAsync (int id);
    Task UpdateTaskTitleAsync (int id, string newTitle);
}