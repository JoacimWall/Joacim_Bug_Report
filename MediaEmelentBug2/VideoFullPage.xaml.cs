using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core.Handlers;
//using Fm.SharedLib.Resources.Theming;
using Microsoft.Maui.Platform;

namespace Fm.SharedLib.Views;
[XamlCompilation(XamlCompilationOptions.Compile)]
[QueryProperty(nameof(VideoBlob), nameof(VideoBlob))]
public partial class VideoFullPage 
{
#if ANDROID
   // private ISetOrientationService _setOrientationService;
#endif
    private string _videoBlob;

    public string VideoBlob
    {
        get { return _videoBlob; }
        set {  _videoBlob= value; }
    }


    public VideoFullPage()
    {


#if ANDROID
        //Snyggast hade varit att bara visa den i Modalt även på IOS men detta
        //finns en bug i mediaspelaren på IOS då krashar videospelaren. Anledningen till Modal är
        //att vi på android måste vara säkra på att endast en instans av mediaspelaren är igång samtidigt annars så
        //slutar den att fungera
        //Shell.SetPresentationMode(this, PresentationMode.ModalAnimated);
        MediaElementHandler.PropertyMapper.AppendToMapping("androidfullpage", (handler, player) =>
            {
                handler.PlatformView.GetFirstChildOfType<Com.Google.Android.Exoplayer2.UI.StyledPlayerView>().FullscreenButtonClick += PlayerView_FullscreenButtonClick;
            });
       // _setOrientationService = ServiceHelper.GetService<ISetOrientationService>();
#endif

        InitializeComponent();
#pragma warning disable CA1416
       // this.Behaviors.Add(new CommunityToolkit.Maui.Behaviors.StatusBarBehavior { StatusBarColor = AppColors.PageBackgroundColor, StatusBarStyle = CommunityToolkit.Maui.Core.StatusBarStyle.LightContent });

#if IOS
        //BoxViewExitScreen.IsVisible = false;
#endif
    }

#if ANDROID
    private bool _isFullScreen;
    private void PlayerView_FullscreenButtonClick(object sender, Com.Google.Android.Exoplayer2.UI.StyledPlayerView.FullscreenButtonClickEventArgs e)
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

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        //Vi pausar video på IOS på Android så ser vi den alltid är modal se konstruktor av denna klass.
#if IOS
            MediaElement.Pause();
#endif

    }

    private bool isFirstTimeLoad = true;
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (isFirstTimeLoad)
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "media/" + this.VideoBlob);
            
            //Add 100 so the buttons is outsie the video view
            //this.MediaElement.HeightRequest = Fm.SharedLib.Converters.Height16_9FromWidthConverter.HeightFromWidth16_9() + 100;
            if (File.Exists(filePath))
                 this.MediaElement.Source = new Uri(filePath);
            else
                this.MediaElement.Source = new Uri("https://qalexicon.blob.core.windows.net/mediaqa/" + this.VideoBlob);

            isFirstTimeLoad = false;
        }
    }
    protected override void OnDisappearing()
    {
#if ANDROID
        if (_isFullScreen)
            //_setOrientationService.OnlyPortrait();
#endif
        base.OnDisappearing();
    }
    void MediaElementControl_Unloaded(System.Object sender, System.EventArgs e)
    {
        
        this.MediaElement.Handler?.DisconnectHandler();
    }
    void MediaElement_MediaEnded(System.Object sender, System.EventArgs e)
    {

       // TietoGlobals.TrackEvent(AnalyticsEvents.Video.ToString(), AnalyticsParameter.MediaPlayed.ToString(), this.VideoBlob);
        VideoScreenExit(null, null);
    }
    async void VideoScreenExit(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
#if ANDROID
       // if (_isFullScreen)
            //_setOrientationService.OnlyPortrait();
#endif
        await Shell.Current.GoToAsync("..");  //Shell.Current.Navigation.PopModalAsync();

    }
}
