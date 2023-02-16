using Microsoft.Extensions.Logging;
#if IOS 
using Maui.Nuke;
#endif
namespace MauiNukebug;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if IOS
		builder.UseNuke(showDebugLogs: true);
#endif

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

