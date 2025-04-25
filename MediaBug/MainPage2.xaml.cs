namespace MediaBug;

public partial class MainPage2 : ContentPage
{
    int count = 0;

    public MainPage2()
    {
        InitializeComponent();
    }

    void MainPage2_Unloaded(object? sender, EventArgs e)
    {
        // Stop and cleanup MediaElement when we navigate away
        TheVideoPlayer.Handler?.DisconnectHandler();
    }
}