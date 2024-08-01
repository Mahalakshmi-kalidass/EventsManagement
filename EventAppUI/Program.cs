using EventsDAL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
    AddCookie(options => { options.LoginPath = "/Login";   })
    .AddGoogle(options => {
        options.ClientId = "931224279555-gete1ncckbt3b0gjgv1uhd946i3ftf1n.apps.googleusercontent.com";
        options.ClientSecret = "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"; 
    });


var app = builder.Build();


app.UseHttpsRedirection();
app.UseSession();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
