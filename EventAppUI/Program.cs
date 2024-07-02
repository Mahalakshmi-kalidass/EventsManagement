var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();

app.Run();
