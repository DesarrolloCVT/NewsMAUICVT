using DevExpress.Maui.Core.Internal;
using Microsoft.Maui;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using Newtonsoft.Json;
using System.ComponentModel;

namespace NewsMauiCVT.Views;

public partial class Posicionamiento : ContentPage
{
    public Posicionamiento()
	{
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        //btn_generar.IsEnabled = false;
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
    private async void Txt_origen_Completed(object sender, EventArgs e)
    {
        lblConfirm.Text = string.Empty;
        lblConfirm.IsVisible = false;
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            string nPallet = txt_origen.Text;

            HttpClient ClientHttp = new HttpClient();
            ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
            try
            {
                //Obtiene Informacion del Pallet
                var rest = ClientHttp.GetAsync("api/Posicionamiento?SSCCPack=" + nPallet).Result;

                if (rest.IsSuccessStatusCode)
                {
                    var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                    List<Package> dt = JsonConvert.DeserializeObject<List<Package>>(resultadoStr) ??
                                throw new InvalidOperationException();
                    if (dt.Count() == 0)
                    {   
                        txt_origen.Text = string.Empty;
                        lblError.Text = "N° de pallet no existe";
                        lblError.IsVisible = true;
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        _ = Task.Delay(300).ContinueWith(t => {
                            txt_origen.Focus();
                        });
                    }
                    else
                    {
                        foreach (var p in dt)
                        {
                            LayoutOrigen.IsVisible = true;
                            txt_destino.IsVisible = true;

                            //ObtieneInfoProducto
                            var rest2 = ClientHttp.GetAsync("api/Produccion/" + p.ArticleProvider_Id).Result;

                            if (rest2.IsSuccessStatusCode)
                            {
                                var resultadoStr2 = rest2.Content.ReadAsStringAsync().Result;
                                List<ArticleProvider> ap = JsonConvert.DeserializeObject<List<ArticleProvider>>(resultadoStr2) ??
                                throw new InvalidOperationException();

                                foreach (var a in ap)
                                {
                                    lbl_codproducto.Text = "Cod.Producto: " + a.ArticleProvider_CodClient;
                                    lbl_producto.Text = "Producto: " + a.ArticleProvider_Description;
                                }
                                int layID = Convert.ToInt32(p.Layout_Id);
                                //ObtieneInfoLayout
                                var rest3 = ClientHttp.GetAsync("api/Bodega?CodLayoutInfo=" + layID).Result;
                                var resultadoStr3 = rest3.Content.ReadAsStringAsync().Result;
                                List<LayoutClass> ly = JsonConvert.DeserializeObject<List<LayoutClass>>(resultadoStr3) ??
                                throw new InvalidOperationException();

                                foreach (var l in ly)
                                {
                                    lbl_ubicacion.Text = "Ubicacion Actual: " + l.Layout_Description;

                                    //ObtieneNombreCortoSitio
                                    var rest4 = ClientHttp.GetAsync("api/Bodega?siteid=" + l.Site_Id).Result;
                                    var resultadoStr4 = rest4.Content.ReadAsStringAsync().Result;
                                    string NombreCortoSitio = JsonConvert.DeserializeObject<string>(resultadoStr4) ??
                                throw new InvalidOperationException();


                                    lbl_sitio.Text = "Sitio: " + NombreCortoSitio;
                                }
                                string fecha;
                                DateTime date = DateTime.Now;
                                date = (DateTime)p.Package_ProductionDate;
                                fecha = date.ToString("dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                lbl_fechaProducto.Text = "Fecha Producción: " + fecha;
                                Console.WriteLine("fecha: " + fecha);
                                lbl_lote.Text = "Lote: " + p.Package_Lot;
                                lbl_cantidad.Text = "Cantidad: " + p.Package_Quantity.ToString();
                                lblError.IsVisible = false;
                                lblError.Text = string.Empty;
                            }
                        }
                        txt_destino.Focus();
                    }
                }
            }
            catch { }
        }
        else
        {
            DependencyService.Get<Model.IAudio>().PlayAudioFile("terran-error.mp3");
            await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private void Txt_destino_Completed(object sender, EventArgs e)
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet.cvt.local/")
                };

                //ObtieneInfoLayout
                var rest = ClientHttp.GetAsync("api/Bodega?CodLayoutInfo=" + Convert.ToInt32(txt_destino.Text)).Result;

                if (rest.IsSuccessStatusCode)
                {
                    lblError2.Text = string.Empty;
                    lblError2.IsVisible = false;

                    LayoutDestinoExistente.IsVisible = true;

                    var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                    List<LayoutClass> ly = JsonConvert.DeserializeObject<List<LayoutClass>>(resultadoStr) ??
                                throw new InvalidOperationException();

                    foreach (var l in ly)
                    {
                        lbl_ubicacion_nueva.Text = "Ubicacion Nueva: " + l.Layout_Description;

                        //NombreCortoSitio
                        var rest2 = ClientHttp.GetAsync("api/Bodega?siteid=" + l.Site_Id).Result;
                        var resultadoStr2 = rest2.Content.ReadAsStringAsync().Result;
                        string NombreCortoSitio = JsonConvert.DeserializeObject<string>(resultadoStr2) ??
                                throw new InvalidOperationException();

                        lbl_sitio_nuevo.Text = "Sitio: " + NombreCortoSitio;
                        LogUsabilidad("Pallet posicionado");
                    }
                    if (lbl_sitio.Text == lbl_sitio_nuevo.Text)
                    {
                        // btn_generar.IsEnabled = true;
                        txt_ConfirmaDestino.Focus();
                        txt_ConfirmaDestino.IsVisible = true;
                        // this.btn_generar.Focus();
                        //  Btn_generar_Clicked(sender, e);
                    }
                    else
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        // btn_generar.IsEnabled = false;
                        txt_destino.Text = string.Empty;
                        txt_destino.Focus();
                        lbl_sitio_nuevo.Text = string.Empty;
                        lbl_ubicacion_nueva.Text = string.Empty;
                        lblError2.Text = "No se puede ingresar en este destino";
                        lblError2.IsVisible = true;
                    }
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    txt_destino.Text = string.Empty;
                    lbl_sitio_nuevo.Text = string.Empty;
                    lbl_ubicacion_nueva.Text = string.Empty;
                    lblError2.Text = "Se ha producido un error";
                    lblError2.IsVisible = true;
                    _ = Task.Delay(150).ContinueWith(t => {
                        txt_destino.Focus();
                    });
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Txt_destino_Completed: " + ex.ToString());
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private void ClearComponent()
    {
        lblError.IsVisible = false;
        lblError2.IsVisible = false;
        lblError.Text = string.Empty;
        lblError2.Text = string.Empty;
        txt_origen.Text = string.Empty;
        lbl_codproducto.Text = string.Empty;
        lbl_producto.Text = string.Empty;
        lbl_lote.Text = string.Empty;
        lbl_cantidad.Text = string.Empty;
        lbl_sitio.Text = string.Empty;
        lbl_ubicacion.Text = string.Empty;
        txt_destino.IsVisible = false;
        txt_ConfirmaDestino.Text = string.Empty;
        txt_ConfirmaDestino.IsVisible = false;
        txt_destino.Text = string.Empty;
        lbl_sitio_nuevo.Text = string.Empty;
        lbl_ubicacion_nueva.Text = string.Empty;
        LayoutOrigen.IsVisible = false;
        LayoutDestinoExistente.IsVisible = false;
    }
    private void Txt_ConfirmaDestino_Completed(object sender, EventArgs e)
    {
        if(!txt_origen.Text.Equals(string.Empty) &&  txt_destino.Text == txt_ConfirmaDestino.Text && !txt_destino.Text.Equals(string.Empty))
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

                //busca staffid
                var rest = ClientHttp.GetAsync("api/Usuario?usernameWMS=" + App.UserSistema).Result;
                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                int staffID = JsonConvert.DeserializeObject<int>(resultadoStr);

                //ObtienePackageIdPosicionamiento
                var rest2 = ClientHttp.GetAsync("api/Posicionamiento?NumPallet=" + txt_origen.Text).Result;
                var resultadoStr2 = rest2.Content.ReadAsStringAsync().Result;
                int Package_Id = JsonConvert.DeserializeObject<int>(resultadoStr2);

                //ActualizaLayoutPackage
                var rest3 = ClientHttp.GetAsync("api/Produccion?PackageId=" + Package_Id + "&layoutid=" + Convert.ToInt32(txt_destino.Text)).Result;
                var resultadoStr3 = rest3.Content.ReadAsStringAsync().Result;

                //AddLocation
                var rest4 = ClientHttp.GetAsync("api/Produccion?PackageId=" + Package_Id + "&LayoutDestinoId=" + Convert.ToInt32(txt_destino.Text) + "&StaffId=" + staffID).Result;
                var resultadoStr4 = rest4.Content.ReadAsStringAsync().Result;
                bool oks = JsonConvert.DeserializeObject<bool>(resultadoStr4);

                if (oks == true)
                {
                    DependencyService.Get<Model.IAudio>().PlayAudioFile("Correcto.mp3");
                    //  DisplayAlert("Alerta", "Registrado", "Aceptar");
                    lblConfirm.IsVisible = true;
                    lblConfirm.Text = "Registrado";
                    ClearComponent();
                    txt_origen.Focus();

                }
                else
                {
                    DependencyService.Get<Model.IAudio>().PlayAudioFile("terran-error.mp3");
                    DisplayAlert("Alerta", "Error al Registrar Verificar", "Aceptar");
                }
            }
            else
            {
                DependencyService.Get<Model.IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }
        else if (txt_destino.Text != txt_ConfirmaDestino.Text)
        {
            DependencyService.Get<Model.IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Destinos no son iguales favor verificar", "Aceptar");
            txt_ConfirmaDestino.Focus();
            txt_ConfirmaDestino.Text = string.Empty;
        }
        else if (txt_origen.Text.Equals(string.Empty))
        {
            DependencyService.Get<Model.IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Ingrese un n° de Pallet", "Aceptar");
            txt_origen.Focus();
        }
        else if (txt_destino.Text.Equals(string.Empty))
        {
            DependencyService.Get<Model.IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Ingrese Destino", "Aceptar");
            txt_destino.Focus();
        }
        /*else
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

                //busca staffid
                var rest = ClientHttp.GetAsync("api/Usuario?usernameWMS=" + App.UserSistema).Result;
                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                int staffID = JsonConvert.DeserializeObject<int>(resultadoStr);

                //ObtienePackageIdPosicionamiento
                var rest2 = ClientHttp.GetAsync("api/Posicionamiento?NumPallet=" + txt_origen.Text).Result;
                var resultadoStr2 = rest2.Content.ReadAsStringAsync().Result;
                int Package_Id = JsonConvert.DeserializeObject<int>(resultadoStr2);

                //ActualizaLayoutPackage
                var rest3 = ClientHttp.GetAsync("api/Produccion?PackageId=" + Package_Id + "&layoutid=" + Convert.ToInt32(txt_destino.Text)).Result;
                var resultadoStr3 = rest3.Content.ReadAsStringAsync().Result;

                //AddLocation
                var rest4 = ClientHttp.GetAsync("api/Produccion?PackageId=" + Package_Id + "&LayoutDestinoId=" + Convert.ToInt32(txt_destino.Text) + "&StaffId=" + staffID).Result;
                var resultadoStr4 = rest4.Content.ReadAsStringAsync().Result;
                bool oks = JsonConvert.DeserializeObject<bool>(resultadoStr4);

                if (oks == true)
                {
                    DependencyService.Get<Model.IAudio>().PlayAudioFile("Correcto.mp3");
                    //  DisplayAlert("Alerta", "Registrado", "Aceptar");
                    lblConfirm.IsVisible = true;
                    lblConfirm.Text = "Registrado";
                    ClearComponent();
                }
                else
                {
                    DependencyService.Get<Model.IAudio>().PlayAudioFile("terran-error.mp3");
                    DisplayAlert("Alerta", "Error al Registrar Verificar", "Aceptar");
                }
            }
            else
            {
                DependencyService.Get<Model.IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }*/
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
        var IdSubMenu = 26;

        DatosApp datosApp = new DatosApp();
        datosApp.LogUsabilidad(IdSubMenu, TipoRegistro);
    }
}