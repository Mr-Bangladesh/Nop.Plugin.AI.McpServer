using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using Nop.Core;
using Nop.Plugin.AI.McpServer.Models;
using Nop.Plugin.AI.McpServer.Services;

namespace Nop.Plugin.AI.McpServer.Components;

public class McpServerCustomerInfoViewComponent : NopViewComponent
{
    private readonly IPersonalAccessTokenService _patService;
    private readonly IWorkContext _workContext;

    public McpServerCustomerInfoViewComponent(IPersonalAccessTokenService patService, IWorkContext workContext)
    {
        _patService = patService;
        _workContext = workContext;
    }

    /// <returns>A task that represents the asynchronous operation</returns>
    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var model = new PersonalAccessTokenModel();
        var customer = await _workContext.GetCurrentCustomerAsync();
        if (customer != null)
        {
            var tokens = await _patService.GetTokensByCustomerAsync(customer.CustomerGuid);
            var active = tokens?.FirstOrDefault(t => t.RevokedOnUtc == null);
            if (active != null)
            {
                var visibleHash = active.TokenHash != null && active.TokenHash.Length >= 6
                    ? active.TokenHash.Substring(0, 6)
                    : active.TokenHash ?? string.Empty;
                model.HasToken = true;
                model.Masked = $"{active.TokenPrefix}...{visibleHash}";
            }
        }

        return View("~/Plugins/AI.Server/Views/McpServerCustomerInfo.cshtml", model);
    }
}
