using Fm.SharedLib.Views;

namespace MediaEmelentBug2;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{

        string blob;

        count++;
        
        if (count % 2 == 0)
			blob = "2023_01_24_16_46_2_1_2_respektera_forbud.mp4";
		else
			blob = "2023_01_24_16_54_2_1_4_kontaktovning.mp4";

        await Shell.Current.GoToAsync($"{nameof(VideoFullPage)}?VideoBlob={blob}");

        SemanticScreenReader.Announce(CounterBtn.Text);
	}
}


