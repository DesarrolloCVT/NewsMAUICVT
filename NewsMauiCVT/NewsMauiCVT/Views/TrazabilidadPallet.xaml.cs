using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class TrazabilidadPallet : ContentPage
{
    public TrazabilidadPallet()
    {   
        InitializeComponent();
        lblError.Text = string.Empty;
        lblError.IsVisible = false;
        txtNPallet.Focus();
        GvData.IsVisible = false;
    }
    protected override void OnAppearing()
    {
        #region Código para cargar página de Scan BarCode desde el teléfono.
        BarcodePage barcodePage = new BarcodePage();
        #endregion
        txtNPallet.Text = "";
        base.OnAppearing();
        ClearComponent();
        txtNPallet.Focus();
        lblError.IsVisible = false;
        GvData.IsVisible = false;
        #region Código para cargar página de Scan BarCode desde el teléfono.
        if(barcodePage.Flag)
        {
            txtNPallet.Text = barcodePage.Set_txt_Barcode();
            barcodePage.CleanData();
        }
        if (DeviceInfo.Model != "MC33")
        {
            btn_escanear.IsVisible = true;
            btn_escanear.IsEnabled = true;
        }
        #endregion
    }
    void ClearComponent()
    {
        lblPallet.Text = string.Empty;
        lblLote.Text = string.Empty;
        lblCantidad.Text = string.Empty;
        lblCantReservada.Text = string.Empty;
        lblCodProd.Text = string.Empty;
        lblProducto.Text = string.Empty;
        lblBodega.Text = string.Empty;
        lblPosicion.Text = string.Empty;
        lblEstado.Text = string.Empty;
        GvData.IsVisible = false;

    }

    private void TxtNPallet_Completed(object sender, EventArgs e)
    {

        DatosTrazabilidadPallet tp = new DatosTrazabilidadPallet();
        List<TrazabilidadPaletClass> lt = new List<TrazabilidadPaletClass>();

        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {

            if (String.IsNullOrWhiteSpace(txtNPallet.Text))
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                lblError.IsVisible = true;
                lblError.Text = "Ingrese N° Pallet";
            }
            else if (!txtNPallet.Text.ToCharArray().All(Char.IsDigit))
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                lblError.IsVisible = true;
                lblError.Text = "Ingrese Solo Numeros";
                txtNPallet.Text = string.Empty;
                txtNPallet.Focus();
            }
            else
            {
                lblError.Text = string.Empty;
                lblError.IsVisible = false;
                lt = tp.BuscaTraabilidadPallet(Convert.ToInt32(txtNPallet.Text));

                if (lt.Count() == 0)
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    lblError.IsVisible = true;
                    lblError.Text = "N° de pallet no existe";
                    txtNPallet.Text = string.Empty;
                    txtNPallet.Focus();
                }
                else
                {
                    foreach (var t in lt)
                    {
                        GvData.IsVisible = true;

                        lblPallet.Text = "SSCC: " + t.Package_SSCC;
                        lblLote.Text = "Lote: " + t.Package_Lot;
                        lblCantidad.Text = "Cantidad: " + t.Package_Quantity;
                        lblCantReservada.Text = "Cant.Reservada: " + t.Package_ReserveQuantity;
                        lblCodProd.Text = "Cod.Producto: " + t.Codproducto;
                        lblProducto.Text = "Prodcuto: " + t.Articulo;
                        lblBodega.Text = "Bodega: " + t.Site;
                        lblPosicion.Text = "Ubicacion: " + t.Ubicacion;
                        lblEstado.Text = "Estado: " + t.Estado;

                        DataTable dt = tp.DetalleTrazabilidadPallet(Convert.ToInt32(txtNPallet.Text));
                        GvData.ItemsSource = dt;

                        GvData.Columns["FECHA"].Caption = "Fecha";
                        GvData.Columns["FECHA"].Width = 110;
                        GvData.Columns["Entidad"].Caption = "Entidad";
                        GvData.Columns["Entidad"].Width = 110;
                        GvData.Columns["Operacion"].Caption = "Operacion";
                        GvData.Columns["Operacion"].Width = 110;
                        GvData.RowHeight = 200;
                        GvData.Columns["Cantidad"].Caption = "Cantidad";
                        GvData.Columns["Cantidad"].Width = 110;
                        GvData.Columns["Staff_Name"].Caption = "Usuario";
                        GvData.Columns["Staff_Name"].Width = 110;
                        GvData.Columns["Package_SSCC"].IsVisible = false;
                    }
                }
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    protected override bool OnBackButtonPressed()
    {
        txtNPallet.Text = string.Empty;
        //return true to prevent back, return false to just do something before going back. 
        return false;
    }
    #region Código para cargar página de Scan BarCode desde el teléfono.
    private void Btn_escanear_Clicked(object sender, EventArgs e)
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
        Application.Current?.MainPage?.Navigation
            .PushModalAsync(new NavigationPage(new BarcodePage())
            { BarTextColor = Colors.White, BarBackgroundColor = Colors.CadetBlue }, true);

    }
    #endregion
}