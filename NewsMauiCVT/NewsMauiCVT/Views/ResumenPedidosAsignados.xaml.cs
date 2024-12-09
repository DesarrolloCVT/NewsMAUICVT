using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class ResumenPedidosAsignados : ContentPage
{
    #region Variables Globales
    int folio;
    DatosAsignacionPedidos dap;
    DataTable dt;
    #endregion
    public ResumenPedidosAsignados(int folioRecibido)
	{
		InitializeComponent();
        #region Inicializadores
        folio = folioRecibido;
        dt = new DataTable();
        dap = new DatosAsignacionPedidos();
        #endregion
        LoadData();
    }
    private async void LoadData()
    {
        try
        {
            dap = new DatosAsignacionPedidos();
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                dt = dap.DetallePedidosAsignados(folio);
                GvData.ItemsSource = dt;
                GvData.Columns["Order_ID"].Caption = "Folio";
                GvData.Columns["Order_ID"].HorizontalContentAlignment = TextAlignment.Center;
                GvData.Columns["Order_ID"].Width = 80;
                GvData.Columns["ItemCode"].Caption = "Cod. Producto";
                GvData.Columns["ItemCode"].HorizontalContentAlignment = TextAlignment.Center;
                GvData.Columns["ItemCode"].Width = 120;
                GvData.Columns["Lote"].Caption = "Lote";
                GvData.Columns["Lote"].HorizontalContentAlignment = TextAlignment.Center;
                GvData.Columns["Lote"].Width = 130;
                GvData.Columns["Cantidad"].Caption = "Cantidad";
                GvData.Columns["Cantidad"].HorizontalContentAlignment = TextAlignment.Center;
                GvData.Columns["Cantidad"].Width = 100;
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
            DataTable dataTable = new DataTable();
            dataTable = dap.DetallePedidosAsignados(folio);
            var data = GvData.SelectedRowHandle;
            string lote = (string)dataTable.Rows[data]["lote"];
            string codigoProducto = (string)dataTable.Rows[data]["itemcode"];
            dt = dap.UbicacionPedidoAsignacion(codigoProducto, lote);

            if (dt.Columns.Count <= 0)
            {
                await DisplayAlert("Alerta", "No contiene informacion", "Ok");
            }
            else
            {
                await Navigation.PushAsync(new UbicacionPedidoAsignacion(codigoProducto, lote));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
            await DisplayAlert("Alerta", "Se ha producido un error al momento de traer los datos ", "OK");
        }
    }
}