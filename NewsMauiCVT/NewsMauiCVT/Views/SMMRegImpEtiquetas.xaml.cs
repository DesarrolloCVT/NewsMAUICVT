using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMMRegImpEtiquetas : ContentPage
{
    #region Variables Globales
    string v_CodigoProducto = "";
    #endregion

    public SMMRegImpEtiquetas()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
        SetFocusText();
    }
    private void SetFocusText()
    {
        _ = Task.Delay(200).ContinueWith(t => {
            txt_pallet.Focus();
        });
    }
    void ClearComponent()
    {
        txt_pallet.Text = string.Empty;
        btn_agregar.IsEnabled = false;
    }
    private async void Txt_pallet_Completed(object sender, EventArgs e)
    {
        DatosSMM_TomaInventario dti = new DatosSMM_TomaInventario();
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            string codpro = dti.ValidaCodProducto(txt_pallet.Text);
            if(!txt_pallet.Text.Equals(string.Empty) && !codpro.Equals(""))
            {
                string codiPro = dti.TraeCodProducti(txt_pallet.Text);
                List<SMMDatoProductosRecepcion> ls = dti.ListaDatosProdRes(codiPro, txt_pallet.Text);

                //string Umd = "";
                foreach (var t in ls)
                {
                    //  Umd = t.UomCode;
                    v_CodigoProducto = t.ItemCode;
                }

                lblProducto.Text = codpro.ToString();
                lblError2.Text = string.Empty;
                lblError2.IsVisible = false;
                btn_agregar.IsEnabled = true;

            }
            else if (txt_pallet.Text.Equals(string.Empty))
            {
                lblError2.Text = "ingrese Codigo Producto";
                lblError2.IsVisible = true;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                txt_pallet.Focus();
            }
            else if (codpro.Equals(""))
            {
                lblError2.IsVisible = true;
                DisplayAlert("Alerta", "Codigo Producto no existe", "Aceptar");
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                txt_pallet.Text = string.Empty;
                txt_pallet.Focus();

            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
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
                        string rest = sm.insertaRegEtiPrec(codPro);
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
    private void txt_pallet_TextChanged(object sender, EventArgs e)
    {

    }
}