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
using NewsMauiCVT.Views;
using BarcodeFormat = ZXing.Net.Maui.BarcodeFormat;
using ZXing.Net.Maui.Controls;
using System.Reflection.Emit;

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
                //OnPropertyChanged(nameof(Result));
            }
        }
        public ICommand ButtonCommand { get; private set; }

        public TestCapturaViewModel() 
        {
            ButtonCommand = new Command(OnButtomCommand);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /*private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {   
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/

        private void OnButtomCommand()
        {
            var scanPage = new ContentPage() 
            {   
                Title = "SCANNER"
            };

            var cameraBarcodeReaderView = new CameraBarcodeReaderView
            {
                HorizontalOptions =  LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };

            var listado = new List<BarcodeFormat> 
            { 
                BarcodeFormat.QrCode,
                BarcodeFormat.Code128,
                BarcodeFormat.Ean13,
                BarcodeFormat.Code39,
                BarcodeFormat.Aztec,
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
                BarcodeFormat.UpcEanExtension
            };
            cameraBarcodeReaderView.IsTorchOn = true;
            scanPage.Content = cameraBarcodeReaderView;

            cameraBarcodeReaderView.CameraLocation = CameraLocation.Rear;

            cameraBarcodeReaderView.BarcodesDetected += (sender, e) =>
            {
                var first = e.Results?.FirstOrDefault();
                if (first is null)
                {
                    return;
                }

                scanPage.Dispatcher.Dispatch(() => {
                    Application.Current.MainPage.Navigation.PopModalAsync();
                    if (string.IsNullOrEmpty(first.ToString()))
                    {
                        Result = "Codigo no valido en Scanner";
                    }
                    else
                    {
                        Result = $"{first.ToString()}";
                    }
                });
            };

            Application.Current.MainPage.Navigation
            .PushModalAsync(
                new NavigationPage(scanPage) { BarTextColor = Colors.White, BarBackgroundColor = Colors.CadetBlue },
                true);
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
