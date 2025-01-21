using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using TietoEvry.Shared.Services.Interfaces;
using TietoEvry.Shared.ViewModels;

namespace MauiApp1test;

public class TestViewModel: BaseViewModel
{
    public TestViewModel(IDialogService dialogService, IAnalyticsService analyticsService, IInternetConnectionHelper internetConnectionHelper, ILogService logService, IPlatformFunctions platformFunctions) : base(dialogService, analyticsService, internetConnectionHelper, logService, platformFunctions)
    {
        
    }
    public ICommand CloseViewCommand => new RelayCommand(async () => await CloseView());

    private async Task CloseView()
    {
        //await Shell.Current.Navigation.PopAsync();
        await PlatformFunctions.GoBack();

    }
}