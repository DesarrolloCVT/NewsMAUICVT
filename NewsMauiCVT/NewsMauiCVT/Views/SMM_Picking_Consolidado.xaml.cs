using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMM_Picking_Consolidado : ContentPage
{
    string _CodigoProducto = "";
    int _CantidadConsoli = 0;
    public SMM_Picking_Consolidado()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        DateTime FConsolidado = Convert.ToDateTime(App.Fconsoli);
        string fecha = FConsolidado.Date.Day + " - " + FConsolidado.Date.Month + " - " + FConsolidado.Date.Year;
        lblPick.Text = "Fecha Consolidado: " + fecha;
        lblPick1.Text = "Departamento: " + App.DptoConsolidado.ToString();
    }
    protected override void OnAppearing()
    {

        base.OnAppearing();
        ClearComponent();
        txt_pallet.Focus();
        //txt_Bodega.SelectedIndex = -1;
    }
    void ClearComponent()
    {
        btn_agregar.IsEnabled = false;
        lblError1.IsVisible = false;
        lblError2.IsVisible = false;
        txt_pallet.Text = string.Empty;
        lblProducto.Text = string.Empty;
        txt_cantidad.Text = string.Empty;
        lblProducto1.Text = string.Empty;
        lblError1.Text = string.Empty;
        lblError2.Text = string.Empty;
    }
    private void FConsolidado_DateSelected(object sender, DateChangedEventArgs e)
    {
        txt_pallet.Focus();
    }
    private void Txt_pallet_Completed(object sender, EventArgs e)
    {

        DatosSMM_TomaInventario dti = new DatosSMM_TomaInventario();
        DatosPickingConsolidadoSMM pk = new DatosPickingConsolidadoSMM();

        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            string codpro = dti.ValidaCodProducto(txt_pallet.Text);

            if (txt_pallet.Text.Equals(string.Empty))
            {
                lblError1.Text = "ingrese Codigo Producto";
                lblError1.IsVisible = true;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                txt_pallet.Focus();
            }
            else if (codpro.Equals(""))
            {
                lblError1.IsVisible = true;
                txt_pallet.Focus();
                lblError1.Text = "Cod.Prodcuto No Existe";
                txt_pallet.Text = string.Empty;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");

            }
            else
            {

                // Fvenci.Focus();
                DateTime FConsolidado = Convert.ToDateTime(App.Fconsoli);
                string fecha = FConsolidado.Date.Year + "-" + FConsolidado.Date.Month + "-" + FConsolidado.Date.Day;
                _CodigoProducto = dti.TraeCodProducti(txt_pallet.Text);
                _CantidadConsoli = pk.ValidaProductoConsolidado(_CodigoProducto, fecha);

                if (_CantidadConsoli == 0)
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    DisplayAlert("Alerta", "Fecha Consolidado Erroneo , Codigo de Producto no existe O Producto supero Cantidad Solicitada , favor verificar", "Aceptar");
                }
                else
                {

                    txt_cantidad.Focus();
                    lblProducto.Text = codpro.ToString();
                    lblProducto1.Text = "Cantidad disponible : " + _CantidadConsoli;
                    lblError1.Text = string.Empty;
                    lblError1.IsVisible = false;
                }


            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private void Txt_cantidad_Completed(object sender, EventArgs e)
    {
        if (txt_pallet.Text.Equals(string.Empty) || txt_pallet.Text.Equals("0"))
        {
            lblError2.Text = "ingrese Codigo Producto";
            lblError2.IsVisible = true;
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            txt_pallet.Focus();
        }
        else if (Convert.ToInt32(txt_cantidad.Text) > _CantidadConsoli)
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Cantidad Pickeada no debe ser mayor a la cantidad solicitada, Cantidad Solicitada : " + _CantidadConsoli, "Aceptar");

        }
        else { btn_agregar.IsEnabled = true; }
    }
    private void Btn_agregar_Clicked(object sender, EventArgs e)
    {

        if (txt_pallet.Text.Equals(string.Empty))
        {
            lblError1.Text = "ingrese Codigo Producto";
            lblError1.IsVisible = true;
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            txt_pallet.Focus();
        }
        else if (txt_cantidad.Text.Equals(string.Empty))
        {
            lblError2.Text = "Ingrese Cantidad";
            lblError2.IsVisible = true;
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            txt_cantidad.Focus();
        }
        else
        {

            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {

                DatosPickingConsolidadoSMM pkc = new DatosPickingConsolidadoSMM();

                DateTime FConsolidado = Convert.ToDateTime(App.Fconsoli);
                string fecha = FConsolidado.Date.Year + "-" + FConsolidado.Date.Month + "-" + FConsolidado.Date.Day;

                int res = pkc.insertaRegistro(fecha, _CodigoProducto, Convert.ToInt32(txt_cantidad.Text), App.DptoConsolidado.ToString());

                if (res != 0)
                {
                    DisplayAlert("Alerta", "Registrado", "Aceptar");
                    txt_pallet.Text = string.Empty;
                    txt_pallet.Focus();
                    txt_cantidad.Text = string.Empty;
                    lblProducto.Text = string.Empty;
                    lblProducto1.Text = string.Empty;
                    btn_agregar.IsEnabled = false;
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    DisplayAlert("Alerta", "ERROR AL REGISTRAR,CONTACTAR CON ADMINISTRADOR", "Aceptar");
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }
    }
    private async void Btn_Terminar_Clicked(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Confirmar", "Estas seguro de Terminar", "SI", "NO");
        if (result)
        {
            await Navigation.PopToRootAsync();
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            Microsoft.Maui.Controls.Application.Current.Quit();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.


        }
        else
        {
            await Navigation.PushAsync(new PageMain());
        }
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
}