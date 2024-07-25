using Android.AdServices.AdIds;
using Android.Media;
using NewsMauiCVT.Model;
using NewsMauiCVT.Platforms.Android;
using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAudio = NewsMauiCVT.Model.IAudio;


namespace NewsMauiCVT.Platforms.Android
{   
    public class AudioService : IAudio
    {
        private readonly IAudioManager audioManager;
        public AudioService()
        {
            audioManager = new Plugin.Maui.Audio.AudioManager();
        }
        public async void PlayAudioFile(string fileName)
        {
            var audioPlayer = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(fileName));
            audioPlayer.Play();
        }
    }
}
