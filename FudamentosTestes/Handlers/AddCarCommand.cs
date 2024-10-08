using FudamentosTestes.Dtos;
using MediatR;

namespace FudamentosTestes.Handlers;

internal record AddCarCommand(string Nome) : IRequest<CarDto>;