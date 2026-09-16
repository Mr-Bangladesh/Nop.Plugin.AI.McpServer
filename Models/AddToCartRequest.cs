using System.Text.Json.Serialization;

namespace Nop.Plugin.AI.McpServer.Models;

public class AddToCartRequest
{
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }
}
