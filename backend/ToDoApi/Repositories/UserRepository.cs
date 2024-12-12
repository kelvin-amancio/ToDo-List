using Microsoft.EntityFrameworkCore;
using ToDoApi.Context;
using ToDoApi.Models;
using ToDoApi.Repositories.Interfaces;
using ToDoApi.ViewModels;

namespace ToDoApi.Repositories
{
    public class UserRepository(AppDbContext appDbContext) : IUserRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<ResultViewModel<User>> RegisterAsync(RegisterUserViewModel registerUserViewModel)
        {
            var hasUser = await GetUserByUserNameAsync(registerUserViewModel.UserName);
            
            if(hasUser is not null)
                return new ResultViewModel<User>($"O usuário {registerUserViewModel.UserName} já existe!");

            var user = new User(registerUserViewModel.UserName, registerUserViewModel.Password);

            await _appDbContext.User.AddAsync(user);
            await _appDbContext.SaveChangesAsync();

            return new ResultViewModel<User>(user);
        }

        public async Task<ResultViewModel<User>> LoginAsync(UserViewModel userViewModel)
        {
            var user = await GetUserByUserNameAndPasswordAsync(userViewModel.UserName, userViewModel.Password);

            if (user is null)
                return new ResultViewModel<User>("Nome de usuário ou senha incorretos!");

            return new ResultViewModel<User>(user);
        }

        private async Task<User?> GetUserByUserNameAsync(string userName)
        {
            var user = await _appDbContext.User.FirstOrDefaultAsync(x => x.UserName == userName);
            return user;
        }

        private async Task<User?> GetUserByUserNameAndPasswordAsync(string userName, string password)
        {
            var user = await _appDbContext.User.FirstOrDefaultAsync(x => x.UserName == userName);

            if (user is null || !User.VerifyPasswordHash(password, user.Password))
                return null;

            return user;
        }
    }
}
