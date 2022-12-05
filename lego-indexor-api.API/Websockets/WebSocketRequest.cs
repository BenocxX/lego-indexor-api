namespace lego_indexor_api.API;

public class WebSocketRequest
{
    public int messageType { get; set; }
    public bool isNewConnection { get; set; }
    public string? macAddress { get; set; }
    public string? fileCamTop { get; set; }
    public string? fileCamSide { get; set; }
    public string? ip { get; set; }
}