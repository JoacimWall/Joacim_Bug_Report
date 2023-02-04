using ImageResourceBugLib;

namespace ImageResourceBug;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		//Shell.Current.GoToAsync(nameof(LibPage));

		App.Current.MainPage = new NavigationPage(new LibPage());
	}
}


