using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using Newtonsoft.Json;
using System.Net.Http;

namespace NewsMauiCVT.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Repaletizado : ContentPage
{
    private int _lblCantidad;
    private int lblCantidad
    {
        get => _lblCantidad;
        set
        {
            _lblCantidad = value;
            OnPropertyChanged(nameof(lblCantidad));
        }
    }    
    public Repaletizado()
	{   
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        #region Código para cargar página de Scan BarCode desde el teléfono.
        BarcodePage barcodePage = new BarcodePage();
        #endregion

        base.OnAppearing();
        SetFocusText();
        PreparePage();
        ClearComponent();

        #region Código para cargar página de Scan BarCode desde el teléfono.
        //Validacion para descartar el uso de la Pistola.
        if (DeviceInfo.Model != "MC33")
        {
            //Se hace visible boton escanear codigo
            btn_escanear.IsVisible = true;
            btn_escanear.IsEnabled = true;

            //validacion: Comprueba que la bandera esta arriba y se ha detectado un codigo antes de hacer el set del TextEdit.
            if (barcodePage.Flag && barcodePage.CodigoDetectado) //True
            {   
                if(txtPosicion.IsFocused)
                {
                    txtPosicion.Text = barcodePage.SetBarcode(); //Set text -> Codigo de barras recuperado.
                }
                else
                {
                    txt_destino.Text = barcodePage.SetBarcode(); //Set text -> Codigo de barras recuperado.
                }
                barcodePage.Flag = !barcodePage.Flag;
                barcodePage.CodigoDetectado = !barcodePage.CodigoDetectado;
            }
            #region Validacion: validación para dispositivo Celular, para utilizar datos pre-existentes. 
            ValidatePreviewData();
            #endregion
        }
        #endregion
    }
    //Validacion para posicionar el Foco en el TextEdit Posicion -> N° de pallet.
    private void SetFocusText()
    {
        _ = Task.Delay(200).ContinueWith(t => {
            txtPosicion.Focus();
        });
    }
    private async void TxtPosicion_Completed(object sender, EventArgs e)
    {
        //Realiza la validez del numero de pallet registrado.
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet) // Valida acceso a internet
        {
            string nPallet = txtPosicion.Text;
            HttpClient ClientHttp = new HttpClient();
            ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
            //Se realiza el call de a traves de metodo Get Uri. 
            var rest2 = ClientHttp.GetAsync("api/Produccion?NumeroDePallet=" + nPallet).Result; 

            if (rest2.IsSuccessStatusCode) //Se valida respuesta del Call
            {
                //Se lee y se almacena el resultado -> Objeto.
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                //Se deserializa el objeto json y se convierte en una lista.
                List<Package> dt = JsonConvert.DeserializeObject<List<Package>>(resultadoStr) ??
                                throw new InvalidOperationException();
                //Se valida si la respuesta contiene datos.
                if (dt.Count() == 0)
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    lblError.IsVisible = true;
                    lblError.Text = "N° de pallet no existe o no posicionado ";
                    txtPosicion.Focus();
                    txtPosicion.Text = string.Empty;
                }
                else
                {
                    foreach (var p in dt)
                    {
                        LayoutOrigen.IsVisible = true;
                        var rest = ClientHttp.GetAsync("api/Produccion/" + p.ArticleProvider_Id).Result;
                        var resultadoStr2 = rest.Content.ReadAsStringAsync().Result;
                        List<ArticleProvider> ap = JsonConvert.DeserializeObject<List<ArticleProvider>>(resultadoStr2) ??
                                throw new InvalidOperationException();
                        foreach (var a in ap)
                        {
                            lbl_codproducto.Text = "Cod.Producto: " + a.ArticleProvider_CodClient;
                            lbl_producto.Text = "Producto: " + a.ArticleProvider_Description;
                        }

                        var rest3 = ClientHttp.GetAsync("api/Bodega?layoutid=" + p.Layout_Id).Result;
                        var resultadoStr3 = rest3.Content.ReadAsStringAsync().Result;
                        int idSite = JsonConvert.DeserializeObject<int>(resultadoStr3);
                        var rest4 = ClientHttp.GetAsync("api/Bodega?siteid=" + idSite).Result;
                        var resultadoStr4 = rest4.Content.ReadAsStringAsync().Result;
                        string vBodega = JsonConvert.DeserializeObject<string>(resultadoStr4) ??
                                throw new InvalidOperationException();

                        lblBodega.Text = "Bodega: " + vBodega;
                        lbl_lote.Text = "Lote: " + p.Package_Lot;
                        lbl_cantidad.Text = "Cantidad: " + p.Package_Quantity.ToString();
                        lblCantidad = (int)p.Package_Quantity;
                        btn_generar.IsEnabled = false;
                        lblError.IsVisible = false;
                        lblError.Text = string.Empty;
                        picker.Focus();
                    }
                }
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
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

            DatosTipoPallet dti = new DatosTipoPallet();
            List<TipoPalletClass> dt2 = dti.ListaTipoPallet();
            List<TipoPall> lPro = new List<TipoPall>();

            cboTipoPallet.IsVisible = true;

            foreach (var s in dt2)
            {
                lPro.Add(new TipoPall { Supportive_Description = s.Supportive_Description });
            }
            cboTipoPallet.BindingContext = lPro;
            cboTipoPallet.Focus();
        }
        else if(selectedIndex == 1)
        {
            txt_destino.IsVisible = true;
            cboTipoPallet.IsVisible = false;
            txt_destino.Focus();
        }
    }
    public class TipoPall
    {
        public string Supportive_Description { get; set; }
    }
    private void Txt_destino_Completed(object sender, EventArgs e)
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            string nPalletdes = txt_destino.Text;
            HttpClient ClientHttp = new HttpClient();
            ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
            var rest2 = ClientHttp.GetAsync("api/Produccion?SSCC=" + nPalletdes).Result;

            if (rest2.IsSuccessStatusCode)
            {
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                List<Package> dt = JsonConvert.DeserializeObject<List<Package>>(resultadoStr) ??
                                throw new InvalidOperationException();
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
                        var rest = ClientHttp.GetAsync("api/Produccion/" + p.ArticleProvider_Id).Result;
                        var resultadoStr2 = rest.Content.ReadAsStringAsync().Result;
                        List<ArticleProvider> ap = JsonConvert.DeserializeObject<List<ArticleProvider>>(resultadoStr2) ??
                                throw new InvalidOperationException();

                        foreach (var a in ap)
                        {
                            lbl_dcodproducto.Text = "Cod.Producto: " + a.ArticleProvider_CodClient;
                            lbl_dproducto.Text = "Producto: " + a.ArticleProvider_Description;
                        }
                        var rest3 = ClientHttp.GetAsync("api/Bodega?layoutid=" + p.Layout_Id).Result;
                        var resultadoStr3 = rest3.Content.ReadAsStringAsync().Result;
                        int idSite = JsonConvert.DeserializeObject<int>(resultadoStr3);
                        var rest4 = ClientHttp.GetAsync("api/Bodega?siteid=" + idSite).Result;
                        var resultadoStr4 = rest4.Content.ReadAsStringAsync().Result;
                        string vBodega = JsonConvert.DeserializeObject<string>(resultadoStr4) ??
                                throw new InvalidOperationException();

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
                            //txt_cantidad.Focus();
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
            txt_cantidad.Focus();
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
            //txtPosicion.Focus();
        }
        else
        if (txt_destino.Text.Equals(string.Empty) && selectedIndex == 1)
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Ingrese Destino", "Aceptar");
            //txt_destino.Focus();
        }
        else
        if (txt_cantidad.Text.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Ingrese Cantidad", "Aceptar");
            //txt_cantidad.Focus();
        }
        else
        {   
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

                //Verifica q exista pallet de origen
                var rest = ClientHttp.GetAsync("api/Produccion?numPallet=" + txtPosicion).Result;
                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                int VeriResPalletOrigen = JsonConvert.DeserializeObject<int>(resultadoStr);

                //Verifica q exista pallet de destino
                var rest2 = ClientHttp.GetAsync("api/Produccion?numPallet=" + txt_destino).Result;
                var resultadoStr2 = rest2.Content.ReadAsStringAsync().Result;
                int VeriResPalletDestino = JsonConvert.DeserializeObject<int>(resultadoStr2);

                try
                {
                    if (selectedIndex == 1)
                    {
                        if (VeriResPalletOrigen == 0)
                        {
                            if (VeriResPalletDestino == 0)
                            {
                                //repaletizado Origen Existente
                                var rest3 = ClientHttp.GetAsync("api/Produccion?Origen=" + txtPosicion.Text + "&Destino=" + txt_destino.Text + "&Cantidad=" + txt_cantidad.Text + "&username=" + App.UserSistema.ToString()).Result;
                                if (rest3.IsSuccessStatusCode)
                                {
                                    var resultadoStr3 = rest3.Content.ReadAsStringAsync().Result;
                                    bool Creado = JsonConvert.DeserializeObject<bool>(resultadoStr3);

                                    if (Creado == true)
                                    {
                                        DependencyService.Get<IAudio>().PlayAudioFile("Correcto.mp3");
                                        DisplayAlert("Alerta", "REPALETIZADO CON EXITO ", "Aceptar");
                                        #region Limpia Componentes
                                        ClearComponent();
                                        #endregion
                                    }
                                    else
                                    {
                                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                        DisplayAlert("Alerta", "ERROR AL REPALETIZAR FAVOR VERIFICAR ", "Aceptar");
                                    }
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
                            Console.WriteLine("lbl_cantidad: " + lbl_cantidad.Text + " txt_cantidad: " + txt_cantidad.Text);
                            int txtCantidad = int.Parse(txt_cantidad.Text);

                            if (lblCantidad >= txtCantidad)
                            {
                                if (VeriResPalletOrigen == 0)
                                {
                                    //repalletizado Origen Nuevo
                                    var rest4 = ClientHttp.GetAsync("api/Produccion?BOrigen=" + txtPosicion.Text + "&Cantidad=" + Convert.ToInt32(txt_cantidad.Text) + "&NusuarioSis=" + App.UserSistema).Result;

                                    if (rest4.IsSuccessStatusCode)
                                    {
                                        var resultadoStr4 = rest4.Content.ReadAsStringAsync().Result;
                                        string NumPallet = JsonConvert.DeserializeObject<string>(resultadoStr4) ??
                                throw new InvalidOperationException();

                                        #region AccTipoPallet
                                        DatosTipoPallet dTip = new DatosTipoPallet();
                                        int IdTipoPal = dTip.TraeIdTipoPallet(cboTipoPallet.SelectedItem.ToString());
                                        dTip.ActualizaTipoPallet(Convert.ToInt32(NumPallet), IdTipoPal);
                                        #endregion

                                        DependencyService.Get<IAudio>().PlayAudioFile("Correcto.mp3");
                                        DisplayAlert("Alerta", " Su Nuevo Pallet es: " + NumPallet, "Aceptar");
                                        #region Limpia Componentes                   
                                        ClearComponent();
                                        #endregion
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
                                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                DisplayAlert("Alerta", "Excede cantidad permitida", "Aceptar");
                            }
                        }
                        catch (Exception ex) 
                        {
                            Console.WriteLine("Btn_generar_Clicked - Error 1: " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Btn_generar_Clicked - Error 2: " + ex.Message);
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }
    }
    private void PreparePage()
    {
        try
        {
            lblError.IsVisible = false;
            lblError2.IsVisible = false;
            lblError3.IsVisible = false;
            LayoutOrigen.IsVisible = false;
            LayoutDestinoExistente.IsVisible = false;
            btn_generar.IsEnabled = false;
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
    private async void ValidatePreviewData()
    {
        try {
            if(picker.SelectedIndex == 1 && txt_destino.IsEnabled == true)
            {
                var result = await DisplayAlert("Confirmar", "Se han detectado datos previamente ingresados, ¿Desea continuar con el registro?", "SI", "NO");
                if (result)
                {
                    Console.WriteLine("Se mantienen los datos");
                }
                else
                {
                    txt_destino.IsVisible = false;
                    picker.SelectedIndex = -1;
                    lblError.Text = string.Empty;
                    lblError2.Text = string.Empty;
                    lblError3.Text = string.Empty;
                    txtPosicion.Text = string.Empty;
                    lbl_codproducto.Text = string.Empty;
                    lbl_producto.Text = string.Empty;
                    lbl_lote.Text = string.Empty;
                    lbl_cantidad.Text = string.Empty;
                    lblBodega.Text = string.Empty;
                    txt_destino.Text = string.Empty;
                    lbl_dproducto.Text = string.Empty;
                    lbl_dlote.Text = string.Empty;
                    lbl_dBodega.Text = string.Empty;
                    lbl_dcantidad.Text = string.Empty;
                    txt_cantidad.Text = string.Empty;
                }
            }
        } catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
    private void ClearComponent()
    {
        if (DeviceInfo.Model != "MC33")
        {
            if (!(picker.SelectedIndex == 1) && (string.IsNullOrEmpty(txtPosicion.Text)) )
            {
                txt_destino.IsVisible = false;
                picker.SelectedIndex = -1;
                lblError.Text = string.Empty;
                lblError2.Text = string.Empty;
                lblError3.Text = string.Empty;
                txtPosicion.Text = string.Empty;
                lbl_codproducto.Text = string.Empty;
                lbl_producto.Text = string.Empty;
                lbl_lote.Text = string.Empty;
                lbl_cantidad.Text = string.Empty;
                lblBodega.Text = string.Empty;
                txt_destino.Text = string.Empty;
                lbl_dproducto.Text = string.Empty;
                lbl_dlote.Text = string.Empty;
                lbl_dBodega.Text = string.Empty;
                lbl_dcantidad.Text = string.Empty;
                txt_cantidad.Text = string.Empty;
            }
        }
        else
        {
            txt_destino.IsVisible = false;
            picker.SelectedIndex = -1;
            lblError.Text = string.Empty;
            lblError2.Text = string.Empty;
            lblError3.Text = string.Empty;
            txtPosicion.Text = string.Empty;
            lbl_codproducto.Text = string.Empty;
            lbl_producto.Text = string.Empty;
            lbl_lote.Text = string.Empty;
            lbl_cantidad.Text = string.Empty;
            lblBodega.Text = string.Empty;
            txt_destino.Text = string.Empty;
            lbl_dproducto.Text = string.Empty;
            lbl_dlote.Text = string.Empty;
            lbl_dBodega.Text = string.Empty;
            lbl_dcantidad.Text = string.Empty;
            txt_cantidad.Text = string.Empty;
        }
    }
    private void Txt_cantidad_Completed(object sender, EventArgs e)
    {
        if (String.IsNullOrWhiteSpace(txt_cantidad.Text))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            lblError3.IsVisible = true;
            lblError3.Text = "ingrese Cantidad";
        }
        else if (!txt_cantidad.Text.ToCharArray().All(Char.IsDigit))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            lblError3.IsVisible = true;
            lblError3.Text = "ingrese solo numeros";
        }
        else
        {
            btn_generar.IsEnabled = true;
            lblError3.IsVisible = false;
            lblError3.Text = string.Empty;
        }
    }
    private void CboTipoPallet_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    private void cboProducto_SelectionChanged(object sender, EventArgs e)
    {

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
    private void Cbo_tipoRepaletizado_Completed(object sender, EventArgs e)
    {
        if (txt_destino.IsEnabled)
        {
            txt_destino.Focus();
        }
        else
        {
            txt_cantidad.Focus();
        }
    }
}