using FluentAssertions;
using FluentAssertions.Execution;
using Fragments.Test.Base;
using Inforce.Domain.Entities;
using Inforce.Service.Dto.AuthorizationDtos;
using Inforce.Service.Dto.UrlDtos;
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
    public class UrlControllerTest : Base.BaseFixture
    {
        private readonly Mock<IUrlService> urlService;
        private readonly UrlController urlController;

        public UrlControllerTest()
        {
            urlService = new Mock<IUrlService>();
            urlController = new UrlController(urlService.Object);
        }
        [Theory]
        [AutoEntityData]
        public async Task GetUrlById_WhenUrlExists_ReturnsOkObjectResult(UrlDto url)
        {
            // Arrange
            urlService.Setup(service => service.GetUrlByIdAsync(url.Id)).ReturnsAsync(url);

            // Act
            var result = await urlController.GetUrlById(url.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as ObjectResult)?.Value.Should().Be(url);
            }
        }
        [Theory]
        [AutoEntityData]
        public async Task CreateShortUrl_WhenUrlIsValid(CreateShortUrlDto url)
        {
            // Arrange       

            // Act
            var result = await urlController.CreateShortUrl(url);

            // Assert
            urlService.Verify(service => service.AddAsync(url));

        }
       
        [Theory]
        [AutoEntityData]
        public async Task RemoveUrl_WhenUrlExists_ReturnsOkResult(int id)
        {
            // Arrange
            urlService.Setup(service => service.DeleteAsync(id)).ReturnsAsync(true);

            // Act
            var result = await urlController.DeleteUrl(id);

            // Assert
            result.Should().BeOfType<OkResult>();
        }
        [Theory]
        [AutoEntityData]
        public async Task RemoveUrl_WhenUrlIsNotExist_ReturnsBadRequesResult(int id)
        {
            // Arrange
            urlService.Setup(service => service.DeleteAsync(id)).ReturnsAsync(false);

            // Act
            var result = await urlController.DeleteUrl(id);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetAllUrls_ReturnsOkObjectResult(UrlDto url)
        {
            // Arrange
            List<UrlDto> urls = new List<UrlDto>() { url };
            urlService.Setup(service => service.GetAllUrlsAsync()).ReturnsAsync(urls);

            // Act
            var result = await urlController.GetAllUrls();

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as ObjectResult)?.Value.Should().Be(urls);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUrlByShortUrl_WhenUrlExists_ReturnsOkObjectResult(UrlDto url)
        {
            // Arrange
            urlService.Setup(service => service.GetUrlByShortName(url.ShortUrl)).ReturnsAsync(url);

            // Act
            var result = await urlController.GetUrlByShortUrl(url.ShortUrl);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as ObjectResult)?.Value.Should().Be(url);
            }
        }
    }
}
