using System;
using Spectre.Console;
using System.Threading;


namespace Todo_List;
public class ConsoleUserInterface
{
    private readonly ITaskRepository _repository;

    // DIP: Inyectamos la abstracción
    public ConsoleUserInterface(ITaskRepository repository)
    {
        _repository = repository;
    }

    public void Run()
    {
        bool keepRunning = true;
        while (keepRunning)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write (new FigletText("TODO List CLI").Centered().Color(Color.Blue));
            ShowTasksTable();

            string option = AnsiConsole.Prompt (
                new SelectionPrompt<string>()
                    .Title("\n[yellow]¿Qué deseas hacer?[/]")
                    .PageSize(5)
                    .AddChoices(new[] { "Agregar Tarea", "Cambiar Estado (Completar/Pendiente)", "Renombrar Tarea", "Eliminar Tarea", "Salir" }));

            switch (option)
            {
                case "Agregar Tarea":
                    var title = AnsiConsole.Ask<string> ("Escribe el título de la tarea:");
                    if (!string.IsNullOrWhiteSpace(title)) _repository.AddTask (title);
                    break;

                case "Cambiar Estado (Completar/Pendiente)":
                    PromptToggleTask();
                    break;

                case "Renombrar Tarea":
                    PromptRenameTask();
                    break;

                case "Eliminar Tarea":
                    PromptDeleteTask();
                    break;

                case "Salir":
                    keepRunning = false;
                    AnsiConsole.MarkupLine ("[bold green] ¡¡¡¡ Hasta luego !!!![/]");
                    break;
            }
        }
    }

    private void ShowTasksTable()
    {
        var tasks = _repository.GetAllTasks();

        if (tasks.Count == 0)
        {
            AnsiConsole.MarkupLine ("[grey]¡¡¡¡¡ No hay tareas registradas !!!!!.[/]");
            return;
        }

        var table = new Table().Border (TableBorder.Rounded);
        table.AddColumn ("[bold]ID[/]");
        table.AddColumn ("[bold]Tarea[/]");
        table.AddColumn ("[bold]Estado[/]");

        foreach (var task in tasks)
        {
            string status = task.IsCompleted ? "[green]✔ Completada[/]" : "[red]⏳ Pendiente[/]";
            string titleStyle = task.IsCompleted ? $"[strike grey]{task.Title}[/]" : task.Title;
            table.AddRow (task.Id.ToString(), titleStyle, status);
        }

        AnsiConsole.Write (table);
    }

    private void PromptToggleTask()
    {
        var tasks = _repository.GetAllTasks();
        if (tasks.Count == 0) return;

        var prompt = new SelectionPrompt<TaskItem>()
              .Title ("Selecciona la tarea para cambiar su estado:")
              .UseConverter (t => Markup.Escape($"[{t.Id}] {t.Title} ({(t.IsCompleted ? "Completada" : "Pendiente")})"));

        prompt.AddChoices (tasks);
        var selectedTask = AnsiConsole.Prompt (prompt);
        _repository.ToggleTaskStatus (selectedTask.Id);
    }

    private void PromptDeleteTask()
    {
        var tasks = _repository.GetAllTasks();
        if (tasks.Count == 0) return;

        var prompt = new SelectionPrompt<TaskItem>()
            .Title ("Selecciona la tarea que deseas [red]ELIMINAR[/]:")
            .UseConverter (t => Markup.Escape($"[{t.Id}] {t.Title} ({(t.IsCompleted ? "Completada" : "Pendiente")})"));

        prompt.AddChoices (tasks);
        var selectedTask = AnsiConsole.Prompt (prompt);

        bool confirm = AnsiConsole.Confirm ($"¿¿¿ Estás seguro de que deseas eliminar la tarea: [yellow]\"{selectedTask.Title}\" ???[/]?");

        if (confirm)
        {
            _repository.DeleteTask (selectedTask.Id);
            AnsiConsole.MarkupLine ("[green]✔ Tarea eliminada correctamente.[/]");
        }
        else
            AnsiConsole.MarkupLine("[grey]Operación cancelada.[/]");
        

        Thread.Sleep (3000);
    }

    private void PromptRenameTask()
    {
        var tasks = _repository.GetAllTasks();
        if (tasks.Count == 0) return;

        var prompt = new SelectionPrompt<TaskItem>()
            .Title ("Selecciona la tarea que deseas [blue]RENOMBRAR[/]:")
            .UseConverter (t => Markup.Escape($"[{t.Id}] {t.Title} ({(t.IsCompleted ? "Completada" : "Pendiente")})"));

        prompt.AddChoices (tasks);
        var selectedTask = AnsiConsole.Prompt (prompt);

        string newTitle = AnsiConsole.Ask<string> ($"Escribe el nuevo título para [yellow]\"{selectedTask.Title}\"[/]:");

        if (!string.IsNullOrWhiteSpace (newTitle))
        {
            _repository.UpdateTaskTitle (selectedTask.Id, newTitle);
            AnsiConsole.MarkupLine ("[green]✔ Tarea renombrada correctamente.[/]");
        }
        else
            AnsiConsole.MarkupLine("[red]El título no puede estar vacío.[/]");
        

        Thread.Sleep (3000);
    }
}