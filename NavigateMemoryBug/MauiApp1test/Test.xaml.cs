using TietoEvry.Maui.Views;
using TietoEvry.Shared.Helpers;
using TietoEvry.Shared.Services.Interfaces;

namespace MauiApp1test;

public partial class Test : BaseContentPage
{
    public Test(TestViewModel vm)
    {
        //GC.Collect(); 
        InitializeComponent();
        BindingContext = vm;
        // BindingContext = new TestViewModel(ServiceHelper.GetService<IDialogService>(),
        //     ServiceHelper.GetService<IAnalyticsService>(),
        //     ServiceHelper.GetService<IInternetConnectionHelper>(),
        //     ServiceHelper.GetService<ILogService>(),
        //     ServiceHelper.GetService<IPlatformFunctions>());
    }
        ~Test() => Console.WriteLine("Finalizer for ~Test()");
    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        Console.WriteLine("PopAsync Test");
       await Shell.Current.Navigation.PopAsync();
       
    }
}