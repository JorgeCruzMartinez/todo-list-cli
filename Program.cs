using Todo_List;
class Program
{
    static void Main (string[] args)
    {
        // 1. Instanciamos el almacén de datos (Abierto/Cerrado)
        ITaskRepository repository = new JsonTaskRepository();

        // 2. Inyectamos el almacén en la UI (Inversión de Dependencias)
        ConsoleUserInterface ui = new (repository);

        // 3. Arrancamos la aplicación
        ui.Run();
    }
}