using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using OmdbWebApplication.Controllers;

namespace Tests.Projects.OmdbWebApplication.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController _controller;
        private Mock<IConfiguration> _configuration;
        private Mock<IMovieProvider> _movieProvider;
        
        [SetUp]
        public void Setup()
        {
            _configuration = new Mock<IConfiguration>();
            _configuration.Setup(c => c.GetSection(It.IsAny<string>())).Returns(new Mock<IConfigurationSection>().Object);
            _movieProvider = new Mock<IMovieProvider>();
            _controller = new HomeController(_configuration.Object, _movieProvider.Object);
        }

        [Test]
        public void Index_ReturnsIActionResult()
        {
            var result = _controller.Index();
            
            Assert.IsInstanceOf<IActionResult>(result);
        }
    }
}