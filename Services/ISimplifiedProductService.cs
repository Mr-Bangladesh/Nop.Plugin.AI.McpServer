using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.AI.McpServer.Models;

namespace Nop.Plugin.AI.McpServer.Services;

public interface ISimplifiedProductService
{
    Task<IPagedList<Product>> SearchProductsAsync(SearchCatalogRequest request);
}