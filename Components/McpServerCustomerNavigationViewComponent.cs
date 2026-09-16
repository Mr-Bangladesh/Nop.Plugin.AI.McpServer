using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Customers;
using Nop.Web.Framework.Components;
using Nop.Web.Models.Customer;

namespace Nop.Plugin.AI.McpServer.Components;

public class McpServerCustomerNavigationViewComponent : NopViewComponent
{
    #region Fields
    private readonly ICustomerService _customerService;
    private readonly IWorkContext _workContext;
    #endregion
    
    #region ctor
    public McpServerCustomerNavigationViewComponent(ICustomerService customerService, IWorkContext workContext)
    {
        _customerService = customerService;
        _workContext = workContext;
    }
    #endregion

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        if (additionalData is not CustomerNavigationModel model)
            return Content(string.Empty);

        var customer = await _workContext.GetCurrentCustomerAsync();
        if (await _customerService.IsGuestAsync(customer))
            return Content(string.Empty);

        return await ViewAsync("~/Plugins/AI.McpServer/Views/Components/CustomerMcpServerMenu.cshtml", model);
    }
}
