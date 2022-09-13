using lego_indexor_api.Core;
using lego_indexor_api.Infrastructure;

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
app.Run();