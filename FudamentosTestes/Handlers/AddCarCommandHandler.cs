using FudamentosTestes.Db;
using FudamentosTestes.Dtos;
using FudamentosTestes.Entities;
using FudamentosTestes.Handlers.Exceptions;
using FudamentosTestes.Services;
using MediatR;

namespace FudamentosTestes.Handlers;

internal class AddCarCommandHandler(ICarChassiProvider carChassiProvider, AppDbContext appDbContext) : IRequestHandler<AddCarCommand, CarDto>
{
    public async Task<CarDto> Handle(AddCarCommand request, CancellationToken cancellationToken)
    {
        // Generating Id
        var id = Guid.NewGuid();

        var isValidChassi = await carChassiProvider.CheckIfValidAsync(id, true, cancellationToken);

        if (!isValidChassi)
            throw new InvalidChassiException($"[{request.Nome}] chassi invalido!");

        var car = new Car(id, request.Nome);
        
        await appDbContext.Cars.AddAsync(car, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);

        return new CarDto(car.Id, car.Name);
    }
}