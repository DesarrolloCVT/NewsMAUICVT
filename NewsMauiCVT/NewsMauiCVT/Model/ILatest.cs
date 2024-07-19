using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Model
{
    public interface ILatest
    {
        string InstalledVersionNumber { get; }
        //Task<string> GetLatestVersionNumber();
        ////Task<string> GetLatestVersionNumber(string appName);
        Task<bool> IsUsingLatestVersion();
        Task OpenAppInStore();
        Task OpenAppInStore(string appName);
    }
}
