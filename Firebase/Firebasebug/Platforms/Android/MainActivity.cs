using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.Firebase.CloudMessaging;

namespace test;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        HandleIntent(Intent);
        CreateNotificationChannel();
    }
    private static void HandleIntent(Android.Content.Intent intent)
    {
        FirebaseCloudMessagingImplementation.OnNewIntent(intent);

    }
    private void CreateNotificationChannel()
    {
        //private const string NotificationChannelId = "100";
        //private const string NotificationChannelName = "libero_notification_channel";
        //private const string NotificationChannelDescription = "Receive notifications";
        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            var channelId = $"{PackageName}.general";
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            var channel = new NotificationChannel(channelId, "General", NotificationImportance.Default);
            notificationManager.CreateNotificationChannel(channel);
            FirebaseCloudMessagingImplementation.ChannelId = channelId;
            //FirebaseCloudMessagingImplementation.SmallIconRef = Resource.Drawable.ic_push_small;
        }
    }
}

