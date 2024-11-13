using Controls.UserDialogs.Maui;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMM_TomaInventario : ContentPage
{
    public SMM_TomaInventario()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        cargaDatos();
        lblError.Text = string.Empty;
        lblError.IsVisible = false;
        lblError2.Text = string.Empty;
        lblError2.IsVisible = false;
        lblError3.Text = string.Empty;
        lblError3.IsVisible = false;

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
        cboFolio.Focus();
        LogUsabilidad("Ingreso");
    }
    void ClearComponent()
    {
        cboFolio.SelectedIndex = -1;
        cboPasillo.SelectedIndex = -1;
        btn_agregar.IsEnabled = false;
        lblError.Text = string.Empty;
        lblError.IsVisible = false;
        lblError2.Text = string.Empty;
        lblError2.IsVisible = false;
        lblError3.Text = string.Empty;
        lblError3.IsVisible = false;
        //txt_Bodega.SelectedIndex = -1;
        txtDia.Text = string.Empty;
        txtMes.Text = string.Empty;
        txtAno.Text = string.Empty;
        txt_pallet.Text = string.Empty;
        lblProducto.Text = string.Empty;
        txt_cantidad.Text = string.Empty;
    }
    void cargaDatos()
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            DatosSMM_TomaInventario ti = new DatosSMM_TomaInventario();
            List<TomaInventarioClass> lt = ti.TraeFoliosInventarios();

            List<FolioSMM> fl = new List<FolioSMM>();
            foreach (var t in lt)
            {
                fl.Add(new FolioSMM { FolioInvSMM = t.Inventario_Id });

            }
            cboFolio.BindingContext = fl;
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    public class FolioSMM
    {
        public string FolioInvSMM { get; set; }
    }
    private void CboFolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_Bodega.Focus();
    }
    private void Txt_Bodega_SelectedIndexChanged(object sender, EventArgs e)
    {
        DatosSMM_TomaInventario dti = new DatosSMM_TomaInventario();
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            if (txt_Bodega.SelectedItem.Equals(string.Empty))
            {
                lblError.Text = "ingrese Bodega";
                lblError.IsVisible = true;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                txt_Bodega.Focus();
            }
            else if (Convert.ToInt32(dti.ValidaBodegaSMM(txt_Bodega.SelectedItem.ToString())) == 0)
            {
                lblError.IsVisible = true;
                txt_Bodega.Focus();
                lblError.Text = "Bodega no existe";
                txt_Bodega.SelectedIndex = -1;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");

            }
            else
            {

                cboPasillo.Focus();
                lblError.Text = string.Empty;
                lblError.IsVisible = false;
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }

    }
    private void CboPasillo_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_pallet.Focus();
    }
    private async void Txt_pallet_Completed(object sender, EventArgs e)
    {
        LogUsabilidad("Ingreso");
        DatosSMM_TomaInventario dti = new DatosSMM_TomaInventario();
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            string codpro = dti.ValidaCodProducto(txt_pallet.Text);
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
                await DisplayAlert("Alerta", "Codigo Producto no existe", "Aceptar");
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                txt_pallet.Text = string.Empty;
                txt_pallet.Focus();

            }
            else
            {

                // Fvenci.Focus();
                txtDia.Focus();
                string codiPro = dti.TraeCodProducti(txt_pallet.Text);
                List<SMMDatoProductosRecepcion> ls = dti.ListaDatosProdRes(codiPro, txt_pallet.Text);

                string Umd = "";
                foreach (var t in ls)
                {
                    Umd = t.UomCode;
                }

                lblProducto.Text = codpro.ToString() + " //  " + Umd.ToString();
                lblError2.Text = string.Empty;
                lblError2.IsVisible = false;
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private void Txt_cantidad_Completed(object sender, EventArgs e)
    {
        if (txt_cantidad.Text.Equals(string.Empty))
        {
            lblError3.Text = "Ingrese Cantidad";
            lblError3.IsVisible = true;
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            //DisplayAlert("Alerta",, "Aceptar");          
            txt_cantidad.Focus();
        }
        else
        {
            btn_agregar.IsEnabled = true;
            lblError3.Text = string.Empty;
            lblError3.IsVisible = false;
        }
    }
    private void Btn_agregar_Clicked(object sender, EventArgs e)
    {
        if (cboFolio.SelectedIndex == -1)
        {
            cboFolio.Focus();
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
        }
        else if (txt_Bodega.SelectedItem.Equals(string.Empty))
        {
            lblError.Text = "ingrese Bodega";
            lblError.IsVisible = true;
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            txt_Bodega.Focus();
        }
        else if (cboPasillo.SelectedIndex == -1)
        {
            cboPasillo.Focus();
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
        }
        else if (txt_pallet.Text.Equals(string.Empty))
        {
            lblError2.Text = "ingrese Codigo Producto";
            lblError2.IsVisible = true;
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            txt_pallet.Focus();
        }
        else if (txt_cantidad.Text.Equals(string.Empty))
        {
            lblError3.Text = "Ingrese Cantidad";
            lblError3.IsVisible = true;
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            txt_cantidad.Focus();
        }
        else
        {

            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {

                DatosSMM_TomaInventario dTi = new DatosSMM_TomaInventario();
                string codPro = dTi.TraeCodProducti(txt_pallet.Text);
                if (dTi.ValidaCodProducto(txt_pallet.Text) != "")
                {
                    if (Convert.ToInt32(dTi.ValidaBodegaSMM(txt_Bodega.SelectedItem.ToString())) != 0)
                    {
                        try
                        {

                            List<SMMDatoProductosRecepcion> ls = dTi.ListaDatosProdRes(codPro, txt_pallet.Text);

                            string CantxEmp = "";
                            int CantBs = 0;
                            string TpEmpaq = "";

                            foreach (var t in ls)
                            {
                                CantxEmp = t.UgpName;
                                CantBs = Convert.ToInt32(t.BaseQty);
                                TpEmpaq = t.UomCode;
                            }

                            int idinv = Convert.ToInt32(cboFolio.SelectedValue);
                            string dn14 = txt_pallet.Text;
                            string cant = txt_cantidad.Text.Replace(".", ",");
                            int idBod = Convert.ToInt32(dTi.ValidaBodegaSMM(txt_Bodega.SelectedItem.ToString()));
                            int iduser = App.Iduser;
                            string fecha = txtAno.Text + "-" + txtMes.Text + "-" + txtDia.Text;
                            string ubPasillo = cboPasillo.SelectedItem.ToString() ?? throw new InvalidOperationException();

                            bool rest = dTi.insertaInventario(idinv, dn14, codPro, cant, idBod, iduser, ubPasillo, fecha, CantxEmp, CantBs, TpEmpaq);
                            if (rest == true)
                            {
                                DisplayAlert("Alerta", "Registrado", "Aceptar");
                                txt_pallet.Text = string.Empty;
                                txt_pallet.Focus();
                                txt_cantidad.Text = string.Empty;
                                lblProducto.Text = string.Empty;
                                txtDia.Text = string.Empty;
                                txtMes.Text = string.Empty;
                                txtAno.Text = string.Empty;
                                btn_agregar.IsEnabled = false;
                            }
                            else
                            {
                                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                DisplayAlert("Alerta", "ERROR AL REGISTRAR,CONTACTAR CON ADMINISTRADOR", "Aceptar");
                            }

                        }
                        catch { }
                    }
                    else
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        DisplayAlert("Alerta", "BODEGA NO ENCONTRADA", "Aceptar");
                        txt_Bodega.Focus();

                    }
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    DisplayAlert("Alerta", "DUN 14 NO ENCONTRADO", "Aceptar");
                    txt_pallet.Focus();
                }

            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }
    }
    private void Fvenci_DateSelected(object sender, DateChangedEventArgs e)
    {
        txt_cantidad.Focus();
    }
    private async void txtDia_Completed(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(txtDia.Text) > 31 || (txtDia.Text.Equals("") || Convert.ToInt32(txtDia.Text) == 0))
            {
                using (UserDialogs.Instance.Alert("ingrese dia correcto"))
                {
                    await Task.Delay(5);


                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    await DisplayAlert("Alerta", "ingrese dia", "Aceptar");
                }
                txtDia.Focus();
            }
            else { txtMes.Focus(); }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    private async void txtMes_Completed(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(txtMes.Text) > 12 || (txtMes.Text.Equals("") || Convert.ToInt32(txtMes.Text) == 0))
            {
                using (UserDialogs.Instance.Alert("ingrese mes correcto"))
                {
                    await Task.Delay(5);


                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    await DisplayAlert("Alerta", "ingrese mes correcto", "Aceptar");
                }
                txtMes.Focus();
            }
            else { txtAno.Focus(); }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    private async void txtAno_Completed(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(txtAno.Text) < DateTime.Now.Year || txtAno.Text.Equals(""))
            {
                using (UserDialogs.Instance.Alert("ingrese año correcto"))
                {
                    await Task.Delay(5);


                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    await DisplayAlert("Alerta", "ingrese año correcto", "Aceptar");
                }
                txtAno.Focus();
            }
            else { txt_cantidad.Focus(); }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
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
        var IdSubMenu = 246;

        DatosApp datosApp = new DatosApp();
        datosApp.LogUsabilidad(IdSubMenu, TipoRegistro);
    }
}