namespace FudamentosTestes.Services;

public interface ICarChassiValidatorService
{
    Task<bool> CheckIfValidAsync(Guid id, CancellationToken ct);
}