using Plugin.Firebase.CloudMessaging;

namespace test;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		

		
		

        var token = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();
		await App.Current.MainPage.DisplayAlert("Firebase",token,"ok");
    }
    

    async void AskPermisson(System.Object sender, System.EventArgs e)
    {
        await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();
    }
}


