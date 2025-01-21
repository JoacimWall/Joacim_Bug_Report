namespace MauiApp1test;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(Test), typeof(Test));
        Routing.RegisterRoute(nameof(Test2), typeof(Test2));
    }
}