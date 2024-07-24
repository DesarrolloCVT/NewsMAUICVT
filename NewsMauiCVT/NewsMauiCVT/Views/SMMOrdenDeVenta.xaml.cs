using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMMOrdenDeVenta : ContentPage
{
    public SMMOrdenDeVenta()
    {
        InitializeComponent();

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        txtFolio.Text = string.Empty;
    }

    private void btnCreaCliente_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SMMCreaCliente { Title = "Finalizar" });

    }

    private void btnOrdenCab_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SMMOdenDeVentaCabecera { Title = "Finalizar" });
    }

    private void btnReanuda_Clicked(object sender, EventArgs e)
    {
        if (txtFolio.Text.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "ingrese un numero de Orden", "Aceptar");
        }
        else
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {


                DatosSMMOrdenDeVenta rc = new DatosSMMOrdenDeVenta();

                int FolioOrden = rc.ValidaFolioOrden(Convert.ToInt32(txtFolio.Text));

                if (FolioOrden != 0)
                {
                    Navigation.PushAsync(new SMMOrdenDeVentaDetalle(FolioOrden) { Title = "Finalizar" });
                }
                else
                {
                    DisplayAlert("Alerta", "Orden no existe o se encuentra cerrada, Favor verificar", "Aceptar");
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

}