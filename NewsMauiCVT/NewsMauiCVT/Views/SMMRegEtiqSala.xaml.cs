using Controls.UserDialogs.Maui;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMMRegEtiqSala : ContentPage
{
    string v_nomProd = "";
    public SMMRegEtiqSala()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
    }

    protected override void OnAppearing()
    {

        base.OnAppearing();
        txt_pallet.Text = string.Empty;
        txt_pallet.Focus();
        btn_agregar.IsEnabled = false;
        txt_cantidad.Text = string.Empty;
        txtDia.Text = string.Empty;
        txtMes.Text = string.Empty;
        txtAno.Text = string.Empty;

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
            //txtDia.Focus();
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
            //txtMes.Focus();
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
            //txtAno.Focus();
        }
        else { btn_agregar.IsEnabled = true; }
    }
    private async void txt_pallet_Completed(object sender, EventArgs e)
    {
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
                string codiPro = dti.TraeCodProducti(txt_pallet.Text);
                List<SMMDatoProductosRecepcion> ls = dti.ListaDatosProdRes(codiPro, txt_pallet.Text);

                //string Umd = "";
                foreach (var t in ls)
                {
                    //  Umd = t.UomCode;
                    //v_CodigoProducto = t.ItemCode;
                    v_nomProd = t.ItemName;
                }

                lblProducto.Text = codpro.ToString();
                lblError2.Text = string.Empty;
                lblError2.IsVisible = false;
                //btn_agregar.IsEnabled = true;

                txt_cantidad.Focus();
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private void txt_cantidad_Completed(object sender, EventArgs e)
    {
        txtDia.Focus();
    }
    private void btn_agregar_Clicked(object sender, EventArgs e)
    {
        if (txt_pallet.Text.Equals(string.Empty))
        {
            lblError2.Text = "Ingrese codigo";
            lblError2.IsVisible = true;
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            txt_pallet.Focus();
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
                    try
                    {
                        DatosSMM_Etiquetas sm = new DatosSMM_Etiquetas();

                        string fven = txtDia.Text + "-" + txtMes.Text + "-" + txtAno.Text;
                        string rest = sm.insertaRegEtiqSala(codPro, v_nomProd, Convert.ToInt32(txt_cantidad.Text), fven);
                        if (rest.Equals("0"))
                        {
                            DisplayAlert("Alerta", "Registrado", "Aceptar");
                            txt_pallet.Text = string.Empty;
                            txt_pallet.Focus();
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
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
}