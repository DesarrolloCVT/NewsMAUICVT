using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMM_ConsultaProducto : ContentPage
{
    string codigoRecibido;
    public SMM_ConsultaProducto()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        lblError2.Text = string.Empty;
        lblError2.IsVisible = false;
        txt_pallet.Focus();
    }
    public SMM_ConsultaProducto(string cod) :this()
    {
        codigoRecibido = cod;
    }
    protected override void OnAppearing()
    {
        #region C�digo para cargar p�gina de Scan BarCode desde el tel�fono.
        BarcodePage barcodePage = new BarcodePage();
        #endregion
        base.OnAppearing();
        txt_pallet.Focus();
        lblError2.Text = string.Empty;
        lblError2.IsVisible = false;
        txt_pallet.Text = string.Empty;
        lblProducto.Text = string.Empty;
        lblPrecio.Text = string.Empty;
        lblUM.Text = string.Empty;
        lblCantUM.Text = string.Empty;
        lblGrupArt.Text = string.Empty;
        lblEstado.Text = string.Empty;
        txt_pallet.Text = codigoRecibido;
        #region C�digo para cargar p�gina de Scan BarCode desde el tel�fono.
        if (DeviceInfo.Model != "MC33")
        {
            btn_Escanear.IsVisible = true;
            btn_Escanear.IsEnabled = true;
            if (barcodePage.Flag && barcodePage.CodigoDetectado) //True
            {
                txt_pallet.Text = barcodePage.Set_txt_Barcode(); //Set text -> Codigo de barras recuperado.
                barcodePage.SetFlag(); // -> Set Flag => False.
                barcodePage.CodigoDetectado = false;
            }
        }
        #endregion
    }
    private void Txt_pallet_Completed(object sender, EventArgs e)
    {

        DatosProductosSMM dti = new DatosProductosSMM();
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            string codpro = dti.ValidaProductoSMM(txt_pallet.Text);
            if (txt_pallet.Text.Equals(string.Empty))
            {
                lblError2.Text = "ingrese Codigo Producto";
                lblError2.IsVisible = true;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                txt_pallet.Focus();
            }
            else if (codpro.Equals(""))
            {
                lblError2.IsVisible = true;
                txt_pallet.Focus();
                lblError2.Text = "Cod.Prodcuto No Existe";
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            }
            else
            {
                List<ValidadorProductosSMMClass> ls = dti.ListDatoProductosSMMValida(txt_pallet.Text);
                foreach (var t in ls)
                {
                    lblProducto.Text = t.ItemCode + " -  " + t.ItemName;
                    lblPrecio.Text = "Precio: " + t.Price;
                    lblUM.Text = "Uni.Medida: " + t.UM;
                    lblCantUM.Text = "Cant.xUM: " + t.CantxUM;
                    lblGrupArt.Text = "Grup. Articulo: " + t.GrupoArticulo;
                    lblUnidades.Text = "Stock unidades Sala:" + t.StockUnidadesSala;


                    if (t.Estado.Equals("Activo"))
                    {
                        lblEstado.TextColor = Colors.Blue;
                        lblEstado.Text = "Estado :" + t.Estado;

                    }
                    else
                    {
                        lblEstado.TextColor = Colors.Red;
                        lblEstado.Text = "Estado :" + t.Estado;
                    }
                }

                lblError2.Text = string.Empty;
                lblError2.IsVisible = false;
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private void Btn_Limpiar_Clicked(object sender, EventArgs e)
    {
        lblError2.Text = string.Empty;
        lblError2.IsVisible = false;
        txt_pallet.Text = string.Empty;
        lblProducto.Text = string.Empty;
        lblPrecio.Text = string.Empty;
        lblUM.Text = string.Empty;
        lblCantUM.Text = string.Empty;
        lblGrupArt.Text = string.Empty;
        lblEstado.Text = string.Empty;
        lblUnidades.Text = string.Empty;
        txt_pallet.Focus();
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
    private void Btn_Escanear_Clicked(object sender, EventArgs e)
    {
        BarcodePage barcodePage = new BarcodePage();
        barcodePage.Flag = true;

        OnKeyDown();
        Application.Current?.MainPage?.Navigation
            .PushModalAsync(new NavigationPage(new BarcodePage())
            { BarTextColor = Colors.White, BarBackgroundColor = Colors.CadetBlue }, true);
    }
}