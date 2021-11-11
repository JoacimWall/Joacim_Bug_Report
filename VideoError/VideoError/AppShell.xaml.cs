using System;
using System.Collections.Generic;
using VideoError.ViewModels;
using VideoError.Views;
using Xamarin.Forms;

namespace VideoError
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
