using System;
using System.Collections.Generic;
using System.Threading.Tasks;


using Microsoft.Maui.Platform;

namespace Fm.Shared.Views
{
    public partial class VideoFullPage 
    {
       
        //Template selectedTemplate;

        //private ISetOrientationService _setOrientationService;

        private string videoBlob;
        

        public VideoFullPage(string videoBlob)
        {

            
            
#if ANDROID
                //handler.PlatformView.SetSelectAllOnFocus(true);
                
                CommunityToolkit.Maui.Core.Handlers.MediaElementHandler.PropertyMapper.AppendToMapping("androidfullpage", (handler, player) =>
                {
                    handler.PlatformView.GetFirstChildOfType<Com.Google.Android.Exoplayer2.UI.StyledPlayerView>().FullscreenButtonClick += PlayerView_FullscreenButtonClick;
                    
                    //handler.PlatformView.GetFirstChildOfType<Com.Google.Android.Exoplayer2.UI.StyledPlayerView>()?.FullscreenButtonClick += 
                });
#endif
           

            InitializeComponent();
            //_setOrientationService = SharedLib.Helpers.ServiceHelper.GetService<ISetOrientationService>();
            this.videoBlob = videoBlob;
            //var filePath = Path.Combine(FileSystem.AppDataDirectory, "media/" + this.videoBlob);
            //Add 100 so the buttons is outsie the video view
            //this.MediaElement.HeightRequest = Fm.SharedLib.Converters.Height16_9FromWidthConverter.HeightFromWidth16_9() + 100;
            this.MediaElement.Source = new Uri(videoBlob);
           

        }
        private bool _isFullScreen;
#if ANDROID

        private void PlayerView_FullscreenButtonClick(object? sender, Com.Google.Android.Exoplayer2.UI.StyledPlayerView.FullscreenButtonClickEventArgs e)
        {
            if (!_isFullScreen)
            {
                _isFullScreen = true;
                //this.FullScreenToggleCtl.Text = SharedLibIconFonts.Icon_fullscreen_exit;
                //_setOrientationService.OnlyLandscape();
                //this.MediaElement.HeightRequest = Fm.SharedLib.Converters.Height16_9FromWidthConverter.HeightFromWidth16_9() - 80;
            }
            else
            {
                _isFullScreen = false;
                //this.FullScreenToggleCtl.Text = SharedLibIconFonts.Icon_fullscreen;
                //_setOrientationService.OnlyPortrait();
                //this.MediaElement.HeightRequest = Fm.SharedLib.Converters.Height16_9FromWidthConverter.HeightFromWidth16_9();
            }
        }
#endif

       
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
        void MediaElementControl_Unloaded(System.Object sender, System.EventArgs e)
        {
            
            this.MediaElement.Handler?.DisconnectHandler();
        }
        void MediaElement_MediaEnded(System.Object sender, System.EventArgs e)
        {

           // Globals.TrackEvent(AnalyticsEvents.Video, AnalyticsParameter.MediaPlayed, this.videoBlob);
        }
        async void VideoScreenExit(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
        {
            //if (_isFullScreen)
            //    _setOrientationService.OnlyPortrait();

            await Shell.Current.Navigation.PopModalAsync();

        }
    }
}
