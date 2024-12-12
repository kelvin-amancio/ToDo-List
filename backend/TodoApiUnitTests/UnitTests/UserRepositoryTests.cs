using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Context;
using ToDoApi.Repositories;
using ToDoApi.ViewModels;

namespace TodoApiUnitTests.UnitTests
{
    public class UserRepositoryTests
    {
        private readonly AppDbContext _dbContext;
        private readonly UserRepository _userRepository;
        private readonly Faker _faker = new("pt_BR");

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TodoAppDb")
                .Options;

            _dbContext = new AppDbContext(options);
            _userRepository = new UserRepository(_dbContext);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task RegistrarAsync_ShouldAddUser_WhenUserDoesNotExist()
        {
            var registerUserViewModel = new RegisterUserViewModel
            {
                UserName = _faker.Person.Email,
                Password = "TodoApp!@#1sz",
                ConfirmPassword = "TodoApp!@#1sz"
            };

            var result = await _userRepository.RegisterAsync(registerUserViewModel);

            result.Should().NotBeNull();
            result.Data.Should().NotBeNull();
            result.Data.UserName.Should().Be(_faker.Person.Email);
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public async Task RegistrarAsync_ShouldReturnError_WhenUserAlreadyExists()
        {
            var registerUserViewModel = new RegisterUserViewModel
            {
                UserName = _faker.Person.Email,
                Password = "TodoApp!@#1sz",
                ConfirmPassword = "TodoApp!@#1sz"
            };

            await _userRepository.RegisterAsync(registerUserViewModel);

            var result = await _userRepository.RegisterAsync(registerUserViewModel);

            result.Should().NotBeNull();
            result.Errors.Should().NotBeEmpty();
            result.Errors.Should().Contain($"O usuário {registerUserViewModel.UserName} já existe!");
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnUser_WhenCredentialsAreValid()
        {
            var registerUserViewModel = new RegisterUserViewModel
            {
                UserName = _faker.Person.Email,
                Password = "TodoApp!@#1sz",
                ConfirmPassword = "TodoApp!@#1sz"
            };

            await _userRepository.RegisterAsync(registerUserViewModel);

            var userViewModel = new UserViewModel
            {
                UserName = _faker.Person.Email,
                Password = "TodoApp!@#1sz"
            };

            var result = await _userRepository.LoginAsync(userViewModel);

            result.Should().NotBeNull();
            result.Data.Should().NotBeNull();
            result.Data.UserName.Should().Be(_faker.Person.Email);
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnError_WhenCredentialsAreInvalid()
        {
            var registerUserViewModel = new RegisterUserViewModel
            {
                UserName = _faker.Person.Email,
                Password = "TodoApp!@#1sz",
                ConfirmPassword = "TodoApp!@#1sz"
            };

            await _userRepository.RegisterAsync(registerUserViewModel);

            var userViewModel = new UserViewModel
            {
                UserName = _faker.Person.Email,
                Password = "Todo!@#1sz"
            };

            var result = await _userRepository.LoginAsync(userViewModel);

            result.Should().NotBeNull();
            result.Errors.Should().NotBeEmpty();
            result.Errors.Should().Contain("Nome de usuário ou senha incorretos!");
        }
    }
}