using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using Newtonsoft.Json;
using System.Net.Http;

namespace NewsMauiCVT.Views;

public partial class TomaInventario : ContentPage
{
    int v_lay = 0;
    public TomaInventario()
    {
        InitializeComponent();
        cargaDatos();

    }
    protected override void OnAppearing()
    {

        base.OnAppearing();
        ClearComponent();
        txt_ubicacion.Text = string.Empty;
        cboFolio.SelectedIndex = -1;
        cboTipoPallet.SelectedIndex = -1;
        lblError.IsVisible = false;
        lblError.Text = string.Empty;
        lblError2.IsVisible = false;
        lblError2.Text = string.Empty;
        lblError3.IsVisible = false;
        lblError3.Text = string.Empty;
        lblError4.IsVisible = false;
        lblError4.Text = string.Empty;
        lblError5.IsVisible = false;
        lblError5.Text = string.Empty;
        cboFolio.Focus();
    }

    void cargaDatos()
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            HttpClient ClientHttp = new()
            {
                BaseAddress = new Uri("http://wsintranet.cvt.local/")
            };
            var rest = ClientHttp.GetAsync("api/TomaInventario").Result;

            if (rest.IsSuccessStatusCode)
            {
                DatosTipoPallet dTip = new();

                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                List<TomaInventarioClass> dt = JsonConvert.DeserializeObject<List<TomaInventarioClass>>(resultadoStr) ??
                                throw new InvalidOperationException();
                List<TipoPalletClass> dt2 = dTip.ListaTipoPallet();

                List<FolPall> fl = [];

                foreach (var t in dt)
                {

                    fl.Add(new FolPall { folioInventario = t.Inventario_Id });

                }

                cboFolio.BindingContext = fl;

                List<TipoPall> lPro = [];
                cboTipoPallet.IsVisible = true;

                foreach (var s in dt2)
                {
                    lPro.Add(new TipoPall { Supportive_Description = s.Supportive_Description });
                }


                cboTipoPallet.BindingContext = lPro;


            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }

    public class FolPall
    {
        public string folioInventario { get; set; }
    }
    public class TipoPall
    {
        public string Supportive_Description { get; set; }


    }
    private void CboFolio_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (cboFolio.SelectedIndex != -1)
        {
            //txt_pallet.Focus();
            //txt_pallet.IsEnabled = true;

            cboTipoPallet.Focus();
        }
    }

    private void Txt_pallet_Completed(object sender, EventArgs e)
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            if (txt_pallet.Text.Equals(string.Empty))
            {
                lblError.Text = "Ingrese un n° de Pallet";
                lblError.IsVisible = true;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                //DisplayAlert("Alerta",, "Aceptar");          
                txt_pallet.Focus();
            }
            else if (!txt_pallet.Text.ToCharArray().All(Char.IsDigit))
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                lblError.Text = "Ingrese solo Numeros";
                lblError.IsVisible = true;
                txt_pallet.Focus();
                txt_pallet.Text = string.Empty;
            }
            else
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet.cvt.local/")
                };

                //verifica numero palllet
                var rest = ClientHttp.GetAsync("api/TomaInventario?NumPallet=" + Convert.ToInt32(txt_pallet.Text)).Result;
                if (rest.IsSuccessStatusCode)
                {
                    var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                    int NumPallet = JsonConvert.DeserializeObject<int>(resultadoStr);

                    if (NumPallet == 0)
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        lblError.Text = "N° de pallet no existe";
                        lblError.IsVisible = true;
                        txt_pallet.Focus();
                        txt_pallet.Text = string.Empty;
                    }
                    else
                    {
                        DatosTomaInventarioCVT dat = new();

                        List<ProductoInventarioCVT> ls = dat.ListProductosInventario(Convert.ToInt32(txt_pallet.Text));

                        foreach (var t in ls)
                        {
                            txt_producto.Text = t.ArticleProvider_CodClient;
                            txt_lote.Text = t.Package_Lot;
                            v_lay = t.Site_Id;


                        }
                        //txt_producto.Focus();
                        //txt_producto.IsEnabled = true;
                        txt_cantidad.Focus();
                        txt_cantidad.IsEnabled = true;
                        txt_ubicacion.IsEnabled = true;

                        txt_producto.IsVisible = true;
                        txt_lote.IsVisible = true;

                        txt_producto.IsEnabled = true;
                        txt_lote.IsEnabled = true;

                        lblError.Text = string.Empty;
                        lblError.IsVisible = false;



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

    //private void Txt_producto_Completed(object sender, EventArgs e)
    //{
    //    var ACC = Connectivity.NetworkAccess;
    //    if (ACC == NetworkAccess.Internet)
    //    {
    //        HttpClient ClientHttp = new HttpClient();
    //        ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

    //        //verifica CodProducto
    //        var rest = ClientHttp.GetAsync("api/TomaInventario?CodProducto=" + txt_producto.Text).Result;
    //        if (rest.IsSuccessStatusCode)
    //        {
    //            var resultadoStr = rest.Content.ReadAsStringAsync().Result;
    //            int CodProd = JsonConvert.DeserializeObject<int>(resultadoStr);

    //            if (CodProd == 0)
    //            {
    //                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
    //                lblError2.Text = "Cod. Producto no existe";
    //                lblError2.IsVisible = true;
    //                txt_producto.Focus();
    //                txt_producto.Text = string.Empty;
    //            }
    //            else
    //            {
    //                lblError2.Text = string.Empty;
    //                lblError2.IsVisible = false;
    //                txt_lote.Focus();
    //                txt_lote.IsEnabled = true; ;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
    //        DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
    //    }
    //}

    //private void Txt_lote_Completed(object sender, EventArgs e)
    //{
    //    var ACC = Connectivity.NetworkAccess;
    //    if (ACC == NetworkAccess.Internet)
    //    {
    //        HttpClient ClientHttp = new HttpClient();
    //        ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");


    //        byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(txt_lote.Text);
    //        string returntext = System.Convert.ToBase64String(mybyte);


    //        var rest = ClientHttp.GetAsync("api/TomaInventario?LoteCode=" + returntext).Result;

    //        //var rest = ClientHttp.GetAsync("api/TomaInventario?NumLote=" + txt_lote.Text).Result;

    //        if (rest.IsSuccessStatusCode)
    //        {                  

    //            var resultadoStr = rest.Content.ReadAsStringAsync().Result;
    //            int vLote = JsonConvert.DeserializeObject<int>(resultadoStr);

    //            if (vLote == 0)
    //            {
    //                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
    //                lblError3.IsVisible = true;
    //                lblError3.Text = "Lote no existe";
    //                txt_lote.Focus();
    //                txt_lote.Text = string.Empty;
    //            }
    //            else
    //            {
    //                lblError3.IsVisible = false;
    //                lblError3.Text = string.Empty;
    //                txt_cantidad.Focus();
    //                txt_cantidad.IsEnabled = true;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
    //        DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
    //    }
    //}

    private void Txt_cantidad_Completed(object sender, EventArgs e)
    {
        //Valida que solo se ingresen numeros
        if (!txt_cantidad.Text.ToCharArray().All(Char.IsDigit))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            lblError4.IsVisible = true;
            lblError4.Text = "Ingrese solo numeros";
            txt_cantidad.Focus();
            txt_cantidad.Text = string.Empty;
        }
        else
        {
            lblError4.IsVisible = false;
            lblError4.Text = string.Empty;
            txt_ubicacion.Focus();
            txt_ubicacion.IsEnabled = true;
        }

    }

    private async void Txt_ubicacion_Completed(object sender, EventArgs e)
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            if (txt_ubicacion.Text.Equals(string.Empty))
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                lblError5.Text = "Ingrese ubicacion";
                lblError5.IsVisible = true;
                txt_ubicacion.Text = string.Empty;
                txt_ubicacion.Focus();
            }
            else
            if (!txt_ubicacion.Text.ToCharArray().All(Char.IsDigit))
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                lblError5.Text = "Ingrese solo numeros";
                lblError5.IsVisible = true;
                txt_ubicacion.Text = string.Empty;
                txt_ubicacion.Focus();
            }
            else
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet.cvt.local/")
                };

                //verifica ubicacion
                var rest = ClientHttp.GetAsync("api/TomaInventario?NumPosicion=" + Convert.ToInt32(txt_ubicacion.Text)).Result;

                DatosTomaInventarioCVT dti = new();
                List<ProductoInventarioCVT> ls = dti.ListProductosInventario(Convert.ToInt32(txt_pallet.Text));

                int bodProdInv = 0;

                foreach (var t in ls)
                {
                    bodProdInv = t.Site_Id;
                }

                int bodDes = dti.traeSiteIdLayout(Convert.ToInt32(txt_ubicacion.Text));

                if (rest.IsSuccessStatusCode)
                {
                    var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                    int vUbic = JsonConvert.DeserializeObject<int>(resultadoStr);

                    if (vUbic == 0)
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        lblError5.Text = "Ubicacion no existe";
                        lblError5.IsVisible = true;
                        txt_ubicacion.Focus();
                        txt_ubicacion.Text = string.Empty;
                    }
                    else if (bodProdInv == bodDes)
                    {
                        btn_agregar.Focus();
                        lblError5.Text = string.Empty;
                        lblError5.IsVisible = false;
                    }
                    else
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        lblError5.Text = "Producto no se encuentra en la bodega de inventario ,favor verificar";
                        lblError5.IsVisible = true;
                        txt_ubicacion.Text = string.Empty;
                        txt_ubicacion.Focus();

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

    void ClearComponent()
    {
        txt_pallet.Text = string.Empty;
        txt_pallet.IsEnabled = false;
        txt_producto.Text = string.Empty;
        txt_producto.IsEnabled = false;
        txt_lote.Text = string.Empty;
        txt_lote.IsEnabled = false;
        txt_cantidad.Text = string.Empty;
        txt_cantidad.IsEnabled = false;
        txt_ubicacion.Text = string.Empty;
        txt_ubicacion.IsEnabled = false;
        txt_pallet.Focus();
    }

    private void Btn_agregar_Clicked(object sender, EventArgs e)
    {
        if (txt_pallet.Text.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Ingrese un n° de Pallet", "Aceptar");
            txt_pallet.Focus();
        }
        else if (txt_producto.Text.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Ingrese Cod.Producto", "Aceptar");
            txt_producto.Focus();
        }
        else if (txt_lote.Text.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Ingrese N°Lote", "Aceptar");
            txt_lote.Focus();
        }
        else if (txt_cantidad.Text.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Ingrese Cantidad", "Aceptar");
            txt_cantidad.Focus();
        }
        else if (txt_ubicacion.Text.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Ingrese ubicacion", "Aceptar");
            txt_ubicacion.Focus();
        }
        else
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {

                DatosTomaInventarioCVT dti = new();
                List<ProductoInventarioCVT> ls = dti.ListProductosInventario(Convert.ToInt32(txt_pallet.Text));

                int bodProdInv = 0;

                foreach (var t in ls)
                {
                    bodProdInv = t.Site_Id;
                }

                int bodDes = dti.traeSiteIdLayout(Convert.ToInt32(txt_ubicacion.Text));

                if (bodProdInv == bodDes)
                {

                    HttpClient ClientHttp = new()
                    {
                        BaseAddress = new Uri("http://wsintranet.cvt.local/")
                    };

                    int Folio = Convert.ToInt32(cboFolio.SelectedValue);
                    int numPall = Convert.ToInt32(txt_pallet.Text);
                    int cant = Convert.ToInt32(txt_cantidad.Text);
                    int ubi = Convert.ToInt32(txt_ubicacion.Text);


                    byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(txt_lote.Text);
                    string lote = System.Convert.ToBase64String(mybyte);

                    // InsertaRegistroInventario
                    var rest = ClientHttp.GetAsync("api/TomaInventario?CodProducto=" + txt_producto.Text + "&Lote=" + lote + "&Cantidad=" + cant + "&Ubicacion=" + ubi + "&Usuario=" + App.UserSistema.ToString() + "&NPallet=" + numPall + "&Inventario_Id=" + Folio).Result;
                    if (rest.IsSuccessStatusCode)
                    {
                        var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                        bool respuesta = JsonConvert.DeserializeObject<bool>(resultadoStr);

                        if (respuesta == true)
                        {
                            // busca staffid
                            var rest2 = ClientHttp.GetAsync("api/Usuario?usernameWMS=" + App.UserSistema).Result;
                            var resultadoStr2 = rest2.Content.ReadAsStringAsync().Result;
                            int staffID = JsonConvert.DeserializeObject<int>(resultadoStr2);

                            //ObtienePackageIdPosicionamiento
                            var rest3 = ClientHttp.GetAsync("api/Posicionamiento?NumPallet=" + txt_pallet.Text).Result;
                            var resultadoStr3 = rest3.Content.ReadAsStringAsync().Result;
                            int Package_Id = JsonConvert.DeserializeObject<int>(resultadoStr3);

                            //ActualizaLayoutPackage
                            var rest4 = ClientHttp.GetAsync("api/Produccion?PackageId=" + Package_Id + "&layoutid=" + Convert.ToInt32(txt_ubicacion.Text)).Result;
                            var resultadoStr4 = rest4.Content.ReadAsStringAsync().Result;

                            //AddLocation
                            var rest5 = ClientHttp.GetAsync("api/Produccion?PackageId=" + Package_Id + "&LayoutDestinoId=" + Convert.ToInt32(txt_ubicacion.Text) + "&StaffId=" + staffID).Result;
                            var resultadoStr5 = rest5.Content.ReadAsStringAsync().Result;
                            bool oks = JsonConvert.DeserializeObject<bool>(resultadoStr5);

                            #region AccTipoPallet
                            DatosTipoPallet dTip = new();

                            int IdTipoPal = dTip.TraeIdTipoPallet(cboTipoPallet.SelectedItem.ToString());

                            dTip.ActualizaTipoPallet(numPall, IdTipoPal);


                            #endregion
                            DisplayAlert("Alerta", "Registrado", "Aceptar");
                            DependencyService.Get<IAudio>().PlayAudioFile("Correcto.mp3");

                            txt_pallet.Text = string.Empty;
                            txt_producto.Text = string.Empty;
                            txt_producto.IsEnabled = false;
                            txt_lote.Text = string.Empty;
                            txt_lote.IsEnabled = false;
                            txt_cantidad.Text = string.Empty;
                            txt_cantidad.IsEnabled = false;
                            txt_ubicacion.Text = string.Empty;
                            txt_ubicacion.IsEnabled = false;
                            txt_pallet.Focus();

                        }
                        else
                        {
                            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                            DisplayAlert("Alerta", "Error al Registrar Verificar", "Aceptar");
                        }
                    }
                    else { DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3"); }
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    DisplayAlert("Alerta", "Bodegas No Coinciden ", "Aceptar");
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
        return false;
    }

    private void CboTipoPallet_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (cboTipoPallet.SelectedIndex != -1)
        {
            txt_pallet.Focus();
            txt_pallet.IsEnabled = true;
        }
    }
}