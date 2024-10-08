namespace FudamentosTestes.Services;

internal sealed class CarChassiValidatorService : ICarChassiValidatorService
{
    public async Task<bool> CheckIfValidAsync(Guid id, CancellationToken ct)
    {
        await Task.Delay(3000, ct);
        return true;
    }
}