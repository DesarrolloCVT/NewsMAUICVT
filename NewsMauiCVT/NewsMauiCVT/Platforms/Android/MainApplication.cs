using Android.App;
using Android.Runtime;
using NewsMauiCVT.Model;
using NewsMauiCVT.Platforms.Android;

namespace NewsMauiCVT
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
            DependencyService.Register<IGetSSID, GetSSIDAndroid>();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
