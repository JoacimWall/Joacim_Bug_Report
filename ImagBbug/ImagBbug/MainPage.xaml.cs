using Microsoft.Maui.Controls;

namespace ImagBbug;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
    protected override void OnAppearing()
    {	//this ur is inly working for 24 hours
        ImageCtl.Source = "https://saprodbabyappvb3o.blob.core.windows.net/userprofileimages/libero.se/1e063891-df37-41e2-9622-3dbe7b2c0f15.jpg?skoid=51d2c490-473d-4ba8-abb6-2e58e5c9b21a&sktid=f101208c-39d3-4c8a-8cc7-ad896b25954f&skt=2024-04-10T06%3A43%3A16Z&ske=2024-04-11T06%3A48%3A16Z&sks=b&skv=2023-11-03&sv=2023-11-03&st=2024-04-10T06%3A43%3A16Z&se=2024-04-11T06%3A48%3A16Z&sr=b&sp=r&sig=RItiGd%2Fm8KojDt2vX5lRbgiVrBvtc7kHdR10WNWlXb0%3D";
        base.OnAppearing();
		//generate error below
       // 2024 - 04 - 10 08:48:49.436471 + 0200 LiberoClub[79120:14372932][SystemGestureGate] < 0x7fbbcb7a8900 > Gesture: System gesture gate timed out.
		//Microsoft.Maui.UriImageSourceService: Warning: Unable to load image URI 'https://saprodbabyappvb3o.blob.core.windows.net/userprofileimages/libero.se/1e063891-df37-41e2-9622-3dbe7b2c0f15.jpg?skoid=51d2c490-473d-4ba8-abb6-2e58e5c9b21a&sktid=f101208c-39d3-4c8a-8cc7-ad896b25954f&skt=2024-04-10T06%3A43%3A16Z&ske=2024-04-11T06%3A48%3A16Z&sks=b&skv=2023-11-03&sv=2023-11-03&st=2024-04-10T06%3A43%3A16Z&se=2024-04-11T06%3A48%3A16Z&sr=b&sp=r&sig=RItiGd%2Fm8KojDt2vX5lRbgiVrBvtc7kHdR10WNWlXb0%3D'.

		//System.InvalidOperationException: Unable to cache image at '/Users/joacimwall/Library/Developer/CoreSimulator/Devices/80FF6BFF-8F71-47B6-B3D1-FED80EC0B3DD/data/Containers/Data/Application/9A701251-63CC-4946-A67B-9F963EE3176E/Library/Caches/com.microsoft.maui/MauiUriImages/D9FCD29404245B2E.jpg?skoid=51d2c490-473d-4ba8-abb6-2e58e5c9b21a&sktid=f101208c-39d3-4c8a-8cc7-ad896b25954f&skt=2024-04-10T06%3A43%3A16Z&ske=2024-04-11T06%3A48%3A16Z&sks=b&skv=2023-11-03&sv=2023-11-03&st=2024-04-10T06%3A43%3A16Z&se=2024-04-11T06%3A48%3A16Z&sr=b&sp=r&sig=RItiGd%2Fm8KojDt2vX5lRbgiVrBvtc7kHdR10WNWlXb0%3D'.
		//   at Microsoft.Maui.UriImageSourceService.CacheImage(NSData imageData, String path)
		//   at Microsoft.Maui.UriImageSourceService.DownloadAndCacheImageAsync(IUriImageSource imageSource, CancellationToken cancellationToken)
		//   at Microsoft.Maui.UriImageSourceService.GetImageAsync(IUriImageSource imageSource, Single scale, CancellationToken cancellationToken)
    }
}


