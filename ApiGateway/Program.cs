using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using ApiGateway.Handlers;
using ApiGateway.Aggregators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Configurando Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration)
    .AddDelegatingHandler<SampleHandler>(true) // Registrando el filtro de la solicitud junto al parametro que sirve para definir si afectara a todas las peticiones
    .AddTransientDefinedAggregator<UserPostsAggregator>()
    .AddCacheManager(x => // Registrando el manejador de caché
    {
        x.WithDictionaryHandle();
    });
// Configurando autenticacion JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = "AuthServer",
            ValidAudience = "AuthClient",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASecretKeyThatIs256BitsLong123"))
        };
    });

builder.Services.AddControllers();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configurando Ocelot
await app.UseOcelot();

// Configurando el pipeline de auth
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
