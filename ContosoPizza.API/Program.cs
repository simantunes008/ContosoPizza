using Microsoft.EntityFrameworkCore;
using ContosoPizza.Domain.Interfaces;
using ContosoPizza.Application.Interfaces;
using ContosoPizza.Application.Services;
using ContosoPizza.Infrastructure.Data;
using ContosoPizza.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURAÇÃO DOS SERVIÇOS (DI Container)

// Adiciona suporte para Controllers (para que a pasta /Controllers funcione)
builder.Services.AddControllers();

// Configura o Swagger (Interface visual para testar a API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. CONFIGURAR A BASE DE DADOS (Entity Framework)
// Ele vai procurar a "DefaultConnection" no ficheiro appsettings.json
builder.Services.AddDbContext<PizzaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. REGISTO DOS PORTS E ADAPTERS (O "Coração" da Clean Architecture)
// Camada de Dados: Sempre que alguém pedir IPizzaRepository, entrega o PizzaRepository (Infrastructure)
builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();

// Camada de Negócio: Sempre que alguém pedir IPizzaService, entrega o PizzaService (Application)
builder.Services.AddScoped<IPizzaService, PizzaService>();

var app = builder.Build();

// 4. CONFIGURAÇÃO DO PIPELINE HTTP (Middleware)

// Ativa o Swagger apenas em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redireciona HTTP para HTTPS (pode causar o erro SSL_ERROR se o certificado não estiver ok)
app.UseHttpsRedirection();

app.UseAuthorization();

// Mapeia automaticamente as rotas definidas nos teus [Route("[controller]")]
app.MapControllers();

app.Run();