using ApiClientes.Data.Interfaces;
using ApiClientes.Data.Repositories;
using ApiClientes.Services.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Adicionando as configurações do Swagger
SwaggerConfiguration.AddSwagger(builder);

//adicionando as configurações do AutoMapper
AutoMapperConfiguration.AddAutoMapper(builder);

// Adicionando as injeções de dependência do projeto
builder.Services.AddTransient<IClienteRepository, ClienteRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
