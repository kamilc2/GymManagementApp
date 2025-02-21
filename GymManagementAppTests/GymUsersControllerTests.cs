using GymManagementApp.Controllers;
using GymManagementApp.Models;
using GymManagementApp.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymManagementAppTests.Controllers
{
    [TestFixture]
    public class GymUsersControllerTests
    {
        private Mock<IGymUserService> _mockService;
        private GymUsersController _controller;
        private List<GymUser> _users;

        [SetUp]
        public void Setup()
        {
            // Tworzymy przykładową listę użytkowników
            _users = new List<GymUser>
            {
                new GymUser { Id = 1, Name = "Kamil", Email = "kamil@example.com", MembershipId = 1 },
                new GymUser { Id = 2, Name = "Ewelina", Email = "ewelina@example.com", MembershipId = 2 }
            };

            // Tworzymy Mock serwisu
            _mockService = new Mock<IGymUserService>();

            // Tworzymy instancję kontrolera z mockowanym serwisem
            _controller = new GymUsersController(_mockService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            if (_controller != null)
            {
                _controller.Dispose();  // ✅ Usuwamy obiekt po teście
            }
        }

        [Test]
        public async Task Index_ReturnsViewWithUsers()
        {
            // Arrange
            _mockService.Setup(s => s.FindAll()).ReturnsAsync(_users);

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOf<List<GymUser>>(result.Model);
            Assert.AreEqual(2, ((List<GymUser>)result.Model).Count);
        }

        [Test]
        public async Task Create_ValidUser_RedirectsToIndex()
        {
            // Arrange
            var newUser = new GymUser { Id = 3, Name = "Nowy", Email = "nowy@example.com", MembershipId = 1 };
            _mockService.Setup(s => s.Add(newUser)).ReturnsAsync(3);

            // Act
            var result = await _controller.Create(newUser) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [Test]
        public async Task Create_InvalidModel_ReturnsView()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Required");

            var newUser = new GymUser { Id = 3, Email = "nowy@example.com", MembershipId = 1 };

            // Act
            var result = await _controller.Create(newUser) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task Delete_ValidId_RedirectsToIndex()
        {
            // Arrange
            _mockService.Setup(s => s.Delete(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(1) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }
    }
}
