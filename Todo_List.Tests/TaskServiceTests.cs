
using Todo_List;
using Todo_List.Tests.Fakes;
using Xunit;

namespace Todo_List.Tests;

public class TaskServiceTests
{
    [Fact]
    public async Task AddTaskAsync_EmptyTitle_ThrowsArgumentException()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService (repository);

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => service.AddTaskAsync(""));

        // Assert
        Assert.IsType<ArgumentException>(exception);
    }


    [Fact]
    public async Task AddTaskAsync_ValidTitle_AddsTaskWithTrimmedTitle()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService (repository);

        // Act
        await service.AddTaskAsync ("  Comprar leche  ");

        // Assert
        Assert.Single (repository.Tasks);
        Assert.Equal ("Comprar leche", repository.Tasks[0].Title);
    }


    [Fact]
    public async Task GetAllTasksAsync_ReturnsAllTasks()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService (repository);

        await repository.AddTaskAsync ("Comprar leche");
        await repository.AddTaskAsync ("Estudiar C#");

        // Act
        var tasks = await service.GetAllTasksAsync();

        // Assert
        Assert.Equal (2, tasks.Count);
        Assert.Equal ("Comprar leche", tasks[0].Title);
        Assert.Equal ("Estudiar C#", tasks[1].Title);
    }

    [Fact]
    public async Task ToggleTaskStatusAsync_ExistingTask_ChangesStatus()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService(repository);

        await repository.AddTaskAsync ("Comprar leche");

        // Act
        await service.ToggleTaskStatusAsync(1);

        // Assert
        Assert.True (repository.Tasks[0].IsCompleted);
    }

    [Fact]
    public async Task DeleteTaskAsync_ExistingTask_RemovesTask()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService (repository);

        await repository.AddTaskAsync ("Comprar leche");

        // Act
        await service.DeleteTaskAsync(1);

        // Assert
        Assert.Empty(repository.Tasks);
    }

    [Fact]
    public async Task UpdateTaskTitleAsync_ValidTitle_UpdatesTaskTitle()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService(repository);

        await repository.AddTaskAsync ("Comprar leche");

        // Act
        await service.UpdateTaskTitleAsync (1, "Comprar pan");

        // Assert
        Assert.Equal ("Comprar pan", repository.Tasks[0].Title);
    }

    [Fact]
    public async Task UpdateTaskTitleAsync_EmptyTitle_ThrowsArgumentException()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService (repository);

        await repository.AddTaskAsync("Comprar leche");
         
        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => service.UpdateTaskTitleAsync (1, ""));

        // Assert
        Assert.IsType<ArgumentException>(exception);
        Assert.Equal ("Comprar leche", repository.Tasks[0].Title);
    }


    [Fact]
    public async Task AddTaskAsync_WhitespaceTitle_ThrowsArgumentException()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService (repository);

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => service.AddTaskAsync ("   "));

        // Assert
        Assert.IsType<ArgumentException>(exception);
        Assert.Empty(repository.Tasks);
    }

    [Fact]
    public async Task UpdateTaskTitleAsync_WhitespaceTitle_ThrowsArgumentException()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService (repository);

        await repository.AddTaskAsync ("Comprar leche");

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => service.UpdateTaskTitleAsync (1, "   "));

        // Assert
        Assert.IsType<ArgumentException>(exception);
        Assert.Equal ("Comprar leche", repository.Tasks[0].Title);
    }

    [Fact]
    public async Task ToggleTaskStatusAsync_Twice_ReturnsToPending()
    {
        // Arrange
        var repository = new FakeTaskRepository();
        var service = new TaskService (repository);

        await repository.AddTaskAsync( "Comprar leche");

        // Act
        await service.ToggleTaskStatusAsync (1);
        await service.ToggleTaskStatusAsync (1);

        // Assert
        Assert.False (repository.Tasks[0].IsCompleted);
    }
}