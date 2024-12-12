using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using ToDoApi.Context;
using ToDoApi.Models;
using ToDoApi.Repositories;
using ToDoApi.Services;
using ToDoApi.ViewModels;

namespace TodoApiUnitTests.UnitTests
{
    public class TaskItemRepositoryTests
    {
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly AppDbContext _dbContext;
        private readonly TaskItemRepository _repository;

        public TaskItemRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TodoAppDb")
                .Options;

            _dbContext = new AppDbContext(options);

            _mockTokenService = new Mock<ITokenService>();
            _mockTokenService.Setup(ts => ts.GetUserId()).Returns("userId");

            _repository = new TaskItemRepository(_dbContext, _mockTokenService.Object);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnPagedResult_WhenTasksExist()
        {
            var taskItems = new List<TaskItem>
            {
                new TaskItem("Tarefa 1", "Adicionando Tarefa 1", false, "userId"),
                new TaskItem("Tarefa 1", "Adicionando Tarefa 2", false, "userId")
            };

            await _dbContext.TaskItem.AddRangeAsync(taskItems);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.GetAllAsync(1, 10);

            result.Should().NotBeNull();
            result.Tasks.Should().HaveCount(2);
            result.CurrentPage.Should().Be(1);
            result.TotalPages.Should().Be(1);
            result.PageSize.Should().Be(10);
        }

        [Fact]
        public async Task AddAsync_ShouldAddTaskItem_WhenValid()
        {
            var taskItemViewModel = new TaskItemViewModel
            {
                Title = "Tarefa 1",
                Description = "Adicionando Tarefa 1",
                Completed = false
            };

            var result = await _repository.AddAsync(taskItemViewModel);

            result.Should().NotBeNull();
            result.Data.Should().NotBeNull();
            result.Data.Title.Should().Be("Tarefa 1");
            result.Data.Description.Should().Be("Adicionando Tarefa 1");
            result.Data.Completed.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateTaskItem_WhenTaskExists()
        {
            var taskItem = new TaskItem("Tarefa 1", "Adicionando Tarefa 1", false, "userId");
            await _dbContext.TaskItem.AddAsync(taskItem);
            await _dbContext.SaveChangesAsync();

            var taskItemViewModel = new TaskItemViewModel
            {
                Id = taskItem.Id,
                Title = "Tarefa 2",
                Description = "Adicionando Tarefa 2",
                Completed = true
            };

            var result = await _repository.UpdateAsync(taskItemViewModel);

            result.Should().NotBeNull();
            result.Data.Should().NotBeNull();
            result.Data.Title.Should().Be("Tarefa 2");
            result.Data.Description.Should().Be("Adicionando Tarefa 2");
            result.Data.Completed.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldDeleteTaskItem_WhenTaskExists()
        {
            var taskItem = new TaskItem("Tarefa 1", "Adicionando Tarefa 1", false, "userId");
            await _dbContext.TaskItem.AddAsync(taskItem);
            await _dbContext.SaveChangesAsync();

            var result = await _repository.DeleteByIdAsync(taskItem.Id);

            result.Should().NotBeNull();
            result.Data.Should().NotBeNull();
            result.Data.Title.Should().Be("Tarefa 1");

            var taskInDb = await _dbContext.TaskItem.FindAsync(taskItem.Id);
            taskInDb.Should().BeNull();
        }
    }
}