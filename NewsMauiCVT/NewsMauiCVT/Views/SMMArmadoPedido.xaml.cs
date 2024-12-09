using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMMArmadoPedido : ContentPage
{
    public SMMArmadoPedido()
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
            txtNPedido.Focus();
        });
    }
    void ClearComponent()
    {
        txtNPedido.Text = string.Empty;
    }
    private void btn_Crear_Clicked(object sender, EventArgs e)
    {
        if (txtNPedido.Text.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "ingrese un numero de RecepcionPallet", "Aceptar");
        }
        else
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                DatosSMM_ArmadoPedido ar = new DatosSMM_ArmadoPedido();

                int FolioPed = ar.ValidaFolioPrdido(Convert.ToInt32(txtNPedido.Text));

                if (FolioPed != 0)
                {
                    Navigation.PushAsync(new SMMArmadoPedidoDetalle(FolioPed) { Title = "Finalizar" });
                }
                else
                {
                    DisplayAlert("Alerta", "Pedido no existe o se encuentra cerrada, Favor verificar", "Aceptar");
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