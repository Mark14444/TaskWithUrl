using FluentAssertions;
using FluentAssertions.Execution;
using Fragments.Test.Base;
using Inforce.Domain.Entities;
using Inforce.Service.Dto.AuthorizationDtos;
using Inforce.Service.Dto.UserDtos;
using Inforce.Service.Services.Interfaces;
using Inforce.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Fragments.Test.Controllers
{
    public class UserControllerTest : Base.BaseFixture
    {
        private readonly Mock<IUserService> userService;
        private readonly UserController userController;

        public UserControllerTest()
        {
            userService = new Mock<IUserService>();
            userController = new UserController(userService.Object);
        }
        [Theory]
        [AutoEntityData]
        public async Task GetUserById_WhenUserExists_ReturnsOkObjectResult(UserDto user)
        {
            // Arrange
            userService.Setup(service => service.GetByIdAsync(user.Id)).ReturnsAsync(user);

            // Act
            var result = await userController.GetUserById(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as ObjectResult)?.Value.Should().Be(user);
            }
        }
        [Theory]
        [AutoEntityData]
        public async Task Login_WhenUserExists_ReturnsOkObjectResult(AuthRequestDto user)
        {
            // Arrange
            userService.Setup(service => service.LoginAsync(user)).ReturnsAsync(new AuthResponseDto(new User { Email = user.Email }, "token"));

            // Act
            var result = await userController.Login(user);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<AuthResponseDto>();
                ((result as OkObjectResult)?.Value as AuthResponseDto)?.Email.Should().Be(user.Email);
            }
        }
        [Theory]
        [AutoEntityData]
        public async Task Login_WhenWrongEmailOrPassword_BadRequestObjectResult(AuthRequestDto user)
        {
            // Arrange
            AuthResponseDto? response = null!;
            userService.Setup(service => service.LoginAsync(user)).ReturnsAsync(response);

            // Act
            var result = await userController.Login(user);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
        [Theory]
        [AutoEntityData]
        public async Task RegiserUser_WhenUserIsValid(CreateUserDto user)
        {
            // Arrange       

            // Act
            var result = await userController.RegiserUser(user);

            // Assert
            userService.Verify(service => service.AddAsync(user));

        }
        [Theory]
        [AutoEntityData]
        public async Task GetMe_WhenUserExists_ReturnsOkObjectResult(UserDto user)
        {
            // Arrange
            userService.Setup(service => service.GetMeAsync()).ReturnsAsync(user);

            // Act
            var result = await userController.GetMe();

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<UserDto>();
                ((result as OkObjectResult)?.Value as UserDto)?.Id.Should().Be(user.Id);
            }
        }
        [Theory]
        [AutoEntityData]
        public async Task RemoveUser_WhenUserExists_ReturnsOkResult(int id)
        {
            // Arrange
            userService.Setup(service => service.RemoveAsync(id)).ReturnsAsync(true);

            // Act
            var result = await userController.RemoveUser(id);

            // Assert
            result.Should().BeOfType<OkResult>();
        }
        [Theory]
        [AutoEntityData]
        public async Task RemoveUser_WhenUserIsNotExist_ReturnsBadRequet(int id)
        {
            // Arrange
            userService.Setup(service => service.RemoveAsync(id)).ReturnsAsync(false);

            // Act
            var result = await userController.RemoveUser(id);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateUser_WhenUserExists_ReturnsOkResult(UserDto entity)
        {
            // Arrange
            userService.Setup(service => service.UpdateAsync(entity)).ReturnsAsync(true);

            // Act
            var result = await userController.UpdateUser(entity);

            // Assert
            result.Should().BeOfType<OkResult>();
        }
        [Theory]
        [AutoEntityData]
        public async Task UpdateUser_WhenUserIsNotExist_ReturnsBadRequet(UserDto entity)
        {
            // Arrange
            userService.Setup(service => service.UpdateAsync(entity)).ReturnsAsync(false);

            // Act
            var result = await userController.UpdateUser(entity);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }
        [Theory]
        [AutoEntityData]
        public async Task GetAllUsers_ReturnsOkObjectResult(UserDto user)
        {
            // Arrange
            List<UserDto> users = new List<UserDto>() { user };
            userService.Setup(service => service.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await userController.GetAllUsers();

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as ObjectResult)?.Value.Should().Be(users);
            }
        }
    }
}