using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMM_Posicionamiento : ContentPage
{
    public SMM_Posicionamiento()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        #region Inicializadores
        btn_generar.IsEnabled = false;
        LayoutDestinoExistente.IsVisible = false;
        LayoutOrigen.IsVisible = false;
        #endregion
        txt_origen.Focus();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
        SetFocusText();
        LogUsabilidad("Ingreso");
    }
    private void SetFocusText()
    {
        _ = Task.Delay(200).ContinueWith(t => {
            txt_origen.Focus();
        });
    }
    void ClearComponent()
    {
        lblError.Text = string.Empty;
        lblError2.Text = string.Empty;
        lblError.IsVisible = false;
        lblError2.IsVisible = false;
        txt_origen.Text = string.Empty;
        lbl_codproducto.Text = string.Empty;
        lbl_producto.Text = string.Empty;
        lbl_lote.Text = string.Empty;
        lbl_cantidad.Text = string.Empty;
        lbl_sitio.Text = string.Empty;
        lbl_ubicacion.Text = string.Empty;
        txt_destino.IsVisible = false;
        txt_destino.Text = string.Empty;
        lbl_sitio_nuevo.Text = string.Empty;
        lbl_ubicacion_nueva.Text = string.Empty;
        LayoutOrigen.IsVisible = false;
        LayoutDestinoExistente.IsVisible = false;
        btn_generar.IsEnabled = false;
    }
    private void Txt_origen_Completed(object sender, EventArgs e)
    {   
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            string nPallet = txt_origen.Text;

            DatosPosicionamientoSMM ps = new DatosPosicionamientoSMM();

            List<ListProdTranferSMMClass> lt = ps.ObtienedatosProducto(nPallet);
            if (lt.Count == 0)
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                lblError.IsVisible = true;
                lblError.Text = "N� de pallet no existe";
                txt_origen.Text = string.Empty;
                txt_origen.Focus();
                btn_generar.IsEnabled = false;
                ClearComponent();
            }
            else
            {
                foreach (var p in lt)
                {
                    LayoutOrigen.IsVisible = true;
                    txt_destino.IsVisible = true;

                    lbl_codproducto.Text = "Cod.Producto: " + p.ItemCode;
                    lbl_producto.Text = "Producto: " + p.ItemName;
                    lbl_sitio.Text = "Bodega: " + p.Site_Description;

                    string LayDesc = ps.TraeLayoutDesc(p.Layout_Id);
                    lbl_ubicacion.Text = "Ubicacion Actual(Pasillo x Fila): " + LayDesc;

                    lbl_lote.Text = "Lote: " + p.Package_Lot;
                    lbl_cantidad.Text = "Cantidad: " + p.Package_Quantity.ToString();
                    lblError.IsVisible = false;
                    lblError.Text = string.Empty;
                }

                txt_destino.Focus();
            }

        }
        else
        {

            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe conectarse a una red local ", "Aceptar");
        }
    }
    private void Txt_destino_Completed(object sender, EventArgs e)
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            lblError2.Text = string.Empty;
            lblError2.IsVisible = false;

            try
            {

                DatosPosicionamientoSMM dp = new DatosPosicionamientoSMM();
                LayoutDestinoExistente.IsVisible = true;

                string lyDes = dp.TraeLayoutDesc(Convert.ToInt32(txt_destino.Text));
                string NomSitio = dp.TraeBodegaxLayout(Convert.ToInt32(txt_destino.Text));

                lbl_ubicacion_nueva.Text = "Ubicacion Nueva(Pasillo x Fila): " + lyDes;
                lbl_sitio_nuevo.Text = "Bodega: " + NomSitio;

                if (lbl_sitio.Text == lbl_sitio_nuevo.Text)
                {
                    btn_generar.IsEnabled = true;
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    btn_generar.IsEnabled = false;
                    txt_destino.Text = string.Empty;
                    txt_destino.Focus();
                    lbl_sitio_nuevo.Text = string.Empty;
                    lbl_ubicacion_nueva.Text = string.Empty;
                    lblError2.Text = "No se puede ingresar en este destino";
                    lblError2.IsVisible = true;
                }
            }
            catch
            {
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private void Btn_generar_Clicked(object sender, EventArgs e)
    {
        if (txt_origen.Text.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Ingrese un n� de Pallet", "Aceptar");
            txt_origen.Focus();
        }
        else if (txt_destino.Text.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Ingrese Destino", "Aceptar");
            txt_destino.Focus();
        }
        else
        {

            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                DatosPosicionamientoSMM dp = new DatosPosicionamientoSMM();
                int Package_Id = dp.ObtienePackageIdPosicionamiento(txt_origen.Text);
                bool resActLayPack = dp.ActualizaLayoutPackage(Package_Id, Convert.ToInt32(txt_destino.Text));

                if (resActLayPack == true)
                {
                    bool addloc = dp.AddLocation(Package_Id, Convert.ToInt32(txt_destino.Text), App.Iduser);

                    if (addloc == true)
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("Correcto.mp3");
                        LogUsabilidad("Posicionamiento Registrado");
                        DisplayAlert("Alerta", "Registrado", "Aceptar");
                        ClearComponent();
                    }
                    else
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        DisplayAlert("Alerta", "No se puede Actualizar Posicion favor contactar con administrador", "Aceptar");
                    }
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    DisplayAlert("Alerta", "No se puede Actualizar Posicion favor contactar con administrador", "Aceptar");
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
    private void LogUsabilidad(string accion)
    {
        var Usuario = App.Iduser;
        var Fecha = DateTime.Now;
        var TipoRegistro = accion;
        var IdSubMenu = 332;

        DatosApp datosApp = new DatosApp();
        datosApp.LogUsabilidad(IdSubMenu, TipoRegistro);
    }
}