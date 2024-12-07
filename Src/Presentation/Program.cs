using Presentation.Configurations;

var builder = WebApplication.CreateBuilder(args);

//                                          services container conf.

builder.Services.ConfigureServices(builder);

var app = builder.Build();

//                                           HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
