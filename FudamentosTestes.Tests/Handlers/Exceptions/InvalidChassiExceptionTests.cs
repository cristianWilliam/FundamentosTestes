using Bogus;
using FluentAssertions;
using FudamentosTestes.Handlers.Exceptions;
using Xunit.Abstractions;

namespace FudamentosTestes.Tests.Handlers.Exceptions;

[Trait("Category", "InvalidChassiExceptionTests")]
public sealed class InvalidChassiExceptionTests
{
    private readonly Faker _faker = new("pt_BR");
    private readonly ITestOutputHelper _testOutputHelper;

    public InvalidChassiExceptionTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Constructor_GivenMessage_ThenShouldSetMessageToException()
    {
        // Arrange
        var message = _faker.Lorem.Text();
        _testOutputHelper.WriteLine(message);

        // Act
        var exception = new InvalidChassiException(message);

        // Assert
        exception.Message.Should().Be(message);
    }

}