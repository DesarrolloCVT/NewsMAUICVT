using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class SMMOrdenDeVentaRegistros : ContentPage
{
    int _folio = 0;
    public SMMOrdenDeVentaRegistros(int folio)
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        cargadatos(folio);
        lblFolioOrden.Text = "Orden N°:" + folio.ToString();
        _folio = folio;
    }
    void cargadatos(int idFolio)
    {
        DatosSMMOrdenDeVenta dCu = new DatosSMMOrdenDeVenta();
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {

            DataTable dt = dCu.RegistroOrdenVEnta(Convert.ToInt32(idFolio));
            List<TotalesOrden> lt = dCu.DatosTotales(Convert.ToInt32(idFolio));

            GvData.ItemsSource = dt;
            GvData.Columns["CodProducto"].Caption = "CodProducto";
            GvData.Columns["CodProducto"].Width = 110;
            GvData.Columns["ItemName"].Caption = "Producto";
            GvData.Columns["ItemName"].Width = 180;
            GvData.Columns["Upc"].Caption = "Upc";
            GvData.Columns["Upc"].Width = 70;
            GvData.Columns["PrecioVenta"].Caption = "Precio";
            GvData.Columns["PrecioVenta"].DisplayFormat = "C0";
            GvData.Columns["PrecioVenta"].Width = 80;
            GvData.Columns["Margen"].Caption = "Margen";
            GvData.Columns["Margen"].Width = 80;
            GvData.Columns["Stock"].Caption = "Stock";
            GvData.Columns["Stock"].Width = 80;
            GvData.Columns["Contribucion"].Caption = "Contrib.";
            GvData.Columns["Contribucion"].Width = 80;
            GvData.Columns["Cantidad"].Caption = "Cant.";
            GvData.Columns["Cantidad"].Width = 80;
            GvData.Columns["Porc_Descuento"].Caption = "%Desc.";
            GvData.Columns["ImptoTipo"].Caption = "Tipo Impuesto";
            GvData.Columns["ImptoTipo"].Width = 80;
            GvData.Columns["ImptoProcentaje"].Caption = "Porc. Impuesto";
            GvData.Columns["ImptoProcentaje"].Width = 80;
            GvData.Columns["TotalImpto"].Caption = "Total impuesto";
            GvData.Columns["TotalImpto"].DisplayFormat = "C0";
            GvData.Columns["TotalImpto"].Width = 80;

            GvData.Columns["Porc_Descuento"].Width = 80;
            GvData.Columns["IdOrdenVentas"].IsVisible = false;
            GvData.Columns["IdDetalleOrden"].IsVisible = false;
            GvData.Columns["Familia"].IsVisible = false;
            GvData.Columns["TotalLinea"].IsVisible = false;
            GvData.Columns["TotalContrib"].IsVisible = false;
            GvData.Columns["PrecioPromedio"].IsVisible = false;
            GvData.AllowVirtualHorizontalScrolling = true;
            GvData.IsVerticalScrollBarVisible = false;
            GvData.IsHorizontalScrollBarVisible = false;


            foreach (var item in lt)
            {
                txtSubtotal.Text = item.Subtotal.ToString();
                txtTotalImp.Text = item.Impto.ToString();
                txtTotal.Text = item.Total.ToString();
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private void btnSalir_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SMMOrdenDeVentaDetalle(_folio));
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
}