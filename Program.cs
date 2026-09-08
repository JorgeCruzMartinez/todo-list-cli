using Spectre.Console;
using System.Runtime.InteropServices;
using Todo_List;

class Program
{
    private static readonly TaskManager _manager = new();

    static void Main(string[] args)
    {
        bool keepRunning = true;
        while (keepRunning)
        {
            AnsiConsole.Clear();

            // Título principal con estilo
            AnsiConsole.Write(
                new FigletText("TODO List CLI")
                    .Centered()
                    .Color(Color.Blue));

            // Mostrar las tareas actuales en una tabla estilizada
            ShowTasksTable();

            // Menú de selección interactivo con las flechas del teclado
            // string option = AnsiConsole.Prompt(
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title ("\n[yellow]¿Qué deseas hacer?[/]")
                    .PageSize (5)
                    .AddChoices (new[] { "Agregar Tarea", "Cambiar Estado (Completar/Pendiente)", "Eliminar Tarea",  "Salir" }));

            switch (option)
            {
                case "Agregar Tarea":
                    var title = AnsiConsole.Ask<string>("Escribe el título de la tarea:");
                    _manager.AddTask(title);
                    break;

                case "Cambiar Estado (Completar/Pendiente)":
                    PromptToggleTask();
                    break;

                case "Eliminar Tarea":
                    PromptDeleteTask();
                    break;

                case "Salir":
                    keepRunning = false;
                    AnsiConsole.MarkupLine("[bold green] ¡¡¡¡ Hasta luego !!!![/]");
                    break;
            }
        }
    }

    private static void ShowTasksTable()
    {
        var tasks = _manager.GetAllTasks();

        if (!tasks.Any())
        {
            AnsiConsole.MarkupLine("[grey]No hay tareas registradas.[/]");
            return;
        }

        var table = new Table().Border(TableBorder.Rounded);
        table.AddColumn("[bold]ID[/]");
        table.AddColumn("[bold]Tarea[/]");
        table.AddColumn("[bold]Estado[/]");

        foreach (var task in tasks)
        {
            string status = task.IsCompleted
                ? "[green]✔ Completada[/]"
                : "[red]⏳ Pendiente[/]";

            // Esta combinación "strike grey" genera el conflicto con ciertos textos
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

        // Se utiliza Markup.Escape para evitar conflictos con los corchetes del ID en Spectre.Console
        var prompt = new SelectionPrompt<TaskItem>()
              .Title("Selecciona la tarea para cambiar su estado:")
              .UseConverter (t => Markup.Escape($"[{t.Id}] {t.Title} ({(t.IsCompleted ? "Completada" : "Pendiente")})"));

        prompt.AddChoices(tasks);

        var selectedTask = AnsiConsole.Prompt (prompt);
        _manager.ToggleTaskStatus (selectedTask.Id);
    }

    private static void PromptDeleteTask()
    {
        var tasks = _manager.GetAllTasks();
        if (!tasks.Any())
        {
            AnsiConsole.MarkupLine ("[grey]¡¡¡¡¡ No hay tareas registradas para eliminar !!!!!.[/]");
            Console.ReadKey();
            return;
        }

        // Reutilizamos la lógica de selección interactiva basada en el objeto TaskItem
        var prompt = new SelectionPrompt<TaskItem>()
            .Title ("Selecciona la tarea que deseas [red]ELIMINAR[/]:");

        // Aplicamos Markup.Escape para renderizar de forma segura los corchetes del ID
        prompt.UseConverter (t => Markup.Escape($"[{t.Id}] {t.Title} ({(t.IsCompleted ? "Completada" : "Pendiente")})"));
        prompt.AddChoices(tasks);

        var selectedTask = AnsiConsole.Prompt (prompt);

        // Ventana de confirmación interactiva (S/N) para evitar accidentes
        bool confirm = AnsiConsole.Confirm ($"¿¿¿ Estás seguro de que deseas eliminar la tarea: [yellow]\"{selectedTask.Title} ???\"[/]?");

        if (confirm)
        {
            _manager.DeleteTask (selectedTask.Id);
            AnsiConsole.MarkupLine ("[green]✔ Tarea eliminada correctamente.[/]");
        }
        else
            AnsiConsole.MarkupLine("[grey]Operación cancelada.[/]");

        System.Threading.Thread.Sleep (3000);
    }
}