﻿using Foundation;
using UIKit;

namespace test;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        application.RegisterForRemoteNotifications();

        return base.FinishedLaunching(application, launchOptions);
    }
}

