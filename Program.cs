using midasblazor.Components;
using MidasBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// HttpClient configurado para acessar sua API
builder.Services.AddHttpClient<LancamentoService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5059/");
});
builder.Services.AddHttpClient<ProjecoesService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5059/");
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
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