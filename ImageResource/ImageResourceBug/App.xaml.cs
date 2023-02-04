using ImageResourceBugLib;

namespace ImageResourceBug;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        App.Current.MainPage = new NavigationPage(new LibPage());
        //MainPage = new AppShell();
	}
}

