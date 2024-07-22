using Android.Content;
using Microsoft.Maui.Controls.PlatformConfiguration;
using NewsMauiCVT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidApp = Android.App.Application;

namespace NewsMauiCVT.Platforms.Android
{
    public class SettingServiceGP : ISettingsServiceAcGP
    {
        public SettingServiceGP()
        { }
        public void OpenSettingsLocali()
        {
            //AndroidApp.Context.StartActivity(new Intent(Android.Provider.ActionLocat‌​ionSourceSettings));
        }


    }
}
