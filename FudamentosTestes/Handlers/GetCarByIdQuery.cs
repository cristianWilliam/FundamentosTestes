using FudamentosTestes.Dtos;
using FudamentosTestes.Entities;
using MediatR;

namespace FudamentosTestes.Handlers;

internal record GetCarByIdQuery(Guid CardId) : IRequest<CarDto?>;