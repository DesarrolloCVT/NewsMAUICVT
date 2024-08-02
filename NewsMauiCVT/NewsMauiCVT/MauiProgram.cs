using ZXing.Net.Maui.Controls;
using CommunityToolkit.Maui;
using Controls.UserDialogs.Maui;
using Microsoft.Extensions.Logging;
using DevExpress.Maui;
using Plugin.Maui.Audio;

namespace NewsMauiCVT
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            MauiAppBuilder mauiAppBuilder = builder.UseMauiApp<App>();
            mauiAppBuilder.UseUserDialogs(() =>
            {
                //setup your default styles for dialogs
                AlertConfig.DefaultBackgroundColor = Colors.Purple;
#if ANDROID
                AlertConfig.DefaultMessageFontFamily = "OpenSans-Regular.ttf";
#else
                    AlertConfig.DefaultMessageFontFamily = "OpenSans-Regular";
#endif
                ToastConfig.DefaultCornerRadius = 15;
            }).ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseDevExpressEditors()
            .UseDevExpress()
            .UseDevExpressCollectionView()
            .UseDevExpressControls()
            .UseDevExpressDataGrid()
            .UseMauiCommunityToolkit()
            .UseBarcodeReader();
#if DEBUG
            /*builder.Logging.AddDebug();
            builder.UseMauiApp<App>().UseDevExpress(useLocalization: false).UseDevExpressCollectionView().UseDevExpressControls().UseDevExpressDataGrid().UseDevExpressEditors().UseMauiCommunityToolkit();*/
#endif
            builder.Services.AddSingleton(AudioManager.Current);
            builder.Services.AddTransient<MainPage>();
            return builder.Build();
        }
    }
}