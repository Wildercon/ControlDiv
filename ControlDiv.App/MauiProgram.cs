using Blazored.Modal;
using ControlDiv.App.Auth;
using ControlDiv.App.Data;
using ControlDiv.App.Repositories;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace ControlDiv.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7090/") });
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<IRepository, Repository>();
            builder.Services.AddScoped<IAccounRepository, AccountRepository>();
            builder.Services.AddSweetAlert2();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationProviderJWT>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
            builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
            builder.Services.AddBlazoredModal();


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            

            return builder.Build();
        }
    }
}