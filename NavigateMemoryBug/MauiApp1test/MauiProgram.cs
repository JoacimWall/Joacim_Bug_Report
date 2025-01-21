using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TietoEvry.Maui;
using TietoEvry.Shared.Helpers;
using TietoEvry.Shared.Services;
using TietoEvry.Shared.Services.Interfaces;

namespace MauiApp1test;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseTietoEvryMaui()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        builder.Services.AddSingleton<IAnalyticsService, AnalyticsService>();
        builder.Services.AddSingleton<ILogService,TsLogServiceNone>();
        builder.Services.AddTransient<TestViewModel>();
        builder.Services.AddTransient<Test>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        MauiApp app = builder.Build();
        //we must initialize our service helper before using it
        ServiceHelper.Initialize(app.Services);
        return app;
    }
    
}