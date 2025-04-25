
using ElixirControlPlatform.API.IAM.Application.Internal.CommandServices;
using ElixirControlPlatform.API.IAM.Application.Internal.OutboundServices;
using ElixirControlPlatform.API.IAM.Application.Internal.QueryServices;
using ElixirControlPlatform.API.IAM.Domain.Repositories;
using ElixirControlPlatform.API.IAM.Domain.Services;
using ElixirControlPlatform.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using ElixirControlPlatform.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using ElixirControlPlatform.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using ElixirControlPlatform.API.IAM.Infrastructure.Tokens.JWT.Services;
using ElixirControlPlatform.API.Profiles.Application.Internal.CommandServices;
using ElixirControlPlatform.API.Profiles.Application.Internal.QueryServices;
using ElixirControlPlatform.API.Profiles.Domain.Repositories;
using ElixirControlPlatform.API.Profiles.Domain.Services;
using ElixirControlPlatform.API.Profiles.Infrastructure.Persistence.EFC.Repositories;
using ElixirControlPlatform.API.Shared.Domain.Repositories;
using ElixirControlPlatform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using ElixirControlPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ElixirControlPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using ElixirControlPlatform.API.Shared.Infrastructure.Pipeline.Middleware.Components;
using ElixirControlPlatform.API.WinemakingProcess.Application.Internal.CommandServices;
using ElixirControlPlatform.API.WinemakingProcess.Application.Internal.QueryServices;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Repositories;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Services;
using ElixirControlPlatform.API.WinemakingProcess.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Configura el servicio CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // Permite cualquier origen
            .AllowAnyHeader()   // Permite cualquier cabecera
            .AllowAnyMethod();  // Permite cualquier método (GET, POST, PUT, DELETE, etc.)
    });
});



//===================================Add services to the container=====================================
builder.Services.AddControllers();
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null)
{
    throw new InvalidOperationException("Connection string not found.");
}


builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {

        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
    else if (builder.Environment.IsProduction())
    {
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
    }
});
//======================================================================================================

//======== Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle ========
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());
//======================================================================================================

// Dependency Injection

//===================================== Shared Bounded Context ====================================
builder.Services.AddScoped<IUnitOfWOrk, UnitOfWork>();
//=================================== END Shared Bounded Context ==================================


//===================================== 1. GONZALO Bounded Context ================================
//----------------- Batches -----------------
builder.Services.AddScoped<IBatchRepository, BatchRepository>();
builder.Services.AddScoped<IBatchCommandService, BatchCommandService>();
builder.Services.AddScoped<IBatchQueryService, BatchQueryService>();

//----------------- Profiles -----------------

builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();



// IAM Bounded Context Dependency Injection Configuration

// TokenSettings Configuration

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();

// Common Exception Handling Middleware
builder.Services.AddExceptionHandler<CommonExceptionHandler>();
builder.Services.AddExceptionHandler<CommonExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Aplica la política CORS globalmente
app.UseCors("AllowAll");


//==================== Verify if the database exists and create it if it doesn't ===================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    context.Database.EnsureCreated();
}
//===============================================================================================


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();