namespace MediaBug;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new StartShell());
    }
    override protected async void OnStart()
    {
        await Task.Delay(2000);
        //await ShowAlertAsync("OnStart", "Hello", "OK", null);
        if (Current != null)
        {
            Windows[0].Page = new AppShell();
        }
    }

    protected override async void OnResume()
    {
        await Task.Delay(2000);
        //await ShowAlertAsync("OnStart", "Hello", "OK", null);
        if (Current != null)
        {
            Windows[0].Page = new AppShell();
        }
        // await ShowAlertAsync("OnResume", "Hello", "OK", null);
    }

    public async Task<bool> ShowAlertAsync(string? message, string? title, string? buttonAccept, string? buttonCancel)
    {
        return await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            if (Application.Current != null && Application.Current.Windows[0].Page != null)
            {
                var page = Application.Current.Windows[0].Page;
                return  page != null && await page.DisplayAlert(title, message, buttonAccept, buttonCancel);
            }

            return false;
        });
    }
}