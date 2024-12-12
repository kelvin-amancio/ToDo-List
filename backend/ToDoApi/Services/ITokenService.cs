using ToDoApi.Models;

namespace ToDoApi.Services
{
    public interface ITokenService
    {
       string Create(User user);
       string GetUserId();
    }
}
