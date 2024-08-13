using ZXing.Net.Maui.Controls;
using ZXing.Net.Maui;

namespace NewsMauiCVT.Views;

public partial class BarcodePage : ContentPage
{
    public static bool _flag;
    public static string _codigoDeBarras;
    public static bool _codigoDetectado;

    public bool CodigoDetectado
    {
        get => _codigoDetectado; 
        set 
        {
            _codigoDetectado = value;
            OnPropertyChanged(nameof(CodigoDetectado));
        } 
    }
    public bool Flag
    {
        get => _flag;
        set
        {
            _flag = value;
            OnPropertyChanged(nameof(Flag));
        }
    }
    public string CodigoDeBarras
    {
        get => _codigoDeBarras;
        set
        {
            _codigoDeBarras = value;
            OnPropertyChanged(nameof(CodigoDeBarras));
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
    public void SetFlag()
    {
        Flag = false;
    }
    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {   
        foreach (var barcode in e.Results)
            Console.WriteLine($"Barcodes: {barcode.Format} -> {barcode.Value}");

        var first = e.Results?.FirstOrDefault();
        if (first is not null)
        {
            CodigoDetectado = true;
            CodigoDeBarras = first.Value;
            Dispatcher.Dispatch(() =>
            {
                // Update BarcodeGeneratorView
                barcodeGenerator.ClearValue(BarcodeGeneratorView.ValueProperty);
                barcodeGenerator.Format = first.Format;
                barcodeGenerator.Value = first.Value;

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
    protected override bool OnBackButtonPressed()
    {
        CodigoDetectado = false;
        return false;
    }
}