using CustomersApi.CasosDeUso;
using CustomersApi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuracion para que todas las URL sean lowerCase
builder.Services.AddRouting(routing=>routing.LowercaseUrls=true); 

// Configurando Base de datos
builder.Services.AddDbContext<CustomerDbContext>(mysqlBuilder => 
{
   mysqlBuilder.UseMySQL(builder.Configuration.GetConnectionString("CsCustomersDb"));
}
);

// Inyectando Servicio
builder.Services.AddScoped<IUpdateCustomerUseCase, UpdateCustomerUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
