using ToDoApi;
using ToDoApi.IoC;

var builder = WebApplication.CreateBuilder(args);

Configuration.ToDoCnn = builder.Configuration.GetConnectionString("ToDoCnn")!;
Configuration.TokenKey = builder.Configuration.GetValue<string>("TokenKey")!;

builder.Services.AddInfrastructure();

builder.Services.AddInfrastructureSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ToDoAppCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
