using EventsDAL.DataRepository;
using EventsDAL.LogProvider;
using EventsDAL.Models;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICRUDDataRepo<Event>, EventDataRepo>();
builder.Services.AddScoped<ICRUDDataRepo<Location>, LocationDataRepo>();
builder.Services.AddScoped<IEventAllocationDataRepo<EventAllocation>, EventAllocationDataRepo>();
builder.Services.AddScoped<IStaffDataRepo<Staff>, StaffDataRepo>();
builder.Services.AddScoped<ITopicsCoveredRepo<TopicCovered>, TopicDataRepo>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccessService<EventAccess>, AccessService>();
//logging

builder.Services.AddSingleton<ILoggerProvider, DatabaseLoggerProvider>();


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
}
  ) ;
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});
//configuration for swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Events API",
        Description = "Events Listing Management API",
        TermsOfService = new Uri("https://kaarinfotech.com/Privacy.html"),
        Contact = new OpenApiContact
        {
            Email="Mahalakshmi.Kalidas@kaarinfotech.com",
            Name="Mahalakshmi",
            Url = new Uri("https://LinkedIn.com/kaarinfotech.com/contactus"),
        },
        License = new OpenApiLicense { 
            Name = "Kaar Infotech",
            Url = new Uri("https://kaarinfotech.com")
        }
        
    }) ;
});
var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
