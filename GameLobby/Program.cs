using GameLobby.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

app.MapHub<GameHub>("/gamehub");
//app.MapBlazorHub(); // For Blazor Server real-time rendering

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>() // Ensure 'App' is defined in the correct namespace
    .AddInteractiveServerRenderMode();

app.Run();
