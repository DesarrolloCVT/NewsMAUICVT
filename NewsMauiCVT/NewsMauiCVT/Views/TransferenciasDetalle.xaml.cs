using Microsoft.Maui.Platform;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class TransferenciasDetalle : ContentPage
{
   private int transferId;
	public TransferenciasDetalle(int value)
	{
		InitializeComponent();
        transferId = value;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        SetFocusText();
        ClearComponent();
    }
    private void ClearComponent()
    {
        txt_pallet.Text = string.Empty;
        lblError.IsVisible = false;
        lblConfirm.IsVisible = false;
    }
    private async void LoadData(string idPosicion)
    {
        DatosConsultaUbicacion dCu = new DatosConsultaUbicacion();
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            DataTable dt = dCu.DetalleConsultaUbicacion(Convert.ToInt32(idPosicion));
            GvData.ItemsSource = dt;
            GvData.Columns["Package_SSCC"].Caption = "N° de Pallet";
            GvData.Columns["Package_SSCC"].Width = 110;
            GvData.Columns["ArticleProvider_CodClient"].Caption = "Cod. Producto";
            GvData.Columns["ArticleProvider_CodClient"].Width = 110;
            GvData.Columns["Package_Quantity"].Caption = "Cantidad";
            GvData.Columns["Package_Quantity"].Width = 110;
            GvData.Columns["ArticleProvider_Description"].Caption = "Producto";
            GvData.Columns["ArticleProvider_Description"].Width = 110;
            GvData.Columns["Staff_Name"].Caption = "Usuario";
            GvData.Columns["Staff_Name"].Width = 110;
            GvData.Columns["Package_ProductionDate"].Caption = "Fecha Producción";
            GvData.Columns["Package_ProductionDate"].Width = 110;
            string totalcoun = GvData.VisibleRowCount.ToString();
            
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private void SetFocusText()
    {
        _ = Task.Delay(200).ContinueWith(t => {
            txt_pallet.Focus();
        });
    }
    private void txt_pallet_Completed(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txt_pallet.Text))
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {

                DatosTransferencia dt = new DatosTransferencia();
                int packageId = int.Parse(txt_pallet.Text);

                int resp = dt.InsertaTransferencia(transferId, packageId);

                if (resp != 0)
                {
                    lblConfirm.Text = "Pallet agregado correctamente";
                    lblConfirm.IsVisible = true;
                }
                else
                {
                    lblError.Text = "No se ha agregado Pallet";
                    lblError.IsVisible = true;
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    DisplayAlert("Alerta", "Error al Registrar Favor Verificar", "Aceptar");
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            lblError.Text = "Seleccione un Pallet válido";
            lblError.IsVisible = true;
            _ = Task.Delay(100).ContinueWith(t => {
                txt_pallet.Focus();
            });
        }
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
        //return true to prevent back, return false to just do something before going back. 
        return false;
    }
}