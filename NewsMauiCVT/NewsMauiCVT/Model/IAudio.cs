using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Model
{
    public interface IAudio : IAudioManager
    {
        public async void PlayAudioFile(string fileName)
        {
            CreatePlayer(await FileSystem.OpenAppPackageFileAsync(fileName));
        }
    }
}
