using DevExpress.Maui.DataGrid;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class ResumenAsignadosMP : ContentPage
{
    #region Variables Globales
    int folio;
    DataTable dt;
    DatosAsignacionMP dct;
    DataTable dataTable;
    #endregion

    public ResumenAsignadosMP(int folioRecibido)
    {
        #region Inicializacion
        InitializeComponent();
        folio = folioRecibido;
        dt = new DataTable();
        dct = new DatosAsignacionMP();
        dataTable = new DataTable();
        #endregion

        LoadData();
    }
	private async void LoadData()
	{
        try
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                dt = dct.DetalleTransferenciasAsignadas(folio);

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
                GvData.Columns["Recepcion"].Caption = "Recepcion";
                GvData.Columns["Recepcion"].HorizontalContentAlignment = TextAlignment.Center;
                GvData.Columns["Recepcion"].Width = 130;
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
    private async void btn_UbicacionesPorLotre_Clicked(object sender, EventArgs e)
    {
        try
        {   
            dataTable = dct.DetalleTransferenciasAsignadas(folio);
            int SelectedRowHandle = GvData.SelectedRowHandle;
            string lote = (string)dataTable.Rows[SelectedRowHandle]["lote"];
            string codigoProducto = (string)dataTable.Rows[SelectedRowHandle]["itemcode"];
            dt = dct.UbicacionMPAsignacion(codigoProducto, lote);

            if (dt.Columns.Count <= 0)
            {
                await DisplayAlert("Alerta", "No contiene informacion", "Ok");
            }
            else
            {
                await Navigation.PushAsync(new UbicacionMPAsignacion(codigoProducto, lote));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
            await DisplayAlert("Alerta", "Se ha producido un error al momento de traer los datos ", "OK");
        }
    }
}