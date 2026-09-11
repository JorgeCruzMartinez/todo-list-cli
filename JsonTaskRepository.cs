using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;


namespace Todo_List;
public class JsonTaskRepository : ITaskRepository
{
    private int _nextId = 1;
    private const string filePath = "tasks.json";
    private List<TaskItem> _tasks = [];

    public JsonTaskRepository()
    {
        LoadTasksFromFile();
    }

    public void AddTask (string title)
    {
        _tasks.Add (new TaskItem { Id = _nextId++, Title = title });
        SaveTasksToFile();
    }

    public List<TaskItem> GetAllTasks() => _tasks;

    public void ToggleTaskStatus (int id)
    {
        var task = _tasks.FirstOrDefault (t => t.Id == id);
        if (task != null)
        {
            task.IsCompleted = !task.IsCompleted;
            SaveTasksToFile();
        }
    }

    public void DeleteTask (int id)
    {
        var task = _tasks.FirstOrDefault (t => t.Id == id);
        if (task != null)
        {
            _tasks.Remove (task);
            SaveTasksToFile();
        }
    }

    public void UpdateTaskTitle (int id, string newTitle)
    {
        var task = _tasks.FirstOrDefault (t => t.Id == id);
        if (task != null)
        {
            task.Title = newTitle;
            SaveTasksToFile();
        }
    }

    private void SaveTasksToFile()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize (_tasks, options);
            File.WriteAllText (filePath, jsonString);
        }
        catch (Exception ex)
        {
            // Nota: En arquitecturas puras de producción, se inyectaría un Logger aquí.
            Console.WriteLine ($"Error al guardar: {ex.Message}");
        }
    }

    private void LoadTasksFromFile()
    {
        try
        {
            if (!File.Exists  (filePath)) return;

            string jsonString = File.ReadAllText (filePath);
            _tasks = JsonSerializer.Deserialize<List<TaskItem>>(jsonString) ?? [];
            _nextId = _tasks.Count != 0 ? _tasks.Max (t => t.Id) + 1 : 1;
        }
        catch (Exception)
        {
            _tasks = [];
            _nextId = 1;
        }
    }
}