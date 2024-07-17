using Controls.UserDialogs.Maui;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMM_TomaInvenario_Bodegas : ContentPage
{

    public SMM_TomaInvenario_Bodegas()
    {
        InitializeComponent();
        CargaDatos();
        //lblError.Text = string.Empty;
        //lblError.IsVisible = false;
        lblError2.Text = string.Empty;
        lblError2.IsVisible = false;
        lblError3.Text = string.Empty;
        lblError3.IsVisible = false;
        lblError4.Text = string.Empty;
        lblError4.IsVisible = false;
        lblError5.Text = string.Empty;
        lblError5.IsVisible = false;
        cboFolio.Focus();
    }

    protected override async void OnAppearing()
    {

        base.OnAppearing();
        cboFolio.SelectedIndex = -1;
        cboFolio.Focus();
        //txt_Bodega.SelectedIndex = -1;
        btn_agregar.IsEnabled = false;
        //lblError.Text = string.Empty;
        //lblError.IsVisible = false;
        lblError2.Text = string.Empty;
        lblError2.IsVisible = false;
        lblError3.Text = string.Empty;
        lblError3.IsVisible = false;
        lblError4.Text = string.Empty;
        lblError4.IsVisible = false;
        lblError5.Text = string.Empty;
        lblError5.IsVisible = false;
        txt_pallet.Text = string.Empty;
        txt_Npallet.Text = string.Empty;
        lblProducto.Text = string.Empty;
        txt_cantidad.Text = string.Empty;
        txt_Ubicacion.Text = string.Empty;
        txtDia.Text = string.Empty;
        txtMes.Text = string.Empty;
        txtAno.Text = string.Empty;

    }

    void CargaDatos()
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
        txt_Npallet.Focus();
    }
    private void Txt_Npallet_Completed(object sender, EventArgs e)
    {
        DatosSMM_TomaInventario dti = new DatosSMM_TomaInventario();
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {


            int idpro = dti.ValidaPallet(Convert.ToInt32(txt_Npallet.Text));

            if (txt_Npallet.Text.Equals(string.Empty))
            {
                lblError4.Text = "ingrese N° Pallet";
                lblError4.IsVisible = true;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                txt_Npallet.Focus();
            }
            else if (idpro == 0)
            {
                lblError4.IsVisible = true;
                txt_Npallet.Focus();
                lblError4.Text = "N° de Pallet No Existe";
                txt_Npallet.Text = string.Empty;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");

            }
            else
            {
                string itemCode = dti.traeItemCodeProducto(txt_Npallet.Text);
                string CodBarraProdcuto = dti.traeCodBarraProductoBodega02(txt_Npallet.Text);
                //string codpro = dti.TraeCodProducti(CodBarraProdcuto);


                string codprodu = dti.ValidaCodProducto(CodBarraProdcuto);

                List<SMMDatoProductosRecepcion> ls = dti.ListaDatosProdRes(itemCode, CodBarraProdcuto);

                string Umd = "";
                foreach (var t in ls)
                {
                    Umd = t.UomCode;
                    txt_pallet.Text = t.BcdCode;
                }

                // Fvenci.Focus();
                txt_Ubicacion.Focus();
                txt_pallet.IsReadOnly = true;
                //txt_pallet.Text = CodBarraProdcuto.ToString();
                lblProducto.Text = codprodu.ToString() + " " + Umd.ToLower();
                lblError4.Text = string.Empty;
                lblError4.IsVisible = false;
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    //private void Txt_pallet_Completed(object sender, EventArgs e)
    //{
    //    DatosSMM_TomaInventario dti = new DatosSMM_TomaInventario();
    //    var ACC = Connectivity.NetworkAccess;
    //    if (ACC == NetworkAccess.Internet)
    //    {
    //        
    //        if (txt_pallet.Text.Equals(string.Empty))
    //        {
    //            lblError2.Text = "ingrese Codigo Producto";
    //            lblError2.IsVisible = true;
    //            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
    //            txt_pallet.Focus();
    //        }
    //        else if (codpro.Equals(""))
    //        {
    //            lblError2.IsVisible = true;
    //            txt_pallet.Focus();
    //            lblError2.Text = "Cod.Prodcuto No Existe";
    //            txt_pallet.Text = string.Empty;
    //            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");

    //        }
    //        else
    //        {

    //            // Fvenci.Focus();
    //            txt_Npallet.Focus();
    //        
    //            lblError2.Text = string.Empty;
    //            lblError2.IsVisible = false;
    //        }
    //    }
    //    else
    //    {
    //        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
    //        DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
    //    }
    //}
    private void Fvenci_DateSelected(object sender, DateChangedEventArgs e)
    {
        txt_cantidad.Focus();
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
        else if (txt_Ubicacion.Equals(string.Empty))
        {
            txt_Ubicacion.Focus();
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
        }
        else if (txt_pallet.Text.Equals(string.Empty))
        {
            lblError2.Text = "ingrese Codigo Producto";
            lblError2.IsVisible = true;
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            txt_pallet.Focus();
        }
        else if (txt_Npallet.Text.Equals(string.Empty))
        {
            lblError4.Text = "ingrese N° Pallet";
            lblError4.IsVisible = true;
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            txt_Npallet.Focus();
        }
        else if (txt_cantidad.Text.Equals(string.Empty))
        {
            lblError3.Text = "Ingrese Cantidad";
            lblError3.IsVisible = true;
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            txt_cantidad.Focus();
        }
        else if (txt_Ubicacion.Text.Equals(string.Empty))
        {
            lblError5.Text = "Ingrese Ubicacion";
            lblError5.IsVisible = true;
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            txt_Ubicacion.Focus();
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
                    if (dTi.ValidaNumeroDePallet(Convert.ToInt32(txt_Npallet.Text), codPro) != 0)
                    {
                        if (Convert.ToInt32(dTi.ValidaBodegaSMM("02")) != 0)
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
                                int idBod = Convert.ToInt32(dTi.ValidaBodegaSMM("02"));
                                int iduser = App.Iduser;
                                string fecha = txtAno.Text + "-" + txtMes.Text + "-" + txtDia.Text;
                                int ubPasillo = Convert.ToInt32(txt_Ubicacion.Text);
                                int SSCC = Convert.ToInt32(txt_Npallet.Text);

                                bool rest = dTi.insertaDetalleInventarioBodegas(idinv, dn14, codPro, cant, idBod, iduser, ubPasillo, fecha, SSCC, CantxEmp, CantBs, TpEmpaq);

                                DatosPosicionamientoSMM dp = new DatosPosicionamientoSMM();
                                int Package_Id = dp.ObtienePackageIdPosicionamiento(txt_Npallet.Text);
                                bool resActLayPack = dp.ActualizaLayoutPackage(Package_Id, Convert.ToInt32(txt_Ubicacion.Text));

                                bool addloc = dp.AddLocation(Package_Id, Convert.ToInt32(txt_Ubicacion.Text), App.Iduser);


                                if (rest == true && addloc == true)
                                {
                                    DisplayAlert("Alerta", "Registrado", "Aceptar");
                                    txt_Npallet.Text = string.Empty;
                                    txt_Npallet.Focus();
                                    txt_pallet.Text = string.Empty;
                                    txt_Ubicacion.Text = string.Empty;
                                    txt_cantidad.Text = string.Empty;
                                    txtDia.Text = string.Empty;
                                    txtMes.Text = string.Empty;
                                    txtAno.Text = string.Empty;
                                    lblProducto.Text = string.Empty;
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
                            txt_Npallet.Focus();

                        }
                    }
                    else
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        DisplayAlert("Alerta", "N° DE PALLET NO ENCONTRADO", "Aceptar");
                        txt_pallet.Focus();
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

    private void Txt_Ubicacion_Completed(object sender, EventArgs e)
    {
        DatosSMM_TomaInventario SmmDt = new DatosSMM_TomaInventario();

        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            int idSite = SmmDt.ValidaBodegaSMM("02");
            int idLocation = SmmDt.ValidaUbicacion(Convert.ToInt32(txt_Ubicacion.Text), idSite);

            if (txt_Ubicacion.Equals(string.Empty))
            {
                lblError5.Text = "ingrese Codigo de ubicacion";
                lblError5.IsVisible = true;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                txt_Ubicacion.Focus();
            }
            if (idLocation == 0)
            {
                lblError5.Text = "Ubicacion no existe en la bodega actual";
                lblError5.IsVisible = true;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                txt_Ubicacion.Focus();
            }
            else
            {
                txtDia.Focus();
                lblError5.Text = string.Empty;
                lblError5.IsVisible = false;
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
        else { txtAno.Focus(); }


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
        else { txt_cantidad.Focus(); }
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }

}