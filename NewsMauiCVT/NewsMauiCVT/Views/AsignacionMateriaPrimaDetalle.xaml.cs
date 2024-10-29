using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class AsignacionMateriaPrimaDetalle : ContentPage
{
	public AsignacionMateriaPrimaDetalle()
	{
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
        LogUsabilidad("Ingreso");
    }
    private void ClearComponent()
    {
        txtPallet.Text = string.Empty;
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
                GvData.Columns["Transfer_Id"].HorizontalContentAlignment = TextAlignment.Center;
                GvData.Columns["Package_SSCC"].Caption = "N° de Pallet";
                GvData.Columns["Package_SSCC"].Width = 110;
                GvData.Columns["Package_SSCC"].HorizontalContentAlignment = TextAlignment.Center;
                GvData.Columns["ArticleProvider_CodClient"].Caption = "Cod. Producto";
                GvData.Columns["ArticleProvider_CodClient"].Width = 110;
                GvData.Columns["ArticleProvider_CodClient"].HorizontalContentAlignment = TextAlignment.Center;
                GvData.Columns["ArticleProvider_Description"].Caption = "Producto";
                GvData.Columns["ArticleProvider_Description"].Width = 110;
                GvData.Columns["ArticleProvider_Description"].HorizontalContentAlignment = TextAlignment.Center;
                GvData.Columns["Package_Quantity"].Caption = "Cantidad";
                GvData.Columns["Package_Quantity"].Width = 110;
                GvData.Columns["Package_Quantity"].HorizontalContentAlignment = TextAlignment.Center;
                GvData.Columns["Site_ShortDescription"].Caption = "Sitio";
                GvData.Columns["Site_ShortDescription"].Width = 110;
                GvData.Columns["Site_ShortDescription"].HorizontalContentAlignment = TextAlignment.Center;
                GvData.Columns["Package_ProductionDate"].Caption = "Fecha Producción";
                GvData.Columns["Package_ProductionDate"].Width = 110;
                GvData.Columns["Package_ProductionDate"].HorizontalContentAlignment = TextAlignment.Center;
                GvData.Columns["Layout_ShortDescription"].Caption = "Ubicacion";
                GvData.Columns["Layout_ShortDescription"].Width = 110;
                GvData.Columns["Layout_ShortDescription"].HorizontalContentAlignment = TextAlignment.Center;
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
    private void txtPallet_Completed(object sender, EventArgs e)
    {
        DatosPallets dp = new DatosPallets();
        List<PalletClass> list = dp.ObtieneInfoPallet(txtPallet.Text);

        if (list.Count > 0)
        {
            LoadData(111130);
            Console.WriteLine("txtPallet_Completed: Pallet valido");
            DisplayAlert("Alerta", "El pallet es: \n" + txtPallet.Text, "OK");
        }
        else
        {
            Console.WriteLine("txtPallet_Completed: Pallet invalido");
            DisplayAlert("Alerta", "El número de pallet ingresado no es válido: \n" + txtPallet.Text, "OK");
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