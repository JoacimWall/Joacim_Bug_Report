using Fm.SharedLib.Views;

namespace MediaElementBug;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(VideoFullPage), typeof(VideoFullPage));
    }
}

