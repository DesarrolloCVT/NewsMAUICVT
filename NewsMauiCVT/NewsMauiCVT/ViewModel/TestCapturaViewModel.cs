using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZXing;
using ZXing.Net.Maui;
using DevExpress.Maui.Core.Internal;

namespace NewsMauiCVT.ViewModel
{
    public class TestCapturaViewModel : INotifyPropertyChanged
    {
        private string _result;

        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public Command<ZXing.Result> ButtonCommand { get; private set; }

        //public ICommand ButtonCommand { get; private set; }

        public TestCapturaViewModel() 
        {
            //ButtonCommand = new Command<ZXing.Result>((x) => OnButtomCommand(x), (x) => true);
            //ButtonCommand = new Command(OnButtomCommand); 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        private void OnButtomCommand()
        {
            //CodigoQr = paramResultado.Text;
        }

        #region ##Old Code##
        /*var options = new MobileBarcodeScanningOptions();
        options.PossibleFormats = new List<ZXing.BarcodeFormat>
        {
            BarcodeFormat.QR_CODE,
            BarcodeFormat.Code128,
            BarcodeFormat.Ean13,
            BarcodeFormat.Code39,
            BarcodeFormat.Aztec,
            BarcodeFormat.All_1D,
            BarcodeFormat.Code93,
            BarcodeFormat.DataMatrix,
            BarcodeFormat.Ean8,
            BarcodeFormat.MaxiCode,
            BarcodeFormat.Imb,
            BarcodeFormat.Itf,
            BarcodeFormat.Msi,
            BarcodeFormat.Pdf417,
            BarcodeFormat.Plessey,
            BarcodeFormat.Rss14,
            BarcodeFormat.UpcEanExtension,
        };

        var page = new ZXingScannerPage(options) { Title = "SCANNER" };
        var closeItem = new ToolbarItem { Text = "CERRAR" };
        closeItem.Clicked += (object sender, EventArgs e) =>
        {
            page.IsScanning = false;
            Device.BeginInvokeOnMainThread(
                () =>
                {
                    Application.Current.MainPage.Navigation.PopModalAsync();
                });
        };
        page.ToolbarItems.Add(closeItem);
        page.OnScanResult += (result) =>
        {
            page.IsScanning = false;

            Device.BeginInvokeOnMainThread(
                () =>
                {
                    Application.Current.MainPage.Navigation.PopModalAsync();
                    if(string.IsNullOrEmpty(result.Text))
                    {
                        Result = "Codigo no valido en Scanner";
                    } else
                    {
                        Result = $"{result.Text}";
                    }
                });
        };
        Application.Current.MainPage.Navigation
            .PushModalAsync(
                new NavigationPage(page) { BarTextColor = Colors.White, BarBackgroundColor = Colors.CadetBlue },
                true);*/
        #endregion
    }
}
