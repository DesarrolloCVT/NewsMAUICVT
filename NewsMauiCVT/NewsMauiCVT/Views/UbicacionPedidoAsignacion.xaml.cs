using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class UbicacionPedidoAsignacion : ContentPage
{
    #region Variables Globales
    string ItemCode;
    string Lote;
    DataTable dt;
    #endregion

    public UbicacionPedidoAsignacion(string itemcode, string lote)
	{
		InitializeComponent();
        this.ItemCode = itemcode;
        this.Lote = lote;
        dt = new DataTable();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadData();
        LogUsabilidad("ingreso a Ubicacion Pedido Asignacion");
    }
    private async void LoadData()
    {
        try
        {
            lblTituloUbicacion.Text = "Asignacion de MP - Ubicaciones ";
            DatosAsignacionPedidos dap = new DatosAsignacionPedidos();
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                dt = dap.UbicacionPedidoAsignacion(ItemCode, Lote);
                lblItemCode.Text += ItemCode;
                lblLote.Text += Lote;
                if (dt.Columns.Count <= 0)
                {
                    await DisplayAlert("Alerta", "No contiene informacion", "Ok");
                }
                else
                {
                    GvData.ItemsSource = dt;
                    GvData.Columns["Layout_Description"].Caption = "Layout Descripcion";
                    GvData.Columns["Layout_Description"].HorizontalContentAlignment = TextAlignment.Center;
                    GvData.Columns["Layout_Description"].Width = 110;
                    GvData.Columns["CantPallets"].Caption = "Cantidad Pallets";
                    GvData.Columns["CantPallets"].HorizontalContentAlignment = TextAlignment.Center;
                    GvData.Columns["CantPallets"].Width = 110;
                    GvData.Columns["Cantidad"].Caption = "Cantidad";
                    GvData.Columns["Cantidad"].HorizontalContentAlignment = TextAlignment.Center;
                    GvData.Columns["Cantidad"].Width = 110;
                    string totalcoun = GvData.VisibleRowCount.ToString();
                }
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
    private void LogUsabilidad(string accion)
    {
        var Usuario = App.Iduser;
        var Fecha = DateTime.Now;
        var TipoRegistro = accion;
        var IdSubMenu = 0;

        DatosApp datosApp = new DatosApp();
        datosApp.LogUsabilidad(IdSubMenu, TipoRegistro);
    }
}