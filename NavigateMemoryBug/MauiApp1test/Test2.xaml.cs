namespace MauiApp1test;

public partial class Test2 : ContentPage
{
    public Test2()
    {
        GC.Collect(); 
        InitializeComponent();
        
    }
        ~Test2() => Console.WriteLine("Finalizer for ~Test2()");
    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        Console.WriteLine("PopAsync Test2");
       await Shell.Current.Navigation.PopAsync();
       
    }
}