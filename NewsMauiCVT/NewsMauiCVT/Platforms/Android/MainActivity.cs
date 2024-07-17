using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Controls.UserDialogs.Maui;

namespace NewsMauiCVT
{
    [Activity(
        Theme = "@style/Maui.SplashTheme",
        MainLauncher = true,
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize |
        ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        const int ResqId = 0;

        readonly string[] Permission =
        {
            Android.Manifest.Permission.AccessFineLocation,
            Android.Manifest.Permission.AccessCoarseLocation,
            Android.Manifest.Permission.LocationHardware,
            Android.Manifest.Permission.ManageExternalStorage,
            Android.Manifest.Permission.ReadExternalStorage,
            Android.Manifest.Permission.WriteExternalStorage,
        };

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            RequestPermissions(Permission, ResqId);
            //DevExpress.Mobile.Forms.Init();
            DevExpress.Maui.DataGrid.Initializer.Init();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
