﻿using Android.Content;
using Android.Net.Wifi;
using Android.App;
using Microsoft.Maui.Controls.PlatformConfiguration;
using NewsMauiCVT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsMauiCVT.Platforms.Android;
using AndroidApp = Android.App.Application;

namespace NewsMauiCVT.Platforms.Android
{   
    public class GetSSIDAndroid : IGetSSID
    {
        public string GetSSID()
        {
            WifiManager? wifiManager = AndroidApp.Context.GetSystemService(Context.WifiService) as WifiManager;
            WifiInfo currentWifi = wifiManager.ConnectionInfo;
            try
            {   
                if (currentWifi != null)
                {
                    String wifiSSID = currentWifi.SSID;
                    return wifiSSID;
                    // Now you have the SSID!
                }
                else
                {
                    Console.WriteLine("El valor de currentWifi es: " + currentWifi.SSID);
                    return "NULL";
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.ToString());
                return currentWifi.SSID;
            }
        }
    }
}
