using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Plugin.AI.McpServer;
using Nop.Plugin.AI.McpServer.Services;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Routing;
using Nop.Web.Infrastructure;

namespace Nop.Plugin.AI.McpServer.Infrastructure;

/// <summary>
/// Represents plugin route provider
/// </summary>
public class RouteProvider : BaseRouteProvider, IRouteProvider
{
    /// <summary>
    /// Register routes
    /// </summary>
    /// <param name="endpointRouteBuilder">Route builder</param>
    public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapMcp("/mcp")
            .RequireAuthorization(policy => policy
                .AddAuthenticationSchemes(PatAuthenticationHandler.SCHEME_NAME)
                .RequireAuthenticatedUser());

        var lang = GetLanguageRoutePattern();

        endpointRouteBuilder.MapControllerRoute(name: McpServerDefaults.CustomerTokensRouteName,
            pattern: $"{lang}/customer/mcp-app",
            defaults: new { controller = "McpServerPublic", action = "CustomerTokens" });

        endpointRouteBuilder.MapControllerRoute(name: McpServerDefaults.GenerateTokenRouteName,
            pattern: $"{lang}/mcpapp/generate-token",
            defaults: new { controller = "McpServerPublic", action = "GenerateToken" });

        endpointRouteBuilder.MapControllerRoute(name: McpServerDefaults.RevokeTokenRouteName,
            pattern: $"{lang}/mcpapp/revoke-token",
            defaults: new { controller = "McpServerPublic", action = "RevokeToken" });
    }

    /// <summary>
    /// Gets a priority of route provider
    /// </summary>
    public int Priority => 50;
}
