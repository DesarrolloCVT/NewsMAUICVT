using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class TestCapturaCodeCam : ContentPage
{
    public TestCapturaCodeCam()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        #region C�digo para cargar p�gina de Scan BarCode desde el tel�fono.
        BarcodePage barcodePage = new BarcodePage();
        #endregion
        base.OnAppearing();
        lblProducto.Text = string.Empty;
        lblCodPro.Text = string.Empty;
        #region C�digo para cargar p�gina de Scan BarCode desde el tel�fono.
        if (DeviceInfo.Model != "MC33")
        {
            btn_escanear.IsVisible = true;
            btn_escanear.IsEnabled = true;
            if (barcodePage.Flag && barcodePage.CodigoDetectado) //True
            {
                txtCodigo.Text = barcodePage.Set_txt_Barcode(); //Set text -> Codigo de barras recuperado.
                barcodePage.SetFlag(); // -> Set Flag => False.
                barcodePage.CodigoDetectado = false;
            }
        }
        #endregion
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        DatosProductosCaptura dt = new DatosProductosCaptura();

        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            List<ProductosCapturaCod> pr = dt.DatosProductos(txtCodigo.Text);
            int con = pr.Count;

            if (con != 0)
            {
                foreach (var t in pr)
                {
                    lblProducto.Text = t.Producto.ToString();
                    lblCodPro.Text = t.Codigo.ToString();
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Codigo No Existe", "Aceptar");
                lblProducto.Text = string.Empty;
                lblCodPro.Text = string.Empty;

            }


        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }

    }
    private void OnKeyDown()
    {
#if ANDROID
        var imm = (Android.Views.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(Android.Content.Context.InputMethodService);
        if (imm != null)
        {
            var activity = Platform.CurrentActivity;
            Android.OS.IBinder wToken = activity.CurrentFocus?.WindowToken;
            imm.HideSoftInputFromWindow(wToken, 0);
        }
#endif
    }
    protected override bool OnBackButtonPressed()
    {
        OnKeyDown();
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
    private void Button_Scanner_Clicked(object sender, EventArgs e)
    {
        BarcodePage barcodePage = new BarcodePage();
        barcodePage.Flag = true;

        OnKeyDown();
        Application.Current?.MainPage?.Navigation
            .PushModalAsync(new NavigationPage(new BarcodePage())
            { BarTextColor = Colors.White, BarBackgroundColor = Colors.CadetBlue }, true);
    }
}