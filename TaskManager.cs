using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_List
{
    public class TaskManager
    {
        private int _nextId = 1;
        private readonly List<TaskItem> _tasks = new();        

        public void AddTask(string title)
        {
            _tasks.Add(new TaskItem { Id = _nextId++, Title = title });
        }

        public List<TaskItem> GetAllTasks() => _tasks;

        public void ToggleTaskStatus(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
            }
        }
    }
}
