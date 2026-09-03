using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Todo_List
{
    public class TaskManager
    {
        private int _nextId = 1;
        private const string filePath = "tasks.json";
        private List<TaskItem> _tasks = new();

        public TaskManager()
        {
            LoadTasksFromFile();
        }

        public void AddTask(string title)
        {
            _tasks.Add(new TaskItem { Id = _nextId++, Title = title });
            SaveTasksToFile();
        }

        public List<TaskItem> GetAllTasks() => _tasks;

        // Cambia el estado de la tarea y guarda automáticamente
        public void ToggleTaskStatus(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                SaveTasksToFile();
            }
        }

        // Serializa la lista y la guarda en el archivo JSON
        private void SaveTasksToFile()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(_tasks, options);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar: {ex.Message}");
            }
        }

        // Lee el archivo JSON y restaura el estado de las tareas
        private void LoadTasksFromFile()
        {
            try
            {
                if (!File.Exists(filePath)) return;

                string jsonString = File.ReadAllText(filePath);
                _tasks = JsonSerializer.Deserialize<List<TaskItem>>(jsonString) ?? new List<TaskItem>();

                // Calcula el siguiente ID autoincremental basado en el ID más alto guardado
                _nextId = _tasks.Any() ? _tasks.Max(t => t.Id) + 1 : 1;
            }
            catch (Exception)
            {
                _tasks = new List<TaskItem>();
                _nextId = 1;
            }
        }

        //public void DeleteTask(int id)
        //{            
        //    var task = _tasks.FirstOrDefault(t => t.Id == id);
        //    if (task != null)
        //    {
        //        _tasks.Remove(task);
        //        SaveTasksToFile(); // Guardamos los cambios inmediatamente en el archivo JSON
        //    }
        //}
    }
}