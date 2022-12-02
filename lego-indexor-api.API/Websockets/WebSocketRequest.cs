namespace lego_indexor_api.API;

public class WebSocketRequest
{
    public int messageType { get; set; }
    public bool isNewConnection { get; set; }
    public string? macAddress { get; set; }
    public string? fileName { get; set; }
    public string? ip { get; set; }
    public string? url { get; set; }
}