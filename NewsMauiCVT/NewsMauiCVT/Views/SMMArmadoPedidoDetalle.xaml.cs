using Controls.UserDialogs.Maui;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;


public partial class SMMArmadoPedidoDetalle : ContentPage
{
    int _folio = 0;
    string _CodProd = "";
    string _Umedida = "";
    //int _OC = 0;
    int _CantidadOC = 0;
    public SMMArmadoPedidoDetalle(int FolioArm)
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        cargaDatos();
        _folio = FolioArm;
        lblPedido.Text = Convert.ToString(_folio);
        txt_CodBarr.Focus();
    }
    protected override void OnAppearing()
    {

        base.OnAppearing();
        txt_CodBarr.Focus();
        txt_CodBarr.Text = string.Empty;
        cboNombreRepo.SelectedIndex = -1;
        btn_agregar.IsVisible = false;
        lblDatosProd.IsVisible = false;

    }
    void cargaDatos()
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            DatosCumplimientoRepoSalaSMM ti = new DatosCumplimientoRepoSalaSMM();
            List<SMMReponedoresClass> lt = ti.ListaReponedores();

            List<SMMReponedoresClass> fl = new List<SMMReponedoresClass>();

            foreach (var t in lt)
            {
                fl.Add(new SMMReponedoresClass { IdReponedor = t.IdReponedor, Nombre = t.Nombre });

            }
            cboNombreRepo.BindingContext = fl;

        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }

        txt_CodBarr.Focus();
    }
    private void txt_CodBarr_Completed(object sender, EventArgs e)
    {

        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {


            DatosSMM_ArmadoPedido ar = new DatosSMM_ArmadoPedido();

            //int oc = ar.TraeOcRecepcion(Convert.ToInt32(lblFolioRecepcion.Text));

            List<SMMProductoArmadoPedido> pr = new List<SMMProductoArmadoPedido>();
            pr = ar.DatosProductosArmadoPes(txt_CodBarr.Text, _folio);
            int cl = pr.Count();

            if (cl != 0)
            {
                foreach (var t in pr)
                {
                    lblDatosProd.IsVisible = true;
                    lblIngresos.IsVisible = true;
                    btn_agregar.IsVisible = true;
                    //lblIngresos2.IsVisible = true;
                    lblCodProducto.Text = "Cod.Prod: " + t.CodProd.ToString();
                    _CodProd = t.CodProd.ToString();
                    _Umedida = t.UomCode.ToString();
                    //_OC = t.NOrden;
                    _CantidadOC = Convert.ToInt32(t.Cantidad);
                    lblDescripion.Text = t.Dscription.ToString();
                    //lblCosto.Text = "Precio OC: " + t.Precio;
                    lblCantSolicitada.Text = "Cantidad :" + t.Cantidad;
                    lblSku.Text = "UnMed: " + t.UomCode;

                }
            }
            else
            {

                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Producto No se encuentra en el pedido", "Aceptar");
                txt_CodBarr.Focus();
            }

        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private async void txtDia_Completed(object sender, EventArgs e)
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
    private async void txtMes_Completed(object sender, EventArgs e)
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
        else
        {
            txtAno.Focus();

        }
    }
    private async void txtAno_Completed(object sender, EventArgs e)
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
        else
        {
            GvGridDatos.IsVisible = true;
            btn_agregar.IsEnabled = true;
        }
    }
    private void cboCantCorr_SelectionChanged(object sender, EventArgs e)
    {

    }
    private void btn_agregar_Clicked(object sender, EventArgs e)
    {

        try
        {
            if (txt_CodBarr.Text.Equals(string.Empty) || txt_CodBarr.Equals(null))
            {
                txt_CodBarr.Focus();
                lblError2.Text = "ingrese Codigo Producto";
                lblError2.IsVisible = true;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");

            }
            if (cboNombreRepo.SelectedIndex == -1)
            {
                cboNombreRepo.Focus();
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                lblError2.Text = "Seleccione Reponedor";
                lblError2.IsVisible = true;
            }
            else
            {
                var ACC = Connectivity.NetworkAccess;
                if (ACC == NetworkAccess.Internet)
                {
                    try
                    {
                        int v_NPed = _folio;
                        string v_CodProd = _CodProd;
                        string v_CodBar = txt_CodBarr.Text;
                        string v_uMed = _Umedida;
                        int v_cant = _CantidadOC;
                        string v_fecha = txtAno.Text + "-" + txtMes.Text + "-" + txtDia.Text;
                        int v_idNVerificado = Convert.ToInt32(cboNombreRepo.SelectedValue);
                        //int v_idUserReg = App.Iduser;
                        string v_CantCo = cboCantCorr.SelectedIndex == -1 ? "0" : cboCantCorr.SelectedValue.ToString() ??
                                throw new InvalidOperationException();
                        string v_venCo = cboVenCorrecto.SelectedIndex == -1 ? "0" : cboVenCorrecto.SelectedValue.ToString() ??
                                throw new InvalidOperationException();
                        string v_etiqCo = cboEtiqueta.SelectedIndex == -1 ? "0" : cboEtiqueta.SelectedValue.ToString() ??
                                throw new InvalidOperationException();
                        string v_enfiCo = cboEnfilado.SelectedIndex == -1 ? "0" : cboEnfilado.SelectedValue.ToString() ??
                                throw new InvalidOperationException();
                        string v_estiCo = cboEstibado.SelectedIndex == -1 ? "0" : cboEstibado.SelectedValue.ToString() ??
                                throw new InvalidOperationException();
                        string v_ConPallCo = cboCondPallet.SelectedIndex == -1 ? "0" : cboCondPallet.SelectedValue.ToString() ??
                                throw new InvalidOperationException();
                        //int v_Verificador = App.Iduser;

                        DatosSMM_ArmadoPedido ar = new DatosSMM_ArmadoPedido();


                        string rest = ar.insertaRegistroArmadoPedido(v_NPed, v_CodProd, v_CodBar, v_uMed, v_cant, v_fecha, v_idNVerificado, v_CantCo, v_venCo, v_etiqCo, v_enfiCo, v_estiCo, v_ConPallCo);
                        if (rest != "0")
                        {
                            DisplayAlert("Alerta", "Registrado", "Aceptar");
                            txt_CodBarr.Text = string.Empty;
                            txt_CodBarr.Focus();
                            lblDatosProd.IsVisible = false;
                            lblIngresos.IsVisible = false;
                            btn_agregar.IsVisible = false;
                            lblDescripion.Text = string.Empty;
                            txtDia.Text = string.Empty;
                            txtMes.Text = string.Empty;
                            txtAno.Text = string.Empty;
                            btn_agregar.IsEnabled = false;
                            cboNombreRepo.SelectedIndex = -1;
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
                    DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
                }

            }

        }
        catch (Exception ex)
        {
            lblError2.IsVisible = true;
            lblError2.Text = ex.Message + " " + "contactar al administrador";
            lblError2.Focus();
        }
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
}