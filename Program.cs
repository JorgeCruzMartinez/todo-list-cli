using Todo_List;
class Program
{
    static async Task Main (string[] args)
    {
        ITaskRepository repository = new JsonTaskRepository();
        ConsoleUserInterface ui = new ConsoleUserInterface (repository);

        // Ejecución asíncrona del menú principal
        await ui.RunAsync();
    }
}