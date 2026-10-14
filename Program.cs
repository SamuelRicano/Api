using SmartClaims.Api.Data;
using SmartClaims.Api.Data.Interfaces;
using SmartClaims.Api.Services;
using SmartClaims.Api.Services.Interfaces;
using SmartClaims.Api.Middlewares;



var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SmartClaims");

//Se agrega mi servicio SRM
builder.Services.AddScoped<ICotizacionesService,CotizacionesService>();
builder.Services.AddScoped<ICotizacionesRepository, CotizacionesRepository>();

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AngularPolicy");

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
