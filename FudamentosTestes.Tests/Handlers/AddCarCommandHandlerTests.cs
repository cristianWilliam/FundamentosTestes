using Bogus;
using FluentAssertions;
using FudamentosTestes.Db;
using FudamentosTestes.Handlers;
using FudamentosTestes.Handlers.Exceptions;
using FudamentosTestes.Services;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace FudamentosTestes.Tests.Handlers;

public sealed class AddCarCommandHandlerTests
{
    private readonly AddCarCommandHandler _handler;
    private readonly Faker _faker = new("pt_BR");
    private readonly ICarChassiValidatorService _mockCarChassiValidatorService;
    private readonly AppDbContext _appDb;

    // Construtor é executado antes de cada test
    public AddCarCommandHandlerTests()
    {
        // App Db Context
        var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>();
        dbContextOptions.UseInMemoryDatabase("FudamentosTestes");

        _appDb = new AppDbContext(dbContextOptions.Options);
        
        // Moq Car Chassi
        _mockCarChassiValidatorService = Substitute.For<ICarChassiValidatorService>();
        
        _handler = new AddCarCommandHandler(_mockCarChassiValidatorService, _appDb);
    }

    [Fact]
    public async Task Handle_GivenChassiInvalid_ThenShouldThrowException()
    {
        // Given invalid chassi command
        var carName = _faker.Vehicle.Model();
        var invalidCommand = new AddCarCommand(carName);
        
        // Given Invalid chassi
        _mockCarChassiValidatorService.CheckIfValidAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(false));
        
        // When handle
        var resultAction = () => _handler.Handle(invalidCommand, CancellationToken.None);
        
        // Then should return exception
        var expectedErrorMessage = $"[{carName}] chassi invalido!";
        await resultAction.Should().ThrowAsync<InvalidChassiException>()
            .WithMessage(expectedErrorMessage);
    }

    [Fact]
    public async Task Handle_GivenChassiValid_ThenShouldInsertAndReturnNewCar()
    {
        // Given valid command
        var expectedCarName = _faker.Person.FullName;
        var validCommand = new AddCarCommand(expectedCarName);
        
        // Given Valid Chassi
        _mockCarChassiValidatorService.CheckIfValidAsync(Arg.Any<Guid>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));
        
        // Then handler is called
        var result = await _handler.Handle(validCommand, CancellationToken.None);
        _appDb.ChangeTracker.Clear();
        
        // Then should insert and return a new car.
        result.Name.Should().Be(expectedCarName);
        result.Id.Should().NotBeEmpty();
        
        var carId = result.Id;
        
        var insertedCar = await _appDb.Cars.SingleOrDefaultAsync(x => x.Id == carId, 
            CancellationToken.None);

        insertedCar.Should().NotBeNull();
        insertedCar!.Name.Should().Be(expectedCarName);
        insertedCar.Id.Should().Be(carId);
    }
    
}