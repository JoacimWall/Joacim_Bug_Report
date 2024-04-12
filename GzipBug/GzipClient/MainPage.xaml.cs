using GzipBug;
using TietoEvry.Shared.Services;

namespace GzipClient;

public partial class MainPage : ContentPage
{
	int count = 0;
	MyRestClient client = new MyRestClient();  
	public MainPage()
	{
		InitializeComponent();

		client.ReInit();

    }

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		count++;
        LoginRequest loginRequest = new LoginRequest { Email = "joacim.wall@gmail.com", Password = "test" };
        var result = await this.client.ExecutePostAsync<LoginResponse>("v1/login", loginRequest, null, false);
       
		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}
public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

