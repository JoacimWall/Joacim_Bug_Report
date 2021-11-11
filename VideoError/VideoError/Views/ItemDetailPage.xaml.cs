using System.ComponentModel;
using Xamarin.Forms;
using VideoError.ViewModels;

namespace VideoError.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
