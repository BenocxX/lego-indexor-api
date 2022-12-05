namespace lego_indexor_api.API.Websockets;

public class WebSocketConnection
{
    public string Id { get; set; }
    public bool IsOpen { get; set; }
    
    public WebSocketConnection(string id, bool isOpen)
    {
        Id = id;
        IsOpen = isOpen;
    }
}
