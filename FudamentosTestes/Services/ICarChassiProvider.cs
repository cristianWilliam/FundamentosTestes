namespace FudamentosTestes.Services;

internal interface ICarChassiProvider
{
    Task<bool> CheckIfValidAsync(Guid id, bool isValid, CancellationToken ct);
}