using Dto;

namespace Events;

public readonly record struct ExpedienteCreadoEvent(Guid Id, string User, ApvPostDto Dto)
{
    public const string Name = "ExpedienteCreado-";
}