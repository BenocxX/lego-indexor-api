namespace lego_indexor_api.API.Websockets;

public class WebSocketResponse
{
    public int messageType { get; set; }
    public string? id { get; set; }
    public string?[] files { get; set; }
}