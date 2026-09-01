using Todo_List;
using Spectre.Console;

class Program
{
    private static readonly TaskManager _manager = new();

    static void Main(string[] args)
    {
        // Datos de prueba iniciales
        _manager.AddTask ("Configurar el repositorio de GitHub");
        _manager.AddTask ("Aprender a usar Spectre.Console");

        bool keepRunning = true;

        while (keepRunning)
        {
            AnsiConsole.Clear();

            // Título principal con estilo
            AnsiConsole.Write(
                new FigletText ("TODO List CLI")
                    .Centered()
                    .Color (Color.Blue));

            // Mostrar las tareas actuales en una tabla estilizada
            ShowTasksTable();

            // Menú de selección interactivo con las flechas del teclado
            // string option = AnsiConsole.Prompt(
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title ("\n[yellow]¿Qué deseas hacer?[/]")
                    .PageSize (5)
                    .AddChoices (new[] { "Agregar Tarea", "Cambiar Estado (Completar/Pendiente)", "Salir" }));

            switch (option)
            {
                case "Agregar Tarea":
                    var title = AnsiConsole.Ask<string> ("Escribe el título de la tarea:");
                    _manager.AddTask (title);
                    break;

                case "Cambiar Estado (Completar/Pendiente)":
                    PromptToggleTask();
                    break;

                case "Salir":
                    keepRunning = false;
                    AnsiConsole.MarkupLine ("[bold green] ¡¡¡¡ Hasta luego !!!![/]");
                    break;
            }
        }
    }

    private static void ShowTasksTable()
    {
        var tasks = _manager.GetAllTasks();

        if (!tasks.Any())
        {
            AnsiConsole.MarkupLine ("[grey] ¡¡¡¡ No hay tareas registradas !!!!.[/]");
            return;
        }

        var table = new Table().Border (TableBorder.Rounded);
        table.AddColumn ("[bold]ID[/]");
        table.AddColumn ("[bold]Tarea[/]");
        table.AddColumn ("[bold]Estado[/]");

        foreach (var task in tasks)
        {
            string status = task.IsCompleted
                ? "[green]✔ Completada[/]"
                : "[red]⏳ Pendiente[/]";

            string titleStyle = task.IsCompleted
                ? $"[strike grey]{task.Title}[/]"
                : task.Title;

            table.AddRow(task.Id.ToString(), titleStyle, status);
        }

        AnsiConsole.Write(table);
    }

    private static void PromptToggleTask()
    {
        var tasks = _manager.GetAllTasks();
        if (!tasks.Any()) return;

        // Crea un menú interactivo para seleccionar a qué tarea cambiarle su estado solo que en lugar de usar el tipo string ocupa objetos completos de tipo TaskItem.
        var prompt = new SelectionPrompt<TaskItem>()
            .Title ("Selecciona la tarea para cambiar su estado:")
            .UseConverter (t => $"[{t.Id}] {t.Title} ({(t.IsCompleted ? "Completada" : "Pendiente")})");

        prompt.AddChoices(tasks);

        var selectedTask = AnsiConsole.Prompt (prompt);
        _manager.ToggleTaskStatus (selectedTask.Id);
    }
}