using Nop.Plugin.AI.McpServer.Components;
using Nop.Services.Cms;
using Nop.Services.Common;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.AI.McpServer;

public class McpServerPlugin : BasePlugin, IWidgetPlugin
{
    public Type GetWidgetViewComponent(string widgetZone)
    {
        if (widgetZone == PublicWidgetZones.AccountNavigationAfter)
            return typeof(McpServerCustomerNavigationViewComponent);

        return null;
    }

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.AccountNavigationAfter });
    }

    #region Properties

    /// <summary>
    /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
    /// </summary>
    public bool HideInWidgetList => false;

    #endregion
}
