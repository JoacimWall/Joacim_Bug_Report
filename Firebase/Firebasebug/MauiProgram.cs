using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Plugin.Firebase.CloudMessaging;
using Plugin.Firebase.Analytics;

#if IOS
using Plugin.Firebase.Core.Platforms.iOS;
#else
using Plugin.Firebase.Core.Platforms.Android;
using Android.App;
using Firebase;

#endif

namespace test;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

    #region Firebase Messages
    private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
    {
        try
        {


            builder.ConfigureLifecycleEvents(events => {
#if IOS
            events.AddiOS(iOS => iOS.FinishedLaunching((app, launchOptions) =>
            {
                
                CrossFirebase.Initialize();
                FirebaseCloudMessagingImplementation.Initialize();
                return false;
            }));

            
#else
                events.AddAndroid(android => android.OnCreate((activity, state) =>
                {
                    CrossFirebase.Initialize(activity);
                    FirebaseAnalyticsImplementation.Initialize(activity);

                }));

#endif
            });
            builder.Services.AddSingleton(_ => CrossFirebaseCloudMessaging.Current);
            //builder.Services.AddSingleton(_ => CrossFirebaseAuth.Current);
            return builder;
        }
        catch (Exception ex)
        {
            var logdic = new Dictionary<string, string>();
            logdic.Add("Info", "Error in RegisterFirebaseServices");
            //TietoGlobals.ReportErrorToAppCenter(ex, logdic);
            return builder;
        }
    }

    #endregion
}

