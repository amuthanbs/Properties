using Microsoft.AspNetCore.Authentication.Negotiate;
using EnManaiWebApi.Controllers;
using Serilog;
using EnManaiWebApi.Model;
using EnManaiWebApi.DAO;
using System.Text.Json.Serialization;
using System.Text.Json;
using TokenBased.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IHouseOwnerDAO, HouseOwnerDAO>();
builder.Services.AddScoped<ILoginDAO, LoginDAO>();
builder.Services.AddScoped<IHouseOwnerDAO, HouseOwnerDAO>();
builder.Services.AddScoped<IRentalDetailsDAO, RentalDetailsDAO>();
builder.Services.AddScoped<ISearchDAO, SearchDAO>();
builder.Services.AddScoped<ISMSVerificationDAO, SMSVerificationDAO>();
builder.Services.AddScoped<IRentalHouseDetails, RentalHouseDetailsDAO>();
builder.Services.AddJWTTokenServices(builder.Configuration);
//builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
//   .AddNegotiate();

//builder.Services.AddAuthorization(options =>
//{
//    // By default, all incoming requests will be authorized according to the default policy.
//    options.FallbackPolicy = options.DefaultPolicy;
//});
//builder.Host.UseSerilog();

var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
