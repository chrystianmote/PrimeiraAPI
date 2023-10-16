using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PrimeiraAPI.Application.Mapping;
using PrimeiraAPI.Application.Swagger;
using PrimeiraAPI.Domain.Model.EmployeeAggregate;
using PrimeiraAPI.Infrastructure;
using PrimeiraAPI.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


builder.Services.AddControllers();

//Pegando os dados de configuração do Banco de Dados na Chave Secret  
var connectionSring = builder.Configuration.GetConnectionString("cnDatabase");
builder.Services.AddDbContext<ConnectionContext>(o => o.UseSqlServer(connectionSring));


builder.Services.AddAutoMapper(typeof(DomainToDTOMapping));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var apiVersioningBuilder = builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.ReportApiVersions = true;
});


apiVersioningBuilder.AddApiExplorer(setup =>
{
    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
    // note: the specified format code will format the version as "'v'major[.minor][-status]"
    setup.GroupNameFormat = "'v'VVV";

    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
    // can also be used to control the format of the API version in route templates
    setup.SubstituteApiVersionInUrl = true;
});

//Configurando o Swagger para usar a interface visual com o cabeçalho JWT 
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<SwaggerDefaultValues>();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()

        }
    });


});

// Injeção de dependencia ver: https://balta.io/blog/addtransient-addscoped-addsingleton
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();

//Criando política personalizada pra permitir somente meu localhost ter acesso.
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "MyPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:8080", "https://localhost:8080")
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
     );
});

var key = Encoding.ASCII.GetBytes(PrimeiraAPI.Key.Secret);

// Aplicando Autenticação JWT 
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{   //não obrigando o uso de HTTPS
    x.RequireHttpsMetadata = false;

    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

//Pegar a descrição da versão da API no Swagger
var versionDescriptionProvider = app.Services
    .GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error-development");
    app.UseSwagger();

    //alterna entre as versões no swagger no select do frontend
    app.UseSwaggerUI(options =>
    {
        foreach (var description in versionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                $"Primeira API - {description.GroupName.ToUpper()}");
        }
    });
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseCors("MyPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
