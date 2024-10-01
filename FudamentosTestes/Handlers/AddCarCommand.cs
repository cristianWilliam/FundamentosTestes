using FudamentosTestes.Dtos;
using MediatR;

namespace FudamentosTestes.Handlers;

public record AddCarCommand(string Nome) : IRequest<CarDto>;