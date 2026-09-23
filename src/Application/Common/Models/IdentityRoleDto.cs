using AutoMapper;

namespace Delex_POS.Application.Common.Models;

public class IdentityRoleDto
{
    public string Id { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;
    public string NormalizedName { get; init; } = string.Empty;
}
