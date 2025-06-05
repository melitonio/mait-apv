using Dto;

namespace Events;

public readonly record struct ApvCreadoEvent(Guid Id, string User, ApvDto Item)
{
    public const string Name = "ApvCreado";
}

public readonly record struct ApvActualizadoEvent(Guid Id, string User, ApvDto Item)
{
    public const string Name = "ApvActualizado";
}

public readonly record struct ApvElimidadoEvent(Guid Id, string User, ApvDto Item)
{
    public const string Name  = "ApvElimidado";
}