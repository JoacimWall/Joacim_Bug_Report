using System.Timers;

namespace MauiApp1test;

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
        Console.WriteLine("has navigate to test");
        Shell.Current.GoToAsync(nameof(Test));
       
        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
    private System.Timers.Timer runGcCollectTimer=new();
    protected override void OnAppearing()
    {
        GC.Collect();
        base.OnAppearing();
        if (DeviceInfo.DeviceType == DeviceType.Physical)
        {
            runGcCollectTimer = new System.Timers.Timer(10000);
            runGcCollectTimer.Elapsed += OnTimerRunGcCollect;
            runGcCollectTimer.AutoReset = true;
            runGcCollectTimer.Enabled = true;
        }
    }

    private void OnTimerRunGcCollect(object? sender, ElapsedEventArgs e)
    {
        GCMemoryInfo prememInfo = GC.GetGCMemoryInfo();
        //Test clean
       
        GC.Collect();
        GC.WaitForPendingFinalizers();

        GCMemoryInfo postmemInfo = GC.GetGCMemoryInfo();

        Console.WriteLine( $"GC.Collect() Before: {prememInfo.TotalCommittedBytes / Math.Pow(1024.0, 2):N2} MB / After: {postmemInfo.TotalCommittedBytes / Math.Pow(1024.0, 2):N2} MB");

    }

    private void OnCounterClicked2(object? sender, EventArgs e)
    {
        Console.WriteLine("has navigate to test 2");
        Shell.Current.GoToAsync(nameof(Test2));
    }
}