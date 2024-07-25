using DevExpress.Maui.Core.Internal;
using ZXing;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace NewsMauiCVT.Views;

public partial class CapturaCodigos : ContentPage
{

    public static readonly BindableProperty IsScanningProperty = BindableProperty.Create("IsScanning", typeof(bool), typeof(CapturaCodigos), false);
    public delegate void ScanResultDelegate(Result result);

    public CapturaCodigos()
	{
		InitializeComponent();
        barcodeView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            AutoRotate = true,
            Multiple = true
        };
    }

    public bool IsScanning
    {
        get
        {
            return (bool)GetValue(IsScanningProperty);
        }
        set
        {
            SetValue(IsScanningProperty, value);
        }
    }

    public event ScanResultDelegate OnScanResult;

    protected async void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        foreach (var barcode in e.Results)
            Console.WriteLine($"Barcodes: {barcode.Format} -> {barcode.Value}");

        var first = e.Results?.FirstOrDefault();
        if (first is not null)
        {
            Dispatcher.Dispatch(() =>
            {
                // Update BarcodeGeneratorView
                barcodeGenerator.ClearValue(BarcodeGeneratorView.ValueProperty);
                barcodeGenerator.Format = first.Format;
                barcodeGenerator.Value = first.Value;

                // Update Label
                ResultLabel.Text = $"Barcodes: {first.Format} -> {first.Value}";
            });
        }
    }
    void SwitchCameraButton_Clicked(object sender, EventArgs e)
    {
        barcodeView.CameraLocation = barcodeView.CameraLocation == CameraLocation.Rear ? CameraLocation.Front : CameraLocation.Rear;
    }
    void TorchButton_Clicked(object sender, EventArgs e)
    {
        barcodeView.IsTorchOn = !barcodeView.IsTorchOn;
    }
}