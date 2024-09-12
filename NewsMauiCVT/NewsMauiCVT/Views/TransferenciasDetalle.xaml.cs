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
    private async void LoadData(int folio)
    {
        try
        {
            DetalleConsultaTransferencia dct = new DetalleConsultaTransferencia();
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                DataTable dt = dct.DetalleConsultaTransferencias(folio);
                GvData.ItemsSource = dt;
                GvData.Columns["Transfer_Id"].Caption = "Folio";
                GvData.Columns["Transfer_Id"].Width = 110;
                GvData.Columns["Package_SSCC"].Caption = "N° de Pallet";
                GvData.Columns["Package_SSCC"].Width = 110;
                GvData.Columns["ArticleProvider_CodClient"].Caption = "Cod. Producto";
                GvData.Columns["ArticleProvider_CodClient"].Width = 110;
                GvData.Columns["ArticleProvider_Description"].Caption = "Producto";
                GvData.Columns["ArticleProvider_Description"].Width = 110;
                GvData.Columns["Package_Quantity"].Caption = "Cantidad";
                GvData.Columns["Package_Quantity"].Width = 110;
                GvData.Columns["Site_ShortDescription"].Caption = "Sitio";
                GvData.Columns["Site_ShortDescription"].Width = 110;
                GvData.Columns["Package_ProductionDate"].Caption = "Fecha Producción";
                GvData.Columns["Package_ProductionDate"].Width = 110;
                GvData.Columns["Layout_ShortDescription"].Caption = "Ubicacion";
                GvData.Columns["Layout_ShortDescription"].Width = 110;
                string totalcoun = GvData.VisibleRowCount.ToString();
                lblCantPallets.Text = "Cantidad Pallets: " + totalcoun;
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine("LoadData: " + ex.ToString());
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
                bool resp = dt.InsertaTransferencia(transferId, packageId);

                if (resp)
                {
                    lblConfirm.Text = "Pallet agregado correctamente";
                    lblConfirm.IsVisible = true;
                    txt_pallet.Text = string.Empty;
                    txt_pallet.Focus();
                    LoadData(transferId);
                }
                else
                {
                    lblError.Text = "No se ha agregado Pallet";
                    lblError.IsVisible = true;
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    txt_pallet.Text = string.Empty;
                    txt_pallet.Focus();
                    LoadData(transferId);
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
            lblError.Text = "Seleccione un N° de Pallet válido";
            lblError.IsVisible = true;
            _ = Task.Delay(100).ContinueWith(t => {
                txt_pallet.Focus();
            });
        }
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return false;
    }
}