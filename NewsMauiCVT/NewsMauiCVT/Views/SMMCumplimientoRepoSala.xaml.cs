using Controls.UserDialogs.Maui;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMMCumplimientoRepoSala : ContentPage
{
    public SMMCumplimientoRepoSala()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        cargaDatos();
        
    }
    protected override void OnAppearing()
    {
        #region Código para cargar página de Scan BarCode desde el teléfono.
        BarcodePage barcodePage = new BarcodePage();
        #endregion

        base.OnAppearing();
        ClearComponent();
        SetFocusText();

        #region Código para cargar página de Scan BarCode desde el teléfono.
        if (DeviceInfo.Model != "MC33" && DeviceInfo.Model != "MC3300x" && DeviceInfo.Model != "RFD0020")
        {
            btn_escanear.IsVisible = true;
            btn_escanear.IsEnabled = true;
            if (barcodePage.Flag && barcodePage.CodigoDetectado) //True
            {
                txtCodigo.Text = barcodePage.SetBarcode(); //Set text -> Codigo de barras recuperado.
                barcodePage.Flag = !barcodePage.Flag;
                barcodePage.CodigoDetectado = !barcodePage.CodigoDetectado;
            }
        }
        #endregion
    }
    private void SetFocusText()
    {
        _ = Task.Delay(200).ContinueWith(t => {
            txtCodigo.Focus();
        });
    }
    void ClearComponent()
    {
        GvGridDatos.IsVisible = false;
        lblProducto.Text = string.Empty;
        lblError2.Text = string.Empty;
        cboNombreRepo.SelectedIndex = -1;
        txtDia.Text = string.Empty;
        txtMes.Text = string.Empty;
        txtAno.Text = string.Empty;
        GvGridDatos.IsVisible = false;
        cboDispo.SelectedIndex = -1;
        cboLimpieza.SelectedIndex = -1;
        CboFefo.SelectedIndex = -1;
        CboFleje.SelectedIndex = -1;
        btn_agregar.IsEnabled = false;
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
        txtCodigo.Focus();
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
        DatosSMM_TomaInventario dti = new DatosSMM_TomaInventario();
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            string codpro = dti.ValidaCodProducto(txtCodigo.Text);
            if (txtCodigo.Text.Equals(string.Empty))
            {
                lblError2.Text = "ingrese Codigo Producto";
                lblError2.IsVisible = true;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                //txt_pallet.Focus();
            }
            else if (codpro.Equals(""))
            {
                lblError2.IsVisible = true;
                await DisplayAlert("Alerta", "Codigo Producto no existe", "Aceptar");
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                txtCodigo.Text = string.Empty;
                txtCodigo.Focus();
            }
            else
            {
                string codiPro = dti.TraeCodProducti(txtCodigo.Text);
                List<SMMDatoProductosRecepcion> ls = dti.ListaDatosProdRes(codiPro, txtCodigo.Text);

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
    private async void txtCodigo_Completed(object sender, EventArgs e)
    {
        DatosSMM_TomaInventario dti = new DatosSMM_TomaInventario();
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            string codpro = dti.ValidaCodProducto(txtCodigo.Text);
            if (txtCodigo.Text.Equals(string.Empty))
            {
                lblError2.Text = "ingrese Codigo Producto";
                lblError2.IsVisible = true;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                txtCodigo.Focus();
            }
            else if (codpro.Equals(""))
            {
                lblError2.IsVisible = true;
                await DisplayAlert("Alerta", "Codigo Producto no existe", "Aceptar");
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                txtCodigo.Text = string.Empty;
                txtCodigo.Focus();
            }
            else
            {
                string codiPro = dti.TraeCodProducti(txtCodigo.Text);
                List<SMMDatoProductosRecepcion> ls = dti.ListaDatosProdRes(codiPro, txtCodigo.Text);

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
    private void cboNombreRepo_SelectionChanged(object sender, EventArgs e)
    {
        txtDia.Focus();
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
            else
            {
                txtAno.Focus();

            }
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
            else
            {
                GvGridDatos.IsVisible = true;
                btn_agregar.IsEnabled = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    private void cboDispo_SelectionChanged(object sender, EventArgs e)
    {
        //cboLimpieza.Focus();
    }
    private void cboLimpieza_SelectionChanged(object sender, EventArgs e)
    {
        //CboFefo.Focus();
    }
    private void CboFefo_SelectionChanged(object sender, EventArgs e)
    {
        //CboFleje.Focus();
    }
    private void btn_agregar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (txtCodigo.Text.Equals(string.Empty) || txtCodigo.Equals(null))
            {
                txtCodigo.Focus();
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
                    DatosSMM_TomaInventario dTi = new DatosSMM_TomaInventario();
                    string codPro = dTi.TraeCodProducti(txtCodigo.Text);

                    if (dTi.ValidaCodProducto(txtCodigo.Text) != "")
                    {
                        try
                        {

                            int v_idNVerificado = Convert.ToInt32(cboNombreRepo.SelectedValue);
                            int v_Verificador = App.Iduser;
                            string v_CodProd = codPro;
                            string v_CodBar = txtCodigo.Text;
                            string v_dispo = cboDispo.SelectedIndex == -1 ? "0" : cboDispo.SelectedValue.ToString() ??
                                throw new InvalidOperationException();
                            string v_limp = cboLimpieza.SelectedIndex == -1 ? "0" : cboLimpieza.SelectedValue.ToString() ??
                                throw new InvalidOperationException();
                            string v_fefo = CboFefo.SelectedIndex == -1 ? "0" : CboFefo.SelectedValue.ToString() ??
                                throw new InvalidOperationException();
                            string v_fleje = CboFleje.SelectedIndex == -1 ? "0" : CboFleje.SelectedValue.ToString() ??
                                throw new InvalidOperationException();
                            string v_fechVenc = txtAno.Text + "-" + txtMes.Text + "-" + txtDia.Text;

                            DatosCumplimientoRepoSalaSMM cum = new DatosCumplimientoRepoSalaSMM();


                            string rest = cum.insertaRegistroCumplimiento(v_idNVerificado, v_Verificador, v_CodProd, v_CodBar, v_dispo, v_limp, v_fefo, v_fleje, v_fechVenc);
                            if (rest.Equals("0"))
                            {
                                DisplayAlert("Alerta", "Registrado", "Aceptar");
                                txtCodigo.Text = string.Empty;
                                txtCodigo.Focus();
                                GvGridDatos.IsVisible = false;
                                lblProducto.Text = string.Empty;
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
                        DisplayAlert("Alerta", "DUN 14 NO ENCONTRADO", "Aceptar");
                        txtCodigo.Focus();
                    }
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
        txtCodigo.Focus();
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