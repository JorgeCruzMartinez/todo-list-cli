using Spectre.Console;


namespace Todo_List;

public class ConsoleUserInterface
{
    private readonly ITaskRepository _repository;

    public ConsoleUserInterface (ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task RunAsync()
    {
        bool keepRunning = true;
        while (keepRunning)
        {
            AnsiConsole.Clear();

            AnsiConsole.Write (
                new FigletText ("TODO List CLI")
                    .Centered()
                    .Color (Color.Blue));

            await ShowTasksTableAsync();

            var option = AnsiConsole.Prompt (
                new SelectionPrompt<string>()
                    .Title("\n[yellow]¿Qué deseas hacer?[/]")
                    .PageSize (5)
                    .AddChoices (new[] { "Agregar Tarea", "Cambiar Estado (Completar/Pendiente)", "Renombrar Tarea", "Eliminar Tarea", "Salir" }));

            switch (option)
            {
                case "Agregar Tarea":
                    var title = AnsiConsole.Ask<string> ("Escribe el título de la tarea:");
                    if (!string.IsNullOrWhiteSpace(title)) await _repository.AddTaskAsync (title);
                    break;

                case "Cambiar Estado (Completar/Pendiente)":
                    await PromptToggleTaskAsync();
                    break;

                case "Renombrar Tarea":
                    await PromptRenameTaskAsync();
                    break;

                case "Eliminar Tarea":
                    await PromptDeleteTaskAsync();
                    break;
                     
                case "Salir":
                    keepRunning = false;
                    AnsiConsole.MarkupLine ("[bold green] ¡¡¡¡ Hasta luego !!!![/]");
                    Thread.Sleep (3000);
                    break;
            }
        }
    }

    private async Task ShowTasksTableAsync()
    {
        var tasks = await _repository.GetAllTasksAsync();

        if (tasks.Count == 0)
        {
            AnsiConsole.MarkupLine("[grey]¡¡¡¡¡ No hay tareas registradas !!!!!.[/]");
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
            table.AddRow(task.Id.ToString(), titleStyle, status);
        }

        AnsiConsole.Write (table);
    }

    private async Task PromptToggleTaskAsync()
    {
        var tasks = await _repository.GetAllTasksAsync();
        if (tasks.Count == 0) return;

        var prompt = new SelectionPrompt<TaskItem>()
              .Title ("Selecciona la tarea para cambiar su estado:")
              .UseConverter (t => Markup.Escape($"[{t.Id}] {t.Title} ({(t.IsCompleted ? "Completada" : "Pendiente")})"));

        prompt.AddChoices (tasks);
        var selectedTask = AnsiConsole.Prompt (prompt);
        await _repository.ToggleTaskStatusAsync (selectedTask.Id);
    }

    private async Task PromptDeleteTaskAsync()
    {
        var tasks = await _repository.GetAllTasksAsync();
        if (tasks.Count == 0) return;

        var prompt = new SelectionPrompt<TaskItem>()
            .Title("Selecciona la tarea que deseas [red]ELIMINAR[/]:")
            .UseConverter(t => Markup.Escape($"[{t.Id}] {t.Title} ({(t.IsCompleted ? "Completada" : "Pendiente")})"));

        prompt.AddChoices(tasks);
        var selectedTask = AnsiConsole.Prompt(prompt);

        bool confirm = AnsiConsole.Confirm($"¿¿¿ Estás seguro de que deseas eliminar la tarea: [yellow]\"{selectedTask.Title}\" ???[/]???");

        if (confirm)
        {
            await _repository.DeleteTaskAsync (selectedTask.Id);
            AnsiConsole.MarkupLine ("[green]✔ Tarea eliminada correctamente.[/]");
        }
        else
            AnsiConsole.MarkupLine("[grey]Operación cancelada.[/]");
        

        Thread.Sleep (2000);
    }

    private async Task PromptRenameTaskAsync()
    {
        var tasks = await _repository.GetAllTasksAsync();
        if (tasks.Count == 0) return;

        var prompt = new SelectionPrompt<TaskItem>()
            .Title ("Selecciona la tarea que deseas [blue]RENOMBRAR[/]:")
            .UseConverter (t => Markup.Escape($"[{t.Id}] {t.Title} ({(t.IsCompleted ? "Completada" : "Pendiente")})"));

        prompt.AddChoices(tasks);
        var selectedTask = AnsiConsole.Prompt (prompt);

        var newTitle = AnsiConsole.Ask<string> ($"Escribe el nuevo título para [yellow]\"{selectedTask.Title}\"[/]:");

        if (!string.IsNullOrWhiteSpace (newTitle))
        {
            await _repository.UpdateTaskTitleAsync (selectedTask.Id, newTitle);
            AnsiConsole.MarkupLine ("[green]✔ Tarea renombrada correctamente.[/]");
        }
        else        
            AnsiConsole.MarkupLine("[red]El título no puede estar vacío.[/]");
        

        Thread.Sleep(2000);
    }
}