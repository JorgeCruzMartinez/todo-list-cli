using System.Text.Json;


namespace Todo_List;

public class JsonTaskRepository : ITaskRepository
{    
    private int _nextId = 1;
    private readonly string _filePath;
    private List<TaskItem> _tasks = [];    


    // 1. CONSTRUCTOR PARA PROGRAM.CS (Sin parámetros)
    // Llama al constructor de abajo pasando la ruta por defecto de la app
    public JsonTaskRepository() : this("tasks.json")
    {
    }

    // 2. CONSTRUCTOR PARA PRUEBAS (Con parámetro)
    public JsonTaskRepository(string filePath)
    {
        _filePath = filePath; // Guarda la ruta en el campo de la clase
        try
        {
            if (File.Exists(_filePath))
            {
                string jsonString = File.ReadAllText(_filePath);
                _tasks = JsonSerializer.Deserialize<List<TaskItem>>(jsonString) ?? [];
                _nextId = _tasks.Count != 0 ? _tasks.Max(t => t.Id) + 1 : 1;
            }
        }
        catch
        {
            _tasks = [];
        }
    }

    public async Task AddTaskAsync (string title)
    {
        _tasks.Add (new TaskItem { Id = _nextId++, Title = title });
        await SaveTasksToFileAsync();
    }

    public async Task<List<TaskItem>> GetAllTasksAsync()
    {
        // En un escenario real, aquí podríamos volver a leer el archivo de forma asíncrona
        return await Task.FromResult (_tasks);
    }

    public async Task ToggleTaskStatusAsync (int id)
    {
        var task = _tasks.FirstOrDefault (t => t.Id == id);
        if (task != null)
        {
            task.IsCompleted = !task.IsCompleted;
            await SaveTasksToFileAsync();
        }
    }

    public async Task DeleteTaskAsync (int id)
    {
        var task = _tasks.FirstOrDefault (t => t.Id == id);
        if (task != null)
        {
            _tasks.Remove (task);
            await SaveTasksToFileAsync();
        }
    }

    public async Task UpdateTaskTitleAsync (int id, string newTitle)
    {
        var task = _tasks.FirstOrDefault (t => t.Id == id);
        if (task != null)
        {
            task.Title = newTitle;
            await SaveTasksToFileAsync();
        }
    }

    private async Task SaveTasksToFileAsync()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize (_tasks, options);
            await File.WriteAllTextAsync (_filePath, jsonString); // ⏳ Operación asíncrona
        }
        catch (Exception ex)
        {
            Console.WriteLine ($"Error al guardar: {ex.Message}");
        }
    }
}