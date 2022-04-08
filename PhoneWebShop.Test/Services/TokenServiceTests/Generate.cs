using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using PhoneWebShop.Business.Services;
using PhoneWebShop.Domain.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace PhoneWebShop.Business.Test.Services.TokenServiceTests
{
    public class Generate
    {
        private readonly Mock<IOptions<JwtSettings>> _mockJwtOptions = new();
        private readonly TokenService _tokenService;

        public Generate()
        {
            _mockJwtOptions.SetupGet(x => x.Value).Returns(new JwtSettings
            {
                Audience = "https://localhost/audience",
                Issuer = "https://localhost/issuer",
                SignKey = "48d08363-a785-490c-9589-aa6530a490aa" // Random GUID
            });
            _tokenService = new TokenService(_mockJwtOptions.Object);
        }

        [Fact]
        public void Should_GenerateTokenWithCorrectClaims()
        {
            var claims = new List<Claim>
            {
                new Claim("Claim1", "Value1"),
                new Claim("Claim2", "Value2")
            };

            // Act
            var token = _tokenService.Generate(claims);

            // Assert
            var tokenHeader = token.Split('.')[0];
            tokenHeader.Should().Be("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9");
        }

        [Fact]
        public void Should_ThrowArgumentException_When_ClaimsIsNullOrEmpty()
        {
            // Arrange
            var claimsEmpty = new List<Claim>();
            List<Claim> claimsNull = null;

            // Act
            Action claimsEmptyAction = () => _tokenService.Generate(claimsEmpty);
            Action claimsNullAction = () => _tokenService.Generate(claimsNull);

            // Assert
            claimsEmptyAction.Should().Throw<ArgumentNullException>();
            claimsNullAction.Should().Throw<ArgumentNullException>();
        }
    }
}
