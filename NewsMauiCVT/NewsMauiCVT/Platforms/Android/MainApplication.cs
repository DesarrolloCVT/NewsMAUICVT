using Android.App;
using Android.Runtime;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using NewsMauiCVT.Platforms.Android;

namespace NewsMauiCVT
{
    [Application(UsesCleartextTraffic = true)]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
            DependencyService.Register<IGetSSID, GetSSIDAndroid>();
            DependencyService.Register<IAudio, AudioService>();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
