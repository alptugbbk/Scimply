
using Core.Abstractions.Cryptographies;
using Core.Abstractions.Services;
using Logic.Concretes.Cryptographies;
using Logic.Concretes.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddScoped<IScimService, ScimService>();
builder.Services.AddScoped<IAESEncryption, AESEncryption>();

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
