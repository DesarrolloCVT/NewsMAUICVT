using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMM_Repaletizado : ContentPage
{
    public SMM_Repaletizado()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        btn_generar.IsEnabled = false;
        LayoutDestinoExistente.IsVisible = false;
        LayoutOrigen.IsVisible = false;
        txtPosicion.Focus();
    }
    protected override void OnAppearing()
    {
        #region Código para cargar página de Scan BarCode desde el teléfono.
        BarcodePage barcodePage = new BarcodePage();
        #endregion
        base.OnAppearing();
        ClearComponent();
        lblError.Text = string.Empty;
        lblError2.Text = string.Empty;
        lblError3.Text = string.Empty;
        lblError.IsVisible = false;
        lblError2.IsVisible = false;
        lblError3.IsVisible = false;
        #region Código para cargar página de Scan BarCode desde el teléfono.
        if (DeviceInfo.Model != "MC33")
        {
            btn_escanear.IsVisible = true;
            btn_escanear.IsEnabled = true;
            if (barcodePage.Flag && barcodePage.CodigoDetectado) //True
            {
                txtPosicion.Text = barcodePage.Set_txt_Barcode(); //Set text -> Codigo de barras recuperado.
                barcodePage.SetFlag(); // -> Set Flag => False.

            }
        }
        #endregion
    }
    private void TxtPosicion_Completed(object sender, EventArgs e)
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            int npallet = Convert.ToInt32(txtPosicion.Text);
            DatosRepaletizadoSMM dr = new DatosRepaletizadoSMM();

            List<SMMPackageClass> ls = dr.ObtieneDatosPaletSMM(npallet);

            if (ls.Count == 0)
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                lblError.IsVisible = true;
                lblError.Text = "N° de pallet no existe o no posicionado ";
                txtPosicion.Focus();
                txtPosicion.Text = string.Empty;
            }
            else
            {

                foreach (var p in ls)
                {
                    LayoutOrigen.IsVisible = true;

                    lbl_codproducto.Text = "Cod.Producto: " + p.ItemCode;
                    lbl_producto.Text = "Producto: " + dr.ObtieneNombreProducSMM(p.ItemCode);

                    int idSite = dr.ObtienSiteLayoutSMM(p.Layout_Id);
                    string vBodega = dr.ObtieneNombreSitio(idSite);

                    lblBodega.Text = "Bodega: " + vBodega;
                    lbl_lote.Text = "Lote: " + p.Package_Lot;
                    lbl_cantidad.Text = "Cantidad: " + p.Package_Quantity.ToString();

                    btn_generar.IsEnabled = false;
                    lblError.IsVisible = false;
                    lblError.Text = string.Empty;
                    picker.Focus();
                }
            }

        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex == 0)
        {
            txt_destino.IsVisible = false;
            LayoutDestinoExistente.IsVisible = false;
            lbl_dcantidad.Text = string.Empty;
            lbl_dcodproducto.Text = string.Empty;
            lbl_dproducto.Text = string.Empty;
            lbl_dlote.Text = string.Empty;
            lbl_dBodega.Text = string.Empty;
            txt_destino.Text = string.Empty;
            txt_cantidad.Focus();
        }
        else
        {

            txt_destino.IsVisible = true;
            txt_destino.Focus();

        }
    }
    private void Txt_destino_Completed(object sender, EventArgs e)
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {

            int nPalletdes = Convert.ToInt32(txt_destino.Text);
            DatosRepaletizadoSMM dr = new DatosRepaletizadoSMM();
            List<SMMPackageClass> dt = dr.ObtieneDatosPaletSMM(nPalletdes);
            if (dt.Count() == 0)
            {

                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                txt_destino.Focus();
                txt_destino.Text = string.Empty;
                lblError2.IsVisible = true;
                lblError2.Text = "N° de pallet no existe o no posicionado ";

            }
            else
            {
                foreach (var p in dt)
                {
                    LayoutDestinoExistente.IsVisible = true;
                    lbl_dcodproducto.Text = "Cod.Producto: " + p.ItemCode;
                    lbl_dproducto.Text = "Producto: " + dr.ObtieneNombreProducSMM(p.ItemCode);
                    int idSite = dr.ObtienSiteLayoutSMM(p.Layout_Id);
                    string vBodega = dr.ObtieneNombreSitio(idSite);

                    lbl_dBodega.Text = "Bodega: " + vBodega;
                    lbl_dlote.Text = "Lote: " + p.Package_Lot;
                    lbl_dcantidad.Text = "Cantidad: " + p.Package_Quantity.ToString();
                    lblError2.IsVisible = false;
                    lblError2.Text = string.Empty;
                }
                if (txtPosicion.Text != txt_destino.Text)
                {

                    if (lblBodega.Text != lbl_dBodega.Text)
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        DisplayAlert("Alerta", "NO COMPATIBLE : Pallet en bodega diferente o N° de pallet son identicos ", "Aceptar");
                        btn_generar.IsEnabled = false;
                    }
                    else if (lbl_dcodproducto.Text == lbl_codproducto.Text && lbl_dlote.Text == lbl_lote.Text)
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("Correcto.mp3");
                        DisplayAlert("Alerta", "COMPATIBLE", "Aceptar");
                        btn_generar.IsEnabled = false;
                        txt_cantidad.Focus();
                    }
                    else
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        DisplayAlert("Alerta", "NO COMPATIBLE : Pallet en bodega diferente o N° de pallet son identicos ", "Aceptar");
                        btn_generar.IsEnabled = false;
                    }

                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    DisplayAlert("Alerta", "NO COMPATIBLE : Pallet en bodega diferente o N° de pallet son identicos", "Aceptar");
                    btn_generar.IsEnabled = false;
                }

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
        int selectedIndex = picker.SelectedIndex;
        if (txtPosicion.Text.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Ingrese un n° de Pallet", "Aceptar");
            txtPosicion.Focus();
        }
        else
     if (txt_destino.Text.Equals(string.Empty) && selectedIndex == 1)
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Ingrese Destino", "Aceptar");
            txt_destino.Focus();
        }
        else
     if (txt_cantidad.Text.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Ingrese Cantidad", "Aceptar");
            txt_cantidad.Focus();
        }
        else
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                DatosRepaletizadoSMM dr = new DatosRepaletizadoSMM();
                int palOrigen = dr.VerificaReservaSMM(txtPosicion.Text);
                int palDest = dr.VerificaReservaSMM(txt_destino.Text);
                string cant = txt_cantidad.Text.Replace(".", ",");

                try
                {

                    if (selectedIndex == 1)
                    {
                        if (palOrigen == 0)
                        {
                            if (palDest == 0)
                            {
                                bool Creado = dr.RepaletizaDestino(txtPosicion.Text, txt_destino.Text, cant, App.Iduser);
                                if (Creado == true)
                                {
                                    DependencyService.Get<IAudio>().PlayAudioFile("Correcto.mp3");
                                    DisplayAlert("Alerta", "REPALETIZADO CON EXITO ", "Aceptar");
                                    #region Limpia Componentes
                                    ClearComponent();
                                    #endregion

                                }
                            }
                            else
                            {
                                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                DisplayAlert("Alerta", "Pallet destino posee reserva, no es posible realizar repaletizado", "Aceptar");
                            }
                        }
                        else
                        {
                            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                            DisplayAlert("Alerta", "Pallet Origen posee reserva, no es posible realizar repaletizado", "Aceptar");
                        }
                    }
                    else
                    {

                        try
                        {
                            if (Convert.ToInt32(lbl_cantidad.Text) >= Convert.ToInt32(txt_cantidad.Text))
                            {
                                if (palOrigen == 0)
                                {
                                    string numPalletNuevo = dr.RepaletizaNuevo(txtPosicion.Text, cant, App.Iduser);
                                    DisplayAlert("Alerta", " Su Nuevo Pallet es: " + numPalletNuevo, "Aceptar");
                                    #region Limpia Componentes                   
                                    ClearComponent();
                                    #endregion
                                }
                                else
                                {
                                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                    DisplayAlert("Alerta", "Pallet Origen posee reserva, no es posible realizar repaletizado", "Aceptar");
                                }
                            }
                            else
                            {
                                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                DisplayAlert("Alerta", "Excede cantidad permitida", "Aceptar");
                            }

                        }
                        catch
                        {
                        }
                    }

                }
                catch { }


            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }

        }
    }
    void ClearComponent()
    {

        txtPosicion.Text = string.Empty;
        txtPosicion.Focus();
        lbl_codproducto.Text = string.Empty;
        lbl_producto.Text = string.Empty;
        lbl_lote.Text = string.Empty;
        lbl_cantidad.Text = string.Empty;
        lblBodega.Text = string.Empty;
        picker.SelectedIndex = -1;
        txt_destino.IsVisible = false;
        txt_destino.Text = string.Empty;
        lbl_dproducto.Text = string.Empty;
        lbl_dlote.Text = string.Empty;
        lbl_dBodega.Text = string.Empty;
        lbl_dcantidad.Text = string.Empty;
        //txt_cantidad.Visible=false;
        txt_cantidad.Text = string.Empty;
        LayoutOrigen.IsVisible = false;
        LayoutDestinoExistente.IsVisible = false;
        btn_generar.IsEnabled = false;

    }
    private void Txt_cantidad_Completed(object sender, EventArgs e)
    {
        if (String.IsNullOrWhiteSpace(txt_cantidad.Text))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            lblError3.IsVisible = true;
            lblError3.Text = "ingrese Cantidad";

        }
        else
        {
            btn_generar.IsEnabled = true;
            lblError3.IsVisible = false;
            lblError3.Text = string.Empty;
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
    private void Btn_escanear_Clicked(object sender, EventArgs e)
    {
        BarcodePage barcodePage = new BarcodePage();
        barcodePage.Flag = true;

        OnKeyDown();
        Application.Current?.MainPage?.Navigation
            .PushModalAsync(new NavigationPage(new BarcodePage())
            { BarTextColor = Colors.White, BarBackgroundColor = Colors.CadetBlue }, true);
    }
}