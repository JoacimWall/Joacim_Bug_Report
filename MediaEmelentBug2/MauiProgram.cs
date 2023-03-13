using CommunityToolkit.Maui;
using Fm.SharedLib.Views;
using Microsoft.Extensions.Logging;

namespace MediaEmelentBug2;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkitMediaElement()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        Routing.RegisterRoute(nameof(VideoFullPage), typeof(VideoFullPage));

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

