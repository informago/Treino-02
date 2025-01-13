using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Treino_REST_02.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Treino REST API",
        Description = "Como desenvolver um REST API utilizando C# e .NET Core",
        TermsOfService = new Uri("https://www.larsoft.com.br/autenticacao-em-2-fatores-2fa-nos-sistemas-larsoft/"),
        Contact = new OpenApiContact
        {
            Name = "Entre em contato com a Larsoft",
            Url = new Uri("https://www.larsoft.com.br/institucional/contato/")
        },
        License = new OpenApiLicense
        {
            Name = "Como a Larsoft trata a segurança de dados",
            Url = new Uri("https://www.larsoft.com.br/institucional/seguranca/")
        }
    });
    opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Name = "Authorization",
        Description = "Token must be provided.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat =  "JWT",
        Scheme = "Bearer"
    });
    opts.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opts.IncludeXmlComments(xmlPath, true);
});

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();
builder.Services.AddDbContext<UsuarioDbContext>(opt => opt.UseSqlServer());
builder.Services.AddIdentityCore<Usuario>()
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddApiEndpoints();

var app = builder.Build();

app.MapGroup("Autenticacao").MapIdentityApi<Usuario>();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseDefaultFiles(new DefaultFilesOptions
{
    DefaultFileNames = new
     List<string> { "index.html" }
}); 
app.UseStaticFiles();

app.Run();

