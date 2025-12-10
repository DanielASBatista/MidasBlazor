using midasblazor.Components;
using Microsoft.EntityFrameworkCore;
using MidasBlazor.Data;
using MidasBlazor.Services;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
    

builder.Services.AddScoped<ProjetoService>();
builder.Services.AddScoped<LancamentoService>(); 

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoAzure")));

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://projetomidas.azurewebsites.net/")
});


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
