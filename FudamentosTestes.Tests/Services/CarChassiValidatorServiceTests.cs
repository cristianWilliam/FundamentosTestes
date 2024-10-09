using FluentAssertions;
using FudamentosTestes.Services;

namespace FudamentosTestes.Tests.Services;

[Trait("Category", "CarChassiValidatorServiceTests")]
public sealed class CarChassiValidatorServiceTests
{
    [Fact]
    public async Task CheckIfValidAsync_GivenAnyParams_ThenShouldReturnTrueAsync()
    {
        // Given any params
        var anyId = Guid.NewGuid();
        var validator = new CarChassiValidatorService();
        
        var result = await validator.CheckIfValidAsync(anyId, CancellationToken.None);
        
        // Then should return true
        result.Should().BeTrue();

    }

    // [Fact]
    // public async Task CheckIfValidAsync_GivenAnyParams_ThenShouldTakeMoreThanTwoSecondsAsync()
    // {
    //     // Given any params
    //     var anyId = Guid.NewGuid();
    //     var validator = new CarChassiValidatorService();
    //     var expectedTimeElapsed = 2000;
    //     
    //     var action = () => validator.CheckIfValidAsync(anyId, CancellationToken.None);
    //     
    //     // Then should return more than two seconds
    //     action.Should().ExecutionTimeOf()
    // }
}