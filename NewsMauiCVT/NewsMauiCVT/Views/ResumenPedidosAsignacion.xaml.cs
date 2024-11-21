using System.Data;

namespace NewsMauiCVT.Views;

public partial class ResumenPedidosAsignacion : ContentPage
{
    DataTable Tabla = new();
    public ResumenPedidosAsignacion(DataTable tablaRecibida)
	{
        InitializeComponent();
        Tabla = tablaRecibida;
        LoadData();
    }
    private void LoadData()
    {
        GvData.ItemsSource = Tabla;
        GvData.Columns["Transfer_ID"].Caption = "Folio";
        GvData.Columns["Transfer_ID"].Width = 110;
        GvData.Columns["ItemCode"].Caption = "Cod. Producto";
        GvData.Columns["ItemCode"].Width = 110;
        GvData.Columns["Lote"].Caption = "Lote";
        GvData.Columns["Lote"].Width = 110;
        GvData.Columns["Cantidad"].Caption = "Cantidad";
        GvData.Columns["Cantidad"].Width = 110;
        string totalcoun = GvData.VisibleRowCount.ToString();
        lblCantPallets.Text = "Asignaciones ingresadas: " + totalcoun;
    }
}