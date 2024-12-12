using ToDoApi.Models;
using ToDoApi.ViewModels;

namespace ToDoApi.Repositories.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<PagedResultViewModel<TaskItem>> GetAllAsync(int pageNumber, int pageSize);
        Task<ResultViewModel<TaskItem>> AddAsync(TaskItemViewModel taskItemViewModel);
        Task<ResultViewModel<TaskItem>> UpdateAsync(TaskItemViewModel taskItemViewModel);
        Task<ResultViewModel<TaskItem>> DeleteByIdAsync(string id);
    }
}
