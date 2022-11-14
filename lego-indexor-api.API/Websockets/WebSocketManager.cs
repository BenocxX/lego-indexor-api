namespace lego_indexor_api.API;

public static class WebSocketManager
{
    private static readonly Dictionary<WebSocketConnection, WebSocketServer> WebSockets = new ();

    public static void Add(WebSocketConnection connection, WebSocketServer server)
    {
        WebSockets.Add(connection, server);
    }

    public static WebSocketServer? GetServer(WebSocketConnection connection)
    {
        foreach (var (webSocketConnection, server) in WebSockets)
        {
            if (webSocketConnection.IsOpen && webSocketConnection.Id == connection.Id)
                return server;
        }
        return null;
    }
    
    public static void Remove(string? id)
    {
        foreach (var (webSocketConnection, _) in WebSockets)
        {
            if (webSocketConnection.IsOpen && webSocketConnection.Id == id)
                WebSockets.Remove(webSocketConnection);
        }
    }
}
