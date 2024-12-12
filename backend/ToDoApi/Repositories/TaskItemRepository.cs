using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoApi.Context;
using ToDoApi.Models;
using ToDoApi.Repositories.Interfaces;
using ToDoApi.Services;
using ToDoApi.ViewModels;

namespace ToDoApi.Repositories
{
    public class TaskItemRepository(AppDbContext appDbContext, ITokenService tokenService) : ITaskItemRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<PagedResultViewModel<TaskItem>> GetAllAsync(int pageNumber, int pageSize)
        {
            var userId = _tokenService.GetUserId();

            var totalTasks = await _appDbContext.TaskItem
                                                .Where(x => x.UserId == userId)
                                                .CountAsync();

            var tasks = await _appDbContext.TaskItem
                                           .Where(x => x.UserId == userId)
                                           .OrderBy(x => x.Title)  
                                           .Skip((pageNumber - 1) * pageSize)
                                           .Take(pageSize)  
                                           .AsNoTracking()
                                           .ToListAsync();

            return new PagedResultViewModel<TaskItem>(tasks, totalTasks, pageNumber, pageSize);
        }

        public async Task<ResultViewModel<TaskItem>> AddAsync(TaskItemViewModel taskItemViewModel)
        {
            var userId = _tokenService.GetUserId();

            var taskItem = new TaskItem(taskItemViewModel.Title, taskItemViewModel.Description, taskItemViewModel.Completed, userId);

            await _appDbContext.TaskItem.AddAsync(taskItem);
            await _appDbContext.SaveChangesAsync();

            return new ResultViewModel<TaskItem>(taskItem);
        }

        public async Task<ResultViewModel<TaskItem>> UpdateAsync(TaskItemViewModel taskItemViewModel)
        {
            var taskItem = await _appDbContext.TaskItem.FirstOrDefaultAsync(x => x.Id == taskItemViewModel.Id);

            if (taskItem is null)
                return new ResultViewModel<TaskItem>($"Tarefa {taskItemViewModel.Id} não encontrada.");

            taskItem.BuilderTaskItem(taskItemViewModel.Title, taskItemViewModel.Description, taskItemViewModel.Completed);

            _appDbContext.TaskItem.Update(taskItem);
            await _appDbContext.SaveChangesAsync();

            return new ResultViewModel<TaskItem>(taskItem);
        }

        public async Task<ResultViewModel<TaskItem>> DeleteByIdAsync(string id)
        {
            var taskItem = await _appDbContext.TaskItem.FirstOrDefaultAsync(x => x.Id == id);

            if (taskItem is null)
                return new ResultViewModel<TaskItem>($"Tarefa com id {id} não encontrada.");

            _appDbContext.TaskItem.Remove(taskItem);
            await _appDbContext.SaveChangesAsync();

            return new ResultViewModel<TaskItem>(taskItem);
        }
    }
}
