using System.Collections.Generic;


namespace Todo_List;
public  interface ITaskRepository
{
    void AddTask (string title);
    List<TaskItem> GetAllTasks();
    void ToggleTaskStatus (int id);
    void DeleteTask (int id);
    void UpdateTaskTitle (int id, string newTitle);
}
