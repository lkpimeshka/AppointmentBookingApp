using AppointmentBookingApp.Application.Appointments;
using AppointmentBookingApp.Application.Auth;
using AppointmentBookingApp.Client.Components;
using AppointmentBookingApp.Client.Providers;
using AppointmentBookingApp.Infrastructure.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Register HttpClient with BaseAddress
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7139/") });

// Register Authentication Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<IAuthStateProvider>(sp => sp.GetRequiredService<AuthStateProvider>());
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<AuthStateProvider>());

// Add Authorization Services
builder.Services.AddAuthorizationCore();  // Authorization core services (needed for Blazor)
builder.Services.AddAuthorization();  // Adds authorization services
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("Professional", policy => policy.RequireRole("Professional"));
//});
builder.Services.AddScoped<ProtectedSessionStorage>(); // Persistent auth storage

//Add Appointment Services
builder.Services.AddScoped<AppointmentBookingApp.Client.Services.AppointmentService>();

//Add Professional Services
builder.Services.AddScoped<AppointmentBookingApp.Client.Services.ProfessionalService>();

// Add Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddCircuitOptions(options =>
    {
        options.DetailedErrors = true;
    });

// Register MudBlazor Services
builder.Services.AddMudServices();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 2000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});


var app = builder.Build();

// Configure Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Ensure authentication middleware is added before authorization
//app.UseAuthentication(); // Add this line if using authentication
app.UseAuthorization();  // Make sure to use this after UseAuthentication

// Add Razor components and interactive server components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
