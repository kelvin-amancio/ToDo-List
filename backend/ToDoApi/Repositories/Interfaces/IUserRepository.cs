using ToDoApi.Models;
using ToDoApi.ViewModels;

namespace ToDoApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ResultViewModel<User>> RegisterAsync(RegisterUserViewModel registerUserViewModel);
        Task<ResultViewModel<User>> LoginAsync(UserViewModel userViewModel);
    }
}
