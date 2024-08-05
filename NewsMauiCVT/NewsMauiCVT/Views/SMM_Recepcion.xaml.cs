using Controls.UserDialogs.Maui;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMM_Recepcion : ContentPage
{
    public SMM_Recepcion()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        stNuevo.IsVisible = false;
        stReanudar.IsVisible = false;
        cargaDatos();
    }
    protected override void OnAppearing()
    {

        base.OnAppearing();
        stNuevo.IsVisible = false;
        stReanudar.IsVisible = false;
        btn_Nuevo.IsEnabled = true;
        btn_ReanudarRecep.IsEnabled = true;
        txtNFactura.Text = string.Empty;
        cboOC.SelectedIndex = -1;
        cboBodegaResp.SelectedIndex = -1;
        txtFolioRecepcion.Text = string.Empty;
    }
    void cargaDatos()
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            DatosRecepcionSMM ti = new();
            List<OcOpenSMM> lt = ti.TraeOcAbiertas();

            foreach (var t in lt)
            {
                cboOC.Items.Add(t.OCProveedor.ToString());

            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private void Btn_Nuevo_Clicked(object sender, EventArgs e)
    {
        stNuevo.IsVisible = true;
        btn_Nuevo.IsEnabled = false;
        stReanudar.IsVisible = false;
        btn_ReanudarRecep.IsEnabled = true;

    }
    private void Btn_ReanudarRecep_Clicked(object sender, EventArgs e)
    {
        stReanudar.IsVisible = true;
        stNuevo.IsVisible = false;
        btn_Nuevo.IsEnabled = true;
        btn_ReanudarRecep.IsEnabled = false;
        txtFolioRecepcion.Focus();
    }
    private async void Btn_Crear_Clicked(object sender, EventArgs e)
    {

        if (cboOC.SelectedIndex == -1)
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            await DisplayAlert("Confirmar", "Seleccione una Orden de Compra", "OK");
            //lblError.Text = "Seleccione una Orden de Compra";

        }
        else if (txtNFactura.Text == null)
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            await DisplayAlert("Confirmar", "Ingrese un N° de Factura", "OK");

        }
        else
        {
            using (UserDialogs.Instance.Loading("Verificando Datos"))
            {
                await Task.Delay(10);

                var ACC = Connectivity.NetworkAccess;
                if (ACC == NetworkAccess.Internet)
                {
                    string[] ids = new string[2];
                    string n = cboOC.SelectedItem.ToString().Trim(' ');
                    ids = n.Split('-');
                    int oc = Convert.ToInt32(ids[0]);
                    int bodegaRes = Convert.ToInt32(cboBodegaResp.SelectedItem.ToString());

                    DatosRecepcionSMM rc = new();


                    int FolioRec = rc.insertaCabeceraRecepcion(oc, Convert.ToInt32(txtNFactura.Text), bodegaRes, txtComentarios.Text);

                    if (FolioRec != 0)
                    {
                        await Navigation.PushAsync(new SMMDetalleRecepcion(FolioRec) { Title = "Finalizar" });
                    }
                    else
                    {
                        await DisplayAlert("Alerta", "Error Al Crear Recepcion ,Contactar Con Administrador", "Aceptar");
                    }
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
                }
            }


        }


    }
    private void Btn_Reanudar_Clicked(object sender, EventArgs e)
    {
        if (txtFolioRecepcion.Text.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "ingrese un numero de Recepcion", "Aceptar");
        }
        else
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {


                DatosRecepcionSMM rc = new();

                int FolioRec = rc.ValidaFolioRecepcion(Convert.ToInt32(txtFolioRecepcion.Text));

                if (FolioRec != 0)
                {
                    Navigation.PushAsync(new SMMDetalleRecepcion(FolioRec) { Title = "Finalizar" });
                }
                else
                {
                    DisplayAlert("Alerta", "Recepcion no existe o se encuentra cerrada, Favor verificar", "Aceptar");
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