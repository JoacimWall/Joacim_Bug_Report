﻿using ImageResourceBugLib;
using Microsoft.Extensions.Logging;

namespace ImageResourceBug;

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

			Routing.RegisterRoute(nameof(LibPage), typeof(LibPage));
#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

