namespace FudamentosTestes.Services;

internal interface ICarChassiValidatorService
{
    Task<bool> CheckIfValidAsync(Guid id, CancellationToken ct);
}