using System.Security.Claims;
using HelpPoint.Common.Errors.Exceptions;
using HelpPoint.Features.Auth;
using Microsoft.IdentityModel.JsonWebTokens;

namespace HelpPoint.Common.Http;

public class CurrentUserAccessor(IHttpContextAccessor httpContextAccessor) : ICurrentUserAccessor
{
    public string GetCurrentUsername() =>
        httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)
            ?.Value ?? throw new UnauthorizedException("token invalido");

    public string GetCurrentUserId() =>
        httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
        ?? throw new UnauthorizedException("Token inválido");

    public string GetCurrentUserSub() =>
        httpContextAccessor.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
        ?? throw new UnauthorizedException("Token inválido");

    public string GetCurrentRole() =>
        httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value ??
        throw new UnauthorizedException("token invalido");



    /// <summary>
    /// Obtiene el valor completo de la cabecera 'Authorization'.
    /// Devuelve null si la cabecera no está presente o el HttpContext no está disponible.
    /// </summary>
    /// <returns>El valor de la cabecera (ej: "Bearer eyJ...") o null.</returns>
    private string? GetRawAuthorizationHeader()
    {
        // Accede directamente a las cabeceras de la solicitud actual
        return httpContextAccessor.HttpContext?.Request.Headers.Authorization;
    }

    /// <summary>
    /// Obtiene solo la parte del token (sin "Bearer ") de la cabecera 'Authorization'.
    /// Devuelve null si la cabecera no está presente, no empieza con "Bearer ", o el HttpContext no está disponible.
    /// </summary>
    /// <returns>La cadena del token JWT crudo o null.</returns>
    public string? GetRawBearerToken()
    {
        var authorizationHeader = GetRawAuthorizationHeader();

        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return authorizationHeader.Substring("Bearer ".Length).Trim();
        }

        return null;
    }
}
