namespace FudamentosTestes.Services;

internal sealed class CarChassiProvider : ICarChassiProvider
{
    public Task<bool> CheckIfValidAsync(Guid id, bool isValid, CancellationToken ct)
    {
        return Task.FromResult(isValid);
    }
}