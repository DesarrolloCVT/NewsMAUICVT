using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class ResumenAsignadosMP : ContentPage
{
    int folio;
    public ResumenAsignadosMP(int folioRecibido)
    {
        InitializeComponent();
        folio = folioRecibido;
        LoadData();
    }
	private async void LoadData()
	{
        try
        {
            DatosAsignacionMP dct = new DatosAsignacionMP();
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                DataTable dt = dct.DetalleTransferenciasAsignadas(folio);
                GvData.ItemsSource = dt;
                GvData.Columns["Transfer_Id"].Caption = "Folio";
                GvData.Columns["Transfer_Id"].HorizontalContentAlignment = TextAlignment.Center; 
                GvData.Columns["Transfer_Id"].Width = 110;
                GvData.Columns["ItemCode"].Caption = "Cod. Producto";
                GvData.Columns["ItemCode"].HorizontalContentAlignment = TextAlignment.Center;
                GvData.Columns["ItemCode"].Width = 110;
                GvData.Columns["Lote"].Caption = "Lote";
                GvData.Columns["Lote"].HorizontalContentAlignment = TextAlignment.Center;
                GvData.Columns["Lote"].Width = 110;
                string totalcoun = GvData.VisibleRowCount.ToString();
                lblCantPallets.Text = "Cantidad de Asignaciones: " + totalcoun;
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
}