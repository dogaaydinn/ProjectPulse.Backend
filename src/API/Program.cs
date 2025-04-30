using Application.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(ProjectMappingProfile).Assembly);

var app = builder.Build();

app.Run();