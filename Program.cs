using MinApi;

var builder = WebApplication.CreateBuilder(args);
DI.RegisterServices(builder.Services);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var apis = app.Services.GetServices<IMinApi>();

foreach (var api in apis)
{
    if (api is null) continue;
    api.Register(app);
}

app.Run();
