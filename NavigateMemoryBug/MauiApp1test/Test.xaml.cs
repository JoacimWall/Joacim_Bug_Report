namespace MauiApp1test;

public partial class Test : ContentPage
{
    public Test()
    {
        //GC.Collect(); 
        InitializeComponent();
        
    }
        ~Test() => Console.WriteLine("Finalizer for ~Test()");
    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        Console.WriteLine("PopAsync Test");
       await Shell.Current.Navigation.PopAsync();
       
    }
}