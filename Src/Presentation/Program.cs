using Presentation.Configurations;

var builder = WebApplication.CreateBuilder(args);

//                                          services container conf.

builder.Services.ConfigureServices(builder);

builder.Services.AddEndpoints();

var app = builder.Build();

//                                           HTTP request pipeline.
app.MapEndpoints();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
