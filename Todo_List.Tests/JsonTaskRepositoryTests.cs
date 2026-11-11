using FluentAssertions;

namespace Todo_List.Tests;
public class JsonTaskRepositoryTests : IDisposable
{
    private readonly string _testFilePath;

    public JsonTaskRepositoryTests()
    {
        // Usamos un archivo JSON temporal único para cada ejecución de pruebas
        _testFilePath = $"test_tasks_{Guid.NewGuid()}.json";
    }

    // El método Dispose limpia el entorno borrando el archivo temporal al terminar cada test
    public void Dispose()
    {
        if (File.Exists(_testFilePath))        
            File.Delete(_testFilePath);
    }

    // Happy path tests
    [Fact]
    public async Task AddTaskAsync_ShouldAddNewTaskAndSaveToFile()
    {
        // Arrange (Preparar)
        var repository = new JsonTaskRepository (_testFilePath);
        string taskTitle = "Aprender Unit Testing en C#";

        // Act (Actuar)
        await repository.AddTaskAsync (taskTitle);

        // Assert (Afirmar)
        var tasks = await repository.GetAllTasksAsync();
        tasks.Should().ContainSingle();
        tasks.First().Title.Should().Be (taskTitle);
        tasks.First().IsCompleted.Should().BeFalse();
        File.Exists (_testFilePath).Should().BeTrue(); // Verifica que el JSON se creó físicamente
    }

    [Fact]
    public async Task ToggleTaskStatusAsync_ShouldInvertCompletionStatus()
    {
        // Arrange
        var repository = new JsonTaskRepository (_testFilePath);
        await repository.AddTaskAsync ("Tarea de prueba");
        var tasks = await repository.GetAllTasksAsync();
        int taskId = tasks.First().Id;

        // Act
        await repository.ToggleTaskStatusAsync (taskId);

        // Assert
        var updatedTasks = await repository.GetAllTasksAsync();
        updatedTasks.First().IsCompleted.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteTaskAsync_ShouldRemoveTaskFromList()
    {
        // Arrange
        var repository = new JsonTaskRepository (_testFilePath);
        await repository.AddTaskAsync ("Tarea a eliminar");
        var tasks = await repository.GetAllTasksAsync();
        int taskId = tasks.First().Id;

        // Act
        await repository.DeleteTaskAsync (taskId);

        // Assert
        var updatedTasks = await repository.GetAllTasksAsync();
        updatedTasks.Should().BeEmpty();
    }

    // Sad path tests
    [Fact]
    public async Task ToggleTaskStatusAsync_WithNonExistentId_ShouldNotThrowExceptionAndChangeNothing()
    {
        // Arrange (Camino infeliz: el repositorio está vacío y buscamos un ID que no existe)
        var repository = new JsonTaskRepository(_testFilePath);
        int invalidId = 999;

        // Act
        Func<Task> act = async () => await repository.ToggleTaskStatusAsync(invalidId);

        // Assert
        // 1. Verificamos que el código no se rompa (no lance excepciones) ante un ID inválido
        await act.Should().NotThrowAsync();

        // 2. Verificamos que la lista siga completamente vacía
        var tasks = await repository.GetAllTasksAsync();
        tasks.Should().BeEmpty();
    }

    [Fact]
    public async Task DeleteTaskAsync_WithNonExistentId_ShouldNotThrowException()
    {
        // Arrange (Agregamos una tarea real, pero intentamos borrar una con ID diferente)
        var repository = new JsonTaskRepository(_testFilePath);
        await repository.AddTaskAsync("Tarea Real");
        int invalidId = -5; // ID negativo inválido

        // Act
        Func<Task> act = async () => await repository.DeleteTaskAsync(invalidId);

        // Assert
        await act.Should().NotThrowAsync();

        // Verificamos que la tarea real siga intacta y no se haya borrado nada por accidente
        var tasks = await repository.GetAllTasksAsync();
        tasks.Should().ContainSingle();
        tasks.First().Title.Should().Be("Tarea Real");
    }

    [Fact]
    public async Task UpdateTaskTitleAsync_WithNonExistentId_ShouldNotModifyExistingTasks()
    {
        // Arrange
        var repository = new JsonTaskRepository(_testFilePath);
        await repository.AddTaskAsync("Título Original");
        int invalidId = 42;

        // Act
        await repository.UpdateTaskTitleAsync(invalidId, "Nuevo Título Intruso");

        // Assert
        var tasks = await repository.GetAllTasksAsync();
        // La tarea original no debió cambiar su título porque el ID no coincidía
        tasks.First().Title.Should().Be("Título Original");
    }


}