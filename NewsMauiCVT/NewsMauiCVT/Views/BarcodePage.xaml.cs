using ZXing.Net.Maui.Controls;
using ZXing.Net.Maui;

namespace NewsMauiCVT.Views;

public partial class BarcodePage : ContentPage
{
    public static bool _flag;
    public static string _codigoDeBarras;

    public bool Flag
    {
        get => _flag;
        set => _flag = value;
    }
    public static string CodigoDeBarras
    {
        get => _codigoDeBarras;
        set
        {
            _codigoDeBarras = value;
        }
    }
    public BarcodePage()
	{
		InitializeComponent();
        barcodeView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            AutoRotate = true,
            Multiple = true
        };
    }
    public string Set_txt_Barcode()
    {   
        return CodigoDeBarras;
    }
    public void CleanData()
    {
        Flag = false;
        CodigoDeBarras = string.Empty;
    }
    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        Flag = true;
        foreach (var barcode in e.Results)
            Console.WriteLine($"Barcodes: {barcode.Format} -> {barcode.Value}");

        var first = e.Results?.FirstOrDefault();
        if (first is not null)
        {
            CodigoDeBarras = first.Value;
            Dispatcher.Dispatch(() =>
            {
                // Update BarcodeGeneratorView
                barcodeGenerator.ClearValue(BarcodeGeneratorView.ValueProperty);
                barcodeGenerator.Format = first.Format;
                barcodeGenerator.Value = first.Value;
                // Update Label
                ResultLabel.Text = $"Barcodes: {first.Format} -> {first.Value}";
                Application.Current?.MainPage?.Navigation.PopModalAsync();
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