using API.Middleware;
using Application.Common.Mapping.AutoMapperProfiles;
using Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(ProjectMappingProfile).Assembly);
builder.Services.AddAuthenticationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

// TODO: Diğer Service, Repository, DbContext vs. bağımlılıkları da buraya eklenecek

var app = builder.Build();

//  Exception Handling Middleware en üstte olmalı
app.UseMiddleware<ExceptionHandlingMiddleware>();

// TODO: Diğer middleware'ler (e.g. app.UseRouting(), app.UseAuthentication(), vs.)

app.Run();