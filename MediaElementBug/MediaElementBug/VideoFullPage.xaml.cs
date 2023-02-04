using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Platform;


namespace Fm.SharedLib.Views
{
    [QueryProperty(nameof(VideoBlob), nameof(VideoBlob))]
    public partial class VideoFullPage 
    {
       // private ISetOrientationService _setOrientationService;
        private string _videoBlob;

        public string VideoBlob
        {
            get { return _videoBlob; }
            set { _videoBlob = value; }
        }


        public VideoFullPage()
        {


            InitializeComponent();
           // _setOrientationService = SharedLib.Helpers.ServiceHelper.GetService<ISetOrientationService>();



        }
        private bool _isFullScreen;


        //void ToggleFullScreen(System.Object sender, System.EventArgs e)
        //{
        //    if (!_isFullScreen)
        //    {
        //        _isFullScreen = true;
        //        this.FullScreenToggleCtl.Text = SharedLibIconFonts.Icon_fullscreen_exit;
        //        _setOrientationService.OnlyLandscape();
        //        this.MediaElement.HeightRequest = Fm.SharedLib.Converters.Height16_9FromWidthConverter.HeightFromWidth16_9() - 80;
        //    }
        //    else
        //    {
        //        _isFullScreen = false;
        //        this.FullScreenToggleCtl.Text = SharedLibIconFonts.Icon_fullscreen;
        //        _setOrientationService.OnlyPortrait();
        //        this.MediaElement.HeightRequest = Fm.SharedLib.Converters.Height16_9FromWidthConverter.HeightFromWidth16_9();
        //    }

        //}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //this.videoBlob = VideoBlob;
            //var filePath = Path.Combine(FileSystem.AppDataDirectory, "media/" + this.VideoBlob);
            //Add 100 so the buttons is outsie the video view
            //this.MediaElement.HeightRequest = Fm.SharedLib.Converters.Height16_9FromWidthConverter.HeightFromWidth16_9() + 100;
            this.MediaElement.Source = new Uri(this.VideoBlob); ;
        }
        void MediaElementControl_Unloaded(System.Object sender, System.EventArgs e)
        {

            this.MediaElement.Handler?.DisconnectHandler();
        }
        void MediaElement_MediaEnded(System.Object sender, System.EventArgs e)
        {

            //Globals.TrackEvent(AnalyticsEvents.Video, AnalyticsParameter.MediaPlayed, this.VideoBlob);
        }
        async void VideoScreenExit(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
        {
            //if (_isFullScreen)
            //    _setOrientationService.OnlyPortrait();

            await Shell.Current.GoToAsync("..");  //Shell.Current.Navigation.PopModalAsync();

        }
    }
}
