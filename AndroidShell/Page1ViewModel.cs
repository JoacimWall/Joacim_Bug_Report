using System;
using System.Windows.Input;

namespace AndroidShell
{
    public class Page1ViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        public Page1ViewModel()
        {
        }
        public ICommand MasterIndexSelectCommand => new Command(async () => await MasterIndexSelectAsync());


        private async Task MasterIndexSelectAsync()
        {
            await Shell.Current.GoToAsync(nameof(NewPage2));
        }
    }
}

