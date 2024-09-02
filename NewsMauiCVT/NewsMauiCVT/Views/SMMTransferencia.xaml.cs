using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMMTransferencia : ContentPage
{
    public SMMTransferencia()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
        SetFocusText();

        /*Shell shell = new Shell();
        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Flyout);
        shell.FlyoutHeaderBehavior = FlyoutHeaderBehavior.Fixed;
        shell.FlyoutVerticalScrollMode = ScrollMode.Auto;*/
    }
    private void SetFocusText()
    {
        _ = Task.Delay(200).ContinueWith(t => {
            txtFolioTransferencia.Focus();
        });
    }
    void ClearComponent()
    {
        txtFolioTransferencia.Text = string.Empty;
    }
    private void btn_Nuevo_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SMM_TransferenciaCabecera { Title = "Finalizar" });
    }
    private async void btn_Reanudar_Clicked(object sender, EventArgs e)
    {
        if (txtFolioTransferencia.Text.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            await DisplayAlert("Alerta", "ingrese un numero de Transferencia", "Aceptar");
        }
        else
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {


                DatosTransferenciaSMM rc = new DatosTransferenciaSMM();

                int FolioTrans = rc.ValidaTransferencia(Convert.ToInt32(txtFolioTransferencia.Text), 1);

                if (FolioTrans != 0)
                {
                    await Navigation.PushAsync(new SMM_TansferenciaDetalle(FolioTrans) { Title = "Finalizar" });
                }
                else
                {
                    await DisplayAlert("Alerta", "Transferencia no existe o se encuentra cerrada, Favor verificar", "Aceptar");
                    txtFolioTransferencia.Text = string.Empty;
                    txtFolioTransferencia.Focus();
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
}