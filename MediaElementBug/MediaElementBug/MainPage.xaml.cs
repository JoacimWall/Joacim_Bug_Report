using Fm.SharedLib.Views;

namespace MediaElementBug;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync($"{nameof(VideoFullPage)}?VideoBlob={"https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"}");
        //await Shell.Current.Navigation.PushModalAsync(new VideoFullPage("https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"));
    }
}


