using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace lego_indexor_api.API;

public class WebSocketServer
{
    private readonly HttpContext _context;
    private WebSocket? _webSocket;
    private readonly byte[] _buffer;

    public WebSocketConnection? Connection;

    public bool IsReading = true;

    public WebSocketServer(HttpContext context)
    {
        _context = context;
        _buffer = new byte[256];
    }
    
    public async Task Run()
    {
        _webSocket = await _context.WebSockets.AcceptWebSocketAsync();
        while (true)
        {
            try
            {
                if (!IsOpen())
                {
                    await Close();
                    return;
                }

                await OnMessage();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                await Close();
                return;
            }
        }
    }

    public void OnClientConnection(string? id)
    {
        if (id == null)
            return;
        
        Connection = new WebSocketConnection(id, true);
        WebSocketManager.Add(Connection, this);
        Console.WriteLine($"Connection ID: {Connection.Id}");
    }

    public async Task OnMessage()
    {
        if (!IsReading)
            return;
        
        var messageObj = await ReadJson();
        if (messageObj == null)
            return;

        switch (messageObj.messageType)
        {
            case MessageType.NewConnection:
                OnClientConnection(messageObj.newConnectionId);
                break;
            case MessageType.PictureTaken:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public async Task Close()
    {
        if (_webSocket == null || Connection == null)
            return;
        await _webSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
    }

    public async Task Send(string message)
    {
        if (_webSocket == null)
            return;
        await _webSocket.SendAsync(Encoding.ASCII.GetBytes(message), WebSocketMessageType.Text, true, CancellationToken.None);
    }
    
    public async Task<string> Read()
    {
        if (_webSocket == null)
            return string.Empty;
        var result = await _webSocket.ReceiveAsync(_buffer, CancellationToken.None);
        return Encoding.ASCII.GetString(_buffer, 0, result.Count);
    }
    
    public async Task<WebSocketRequest?> ReadJson()
    {
        var message = await Read();
        return MessageToJson(message);
    }

    public WebSocketRequest? MessageToJson(string message)
    {
        return string.IsNullOrEmpty(message) ? null : JsonSerializer.Deserialize<WebSocketRequest>(message);
    }

    public bool IsOpen() => _webSocket?.State is WebSocketState.Open or WebSocketState.CloseSent;
}
