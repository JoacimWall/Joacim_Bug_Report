using System;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System.Collections.ObjectModel;
using ClickEventBug.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClickEventBug
{
	public class MainPageViewModel : ObservableObject
    {
		public MainPageViewModel()
		{
            var list = new ObservableCollection<PhotoBookV2>();
            list.Add(new PhotoBookV2 { BookTemplateType = BookTemplateType.Standard, Title = "Test 1" });
            list.Add(new PhotoBookV2 { BookTemplateType = BookTemplateType.Standard, Title = "Test 2" });
            list.Add(new PhotoBookV2 { BookTemplateType = BookTemplateType.Standard, Title = "Test 3" });
            PhotoBooks = list;
        }
        public ICommand RefreshBooksCommand => new RelayCommand<bool>(async (e) => await RefreshAsync(e));

        private Task RefreshAsync(bool e)
        {
            throw new NotImplementedException();
        }

        public ICommand PhotobookMenuCommand => new Command(async (e) => await PhotobookMenuAsync(e));

        private async Task PhotobookMenuAsync(object e)
        {
            Console.WriteLine("Menu clicked");
        }
        public ICommand ShowPhotoBookCommand => new Command(async () => await ShowPhotoBookAsync());

        private async Task ShowPhotoBookAsync()
        {
            Console.WriteLine("Selected Item in collection click");
        }

        public bool IsLoading { get; set; } = false;
        private ObservableCollection<PhotoBookV2> _photoBooks;
        public ObservableCollection<PhotoBookV2> PhotoBooks
        {
            get { return _photoBooks; }
            set { SetProperty(ref _photoBooks, value); }
        }
        private PhotoBookV2 _selectedPhotoBook;
        public PhotoBookV2 SelectedPhotoBook
        {
            get { return _selectedPhotoBook; }
            set { SetProperty(ref _selectedPhotoBook, value); }
        }
    }
}

