using System.Net;
using lego_indexor_api.API;
using lego_indexor_api.Core;
using lego_indexor_api.Infrastructure;
using WebSocketManager = lego_indexor_api.API.WebSocketManager;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationCore();
builder.Services.AddInfrastructure();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseWebSockets();

app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var server = new WebSocketServer(context);
        await server.Run();
        WebSocketManager.Remove(server.Connection?.Id);
    }
    else
    {
        context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
    }
});

app.Run();