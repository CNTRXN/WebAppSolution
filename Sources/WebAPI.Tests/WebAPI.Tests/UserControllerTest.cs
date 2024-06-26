using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.DataContext.Models;
using WebAPI.Services;

namespace WebAPI.Tests
{
    [TestFixture]
    public class GetUsersTests
    {
        private Fixture _fixture;
        private Mock<IUserService> _userRepoMock;
        private UserController _controller;

        public GetUsersTests() 
        {
            _fixture = new Fixture();
            _userRepoMock = new Mock<IUserService>();
        }

        [Test]
        public async Task GetUser_Return_OK() 
        {
            var usersList = _fixture.CreateMany<User>(4);

            _userRepoMock.Setup(repo => repo.GetUsers().Result).Returns(usersList);

            _controller = new UserController(_userRepoMock.Object);

            var result = await _controller.GetAllUsers();
            var obj = result as ObjectResult;

            Assert.That(obj.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GetUser_Return_BadRequest()
        {
            var user = _fixture.Create<User>();

            int userId = 4;

            _userRepoMock.Setup(repo => repo.GetUser(3).Result).Returns(user);

            _controller = new UserController(_userRepoMock.Object);

            var result = await _controller.GetUserById(userId);
            var obj = result as ObjectResult;

            Assert.That(obj.StatusCode, Is.EqualTo(400));
        }
    }
}
